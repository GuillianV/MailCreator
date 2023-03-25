using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
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
        
        public String filePath { get; private set; }
        public FileInfo fileInfo { get; private set; }
        public ExcelManager(FileInfo _fileInfo)
        {
            this.fileInfo = _fileInfo;
            this.filePath = this.fileInfo.FullName;

        }



        public void ReadExcelFile()
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(this.filePath, true))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                foreach (Row row in sheetData.Elements<Row>())
                {

                    foreach(Column column in sheetData.Elements<Column>())
                    {

                    }


                    foreach (Cell cell in row.Elements<Cell>())
                    {

                        string value = "";

                        if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                        {
                            int sharedStringId = int.Parse(cell.CellValue.Text);
                            SharedStringItem sharedStringItem = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(sharedStringId);
                            value = sharedStringItem.InnerText;
                        }
                        else
                            value = cell.CellValue.InnerText;


                        Console.Write(value);

                    }
                    Console.WriteLine();
                }
            }
        }



    }

}
