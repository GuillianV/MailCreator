using DocumentFormat.OpenXml.Spreadsheet;
using Excel;
using Excel.UI;
using ExcelPart.Data;
using ExcelPart.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExcelPart
{
    internal class ExcelWeekParser
    {

        private  List<CellViewBinding> _cells = new List<CellViewBinding>();

        private Dictionary<string, List<CellViewBinding>> _weeks = new Dictionary<string, List<CellViewBinding>>();

        public ExcelWeekParser(List<CellViewBinding> cells)
        {
            this._cells = cells;
            GetWeek();
        }


        public List<CellViewBinding> GetWeekCells(string semainKey)
        {
            List<CellViewBinding> cells = new List<CellViewBinding>();
            if(_weeks.ContainsKey(semainKey))
                cells = _weeks[semainKey];

            ParseWeek(cells);


            return cells;

        }


        private void GetWeek()
        {
            string workingWeek = "";

            List<CellViewBinding> cells = new List<CellViewBinding>();
            
            _cells.ForEach(cell =>
           {


               if(MatchWeek(MatchReference(cell.InnerText)[0],Convert.ToInt32(MatchReference(cell.InnerText)[1]))){

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


        private List<MatiereViewBinding> ParseWeek(List<CellViewBinding> weekCells)
        {

            List<MatiereViewBinding> result = new List<MatiereViewBinding>();

            weekCells.OrderBy(c => c.RowReference).ThenBy(c => c.ColumnReference).ToList().ForEach(cell => { 
            
                if(Promos.MatchPromo(cell.InnerText) != null)
                {
                    weekCells.Where(wc => wc.RowReference == cell.RowReference && wc.Reference != cell.Reference).ToList().ForEach(matiereCell =>
                    {
                        MatiereViewBinding matiereViewBinding = new MatiereViewBinding();
                        matiereViewBinding.Matiere = matiereCell.InnerText;
                        result.Add(matiereViewBinding);
                    });
                }
            
            
            
            });

            return result;

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
