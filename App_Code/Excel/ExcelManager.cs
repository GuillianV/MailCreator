using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Excel.DataView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                this._worksheetPart = this._workbookPart.WorksheetParts.Last();
                this._sheetData = this._worksheetPart.Worksheet.Elements<SheetData>().Last();
                this._excelLoaded = true;
                
            }catch(Exception e)
            {
                throw new Exception("Excel file cannot be load , Exception : "+e.Message);
            }
       
        }

        public List<CellViewBinding> ShowCellsValues()
        {
            List<CellViewBinding> cellViewBindings = new List<CellViewBinding>();

            if (!this.IsFileLoaded())   
                return cellViewBindings;

            int byteColumRangeExceed = Convert.ToInt32(Encoding.ASCII.GetBytes("Q").First());

            foreach (Row row in this._sheetData.Elements<Row>())
            {

                foreach (Cell cell in row.Elements<Cell>())
                {
                    string value = GetCellInnerText(cell);

                    bool matchColumn = true;
                    string columRef = default(string);
                    int rowRef = default(int);
                    Match match = Regex.Match(cell.CellReference.Value, @"([A-Z]+)(\d+)");
                    if (match.Success)
                    {
                        int bytepoids = 0;
                        columRef = match.Groups[1].Value;
                        rowRef = int.Parse(match.Groups[2].Value);
                        byte[] bytes = Encoding.ASCII.GetBytes(columRef);
                        foreach (byte b in bytes)
                        {
                            bytepoids += b;
                        }

                        if(bytepoids > byteColumRangeExceed)
                        {
                            matchColumn = false;
                        }

                    }


                    if (!string.IsNullOrEmpty(value) && matchColumn && columRef != default(string) && rowRef != default(int))
                        cellViewBindings.Add(new CellViewBinding(value, columRef,rowRef, cell.DataType ?? CellValues.Number));

                }
            }

            return cellViewBindings;

        }



        public string GetCellInnerText(Cell cell)
        {
            if (cell == null || cell.CellValue == null || cell.CellValue.InnerText == null)
                return "";

            string value = cell.CellValue.InnerText;

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
