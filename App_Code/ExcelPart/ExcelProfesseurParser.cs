﻿using Excel;
using Excel.UI;
using Json;
using JsonPart;
using JsonPart.Records;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelPart
{
    public class ExcelProfesseurParser
    {

        private List<CellViewBinding> _cells = new List<CellViewBinding>();

        private List<Professeur> _professeurs = new List<Professeur>();

        public ExcelProfesseurParser(List<CellViewBinding> cells)
        {
            this._cells = cells;
        }


        private void SaveProfesseurs() { 


            int rowNumber = _cells.OrderByDescending(cell => cell.RowReference).FirstOrDefault()?.RowReference ?? 0;
            for(int i = 1; i < rowNumber; i++)
            {
                List<CellViewBinding> rowCells = _cells.Where(cell => cell.RowReference == i).ToList();
                string civilite = "";
                string nom = "";
                string prenom = "";
                string email = "";
                rowCells?.ForEach(rowCell =>
                {

                    switch (rowCell.ColumnReference)
                    {
                        case "A":
                            civilite = rowCell.InnerText;
                            break;

                        case "B":
                            nom = rowCell.InnerText;
                            break;

                        case "C":
                            prenom = rowCell.InnerText;
                            break;

                        case "D":
                            email = rowCell.InnerText;
                            break;
                    }

                });

                _professeurs.Add(new Professeur(civilite, nom, prenom, email));
            }

            _professeurs.UpdateJsonProfesseurs();
        }
    }

}
