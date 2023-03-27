using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel
{
    class ExcelManager
    {
        
        public String FilePath { get; private set; }
        public FileInfo FileInfo { get; private set; }

        private SpreadsheetDocument _document;

        private WorkbookPart _workbookPart;

        private WorksheetPart _worksheetPart;

        private SheetData _sheetData;

        private bool _excelLoaded = false;

        public ExcelManager(FileInfo _fileInfo)
        {
            this.FileInfo = _fileInfo;
            this.FilePath = this.FileInfo.FullName;
            this.LoadExcelFile();
        }

        public bool IsFileLoaded()
        {
            return _excelLoaded;
        }

        private void LoadExcelFile()
        {
            try
            {
                this._document = SpreadsheetDocument.Open(this.FilePath, true);
                this._workbookPart = this._document.WorkbookPart;
                this._worksheetPart = this._workbookPart.WorksheetParts.First();
                this._sheetData = this._worksheetPart.Worksheet.Elements<SheetData>().First();
                this._excelLoaded = true;
                
            }catch(Exception e)
            {
                throw new Exception($"Excel file cannot be load , Exception : {e.Message}");
            }
       
        }

        public List<CellViewBinding> ShowCellsValues()
        {
            List<CellViewBinding> cellViewBindings = new List<CellViewBinding>();

            if (!this.IsFileLoaded())
                return cellViewBindings;

            foreach (Row row in this._sheetData.Elements<Row>())
            {

                foreach (Cell cell in row.Elements<Cell>())
                {
                    string value = GetCellInnerText(cell);
                   
                    if(!string.IsNullOrEmpty(value))
                        cellViewBindings.Add(new CellViewBinding(value, cell.CellReference, cell.DataType ?? CellValues.Number));

                }
            }

            return cellViewBindings;

        }



        public string GetCellInnerText(Cell cell)
        {

            string value = cell.CellValue?.InnerText;

            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                int sharedStringId = int.Parse(cell.CellValue.Text);
                SharedStringItem sharedStringItem = this._workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(sharedStringId);
                value = sharedStringItem.InnerText;
            }

            return value;

        }

    }

}
