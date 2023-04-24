using DataView;
using DataView.Entries;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel;
using Excel.DataView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelPart
{
    public class ExcelWeekParser
    {

        private  List<CellViewBinding> _cells = new List<CellViewBinding>();

        private Dictionary<string, List<CellViewBinding>> _weeks = new Dictionary<string, List<CellViewBinding>>();

        public ExcelWeekParser(List<CellViewBinding> cells)
        {
            this._cells = cells;
            InitWeek();
        }



        private void InitWeek()
        {
            string workingWeek = "";

            List<CellViewBinding> cells = new List<CellViewBinding>();
            
            _cells.ForEach(cell =>
           {

               string[] referenceRes = MatchReference(cell.InnerText);
               if (MatchWeek(referenceRes[0],Convert.ToInt32(referenceRes[1]))){

                   if (!string.IsNullOrEmpty(workingWeek) && cells.Count > 0 && workingWeek != cell.InnerText)
                   {
                       if (_weeks.ContainsKey(workingWeek))
                       {
                           _weeks[workingWeek].Add(cell);
                       }
                       else
                       {

                           _weeks.Add(workingWeek, cells);
                       }

                       cells = new List<CellViewBinding>();
                       
                   }

                   workingWeek = cell.InnerText;

               }

               if (!string.IsNullOrEmpty(workingWeek))
                   cells.Add(cell);
                  
           });
        }






        public List<Semaine> GetWeeks()
        {
            List<Semaine> weekViews = new List<Semaine>();
            _weeks.ToList().ForEach(week =>
            {
                weekViews.Add(GetWeek(week.Key));
            });

            return weekViews;


        }

        public Semaine GetWeek(string semainKey)
        {

            List<CellViewBinding> weekCells = new List<CellViewBinding>();
            if (_weeks.ContainsKey(semainKey))
                weekCells = _weeks[semainKey];
            else
                return null;


            Semaine week = new Semaine(weekCells.First().InnerText);
            week.SetMatieres(ParseMatiere(weekCells));

            return week;

        }

        private List<Matiere> ParseMatiere(List<CellViewBinding> weekCells)
        {
            List<Matiere> result = new List<Matiere>();

            weekCells.OrderBy(c => c.RowReference).ThenBy(c => c.ColumnReference).ToList().ForEach(cell => {

                if (Promos.MatchPromo(cell.InnerText) != null)
                {

                    weekCells.Where(wc => wc.RowReference == cell.RowReference && wc.Reference != cell.Reference).ToList().ForEach(matiereCell =>
                    {

                        string matiere = "";
                        string prof = "";
                        string sceance = "";
                        bool visio = false;
                        string salle = "";
                        string[] matierCellSplited = matiereCell.InnerText.Split('\n');
                        if (matierCellSplited.Length > 0)
                        {

                            if (matierCellSplited.Length == 3)
                            {
                                matiere = matierCellSplited[0];

                                if(MatchSeance(matierCellSplited[1]))
                                {
                                    sceance = matierCellSplited[1];
                                    prof = matierCellSplited[2];
                                }
                                else
                                {
                                    prof = matierCellSplited[1];
                                    sceance = matierCellSplited[2];
                                }

                            
                                if (prof.Contains("(visio)"))
                                {
                                    prof.Replace("(visio)", String.Empty);
                                    visio = true;

                                }
                            }


                            if (matierCellSplited.Length == 2)
                            {
                                matiere = matierCellSplited[0];
                                prof = matierCellSplited[1];
                                if (prof.Contains("(visio)"))
                                {
                                    prof.Replace("(visio)", String.Empty);
                                    visio = true;

                                }

                            }


                            if (matierCellSplited.Length == 1)
                                matiere = matierCellSplited[0];



                        }
                        Jour jour = Jours.GetJourByColumn(matiereCell.ColumnReference);
                        List<CellViewBinding> sallesCells = new List<CellViewBinding>();
                        if (jour.ColumnReference.Count > 0)
                        {

                            foreach (string jourColumnRef in jour.ColumnReference)
                            {
                                CellViewBinding salleCell = weekCells.FirstOrDefault(wc => wc.RowReference == matiereCell.RowReference + 4 && wc.ColumnReference == jourColumnRef);
                                if (salleCell != null)
                                {
                                    sallesCells.Add(salleCell);
                                    break;
                                }

                            }


                            sallesCells.ForEach(salleCell =>
                            {
                                if (!string.IsNullOrEmpty(salleCell.InnerText))
                                    salle += salleCell.InnerText+",";
                            });


                        }

                        salle = String.Join(",", salle.Split('+'));
                        salle = String.Join(",", salle.Split('/'));



                        Matiere Matiere = new Matiere(cell.InnerText, matiere, prof, salle, jour.Nom, sceance, visio);
                        result.Add(Matiere);
                    });
                }



            });

            return result;
        }


        private bool MatchSeance(string sceance)
        {
            return sceance.Contains("/");
        }

        private bool MatchWeek(string week, int weekNumber)
        {
            if(week == null || weekNumber == null)
                return false;

            if (week.Contains("S") && weekNumber > 0)
                return true;

            return false;
        }


        private string[] MatchReference(string cellReference)
        {

            Match match = Regex.Match(cellReference, @"([A-Z]+)(\d+)");
            if (match.Success)
            {
                string column = match.Groups[1].Value;
                int row = int.Parse(match.Groups[2].Value);

                return new string[] { column, row.ToString() };

            }

            return new string[] { "","0" };

        }


    }
}
