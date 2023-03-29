using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Excel.UI
{
    public class CellViewBinding
    {

        public String InnerText {get; private set ;}
        public String Reference { get; private set; }
        public CellValues CellType { get; private set; }

        public int RowReference { get; private set; }
        public String ColumnReference { get; private set; }


        public CellViewBinding(String _innerText, String _reference,CellValues _cellType)
        {
            this.InnerText = _innerText;
            this.Reference = _reference;
            this.CellType =_cellType;

            Match match = Regex.Match(this.Reference, @"([A-Z]+)(\d+)");
            if (match.Success)
            {
                this.ColumnReference = match.Groups[1].Value;
                this.RowReference = int.Parse(match.Groups[2].Value);
            }


        }

    }
}
