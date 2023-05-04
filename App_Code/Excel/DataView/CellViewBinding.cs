using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Excel.DataView
{
    public class CellViewBinding
    {

        public String InnerText {get; private set ;}
        public String Reference { get; private set; }
        public CellValues CellType { get; private set; }

        public int RowReference { get; private set; }
        public String ColumnReference { get; private set; }


        public CellViewBinding(String _innerText,string _columnReference, int _rowReference,CellValues _cellType)
        {
            this.InnerText = _innerText;
            this.Reference = _columnReference+_rowReference.ToString();
            this.CellType =_cellType;
            this.ColumnReference = _columnReference;
            this.RowReference = _rowReference;


        }

    }
}
