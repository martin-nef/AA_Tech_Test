using AA_Tech_Test;
using AA_Tech_Test.Exceptions;
using AA_Tech_Test.Types;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AA_Tech_Test.Utilities
{
    public class ExcelTools
    {
        /// <summary>
        /// Reads a special Excel spreadsheet document and produces a FormData object, 
        /// used to fill a form at https://agileautomations.co.uk/home/inputform.
        /// 
        /// The spreadsheet must have 4 rows with the first cell of each row
        /// containing a contact's name, email, subject and a message - in that order.
        /// 
        /// The spreadsheet will be updated with the reference returned by the form,
        /// which will be appended to the bottom of the spreadsheet.
        /// 
        /// This only uses the first spreadsheet in the document.
        /// </summary>
        /// <param name="path">Path to the Excel spreadsheet.</param>
        /// <returns></returns>
        public static Spreadsheet ReadExcelFile(string path)
        {
            // Make sure that we have the fuill path, just in case
            path = Path.GetFullPath(path);

            // Initialise the return variable
            Spreadsheet spreadsheet;

            // Open the Excel document in read-only mode
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, false))
            {
                // Open and read the first spreadsheet in the document
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Elements<Row>();
                int rowCount = rows.Count();
                int colCount = rows.First().Elements<Cell>().Count();
                spreadsheet = new Spreadsheet(rowCount, colCount);

                if (rowCount < 4)
                {
                    // Since we assume there are at least 4 rows, continuing would cause an error.
                    throw new InvalidSpreadsheetFormatException();
                }

                // Get the values from the first cell of the first four rows
                spreadsheet.Data[0, 0] = GetCellValue(path, "Sheet1", "A1");
                spreadsheet.Data[1, 0] = GetCellValue(path, "Sheet1", "A2");
                spreadsheet.Data[2, 0] = GetCellValue(path, "Sheet1", "A3");
                spreadsheet.Data[3, 0] = GetCellValue(path, "Sheet1", "A4");
            }

            return spreadsheet;
        }

        public static FormData SpreadsheetToFormData(Spreadsheet spreadsheet)
        {
            FormData data = new FormData
            {
                ContactName = spreadsheet.Data[0, 0],
                ContactEmail = spreadsheet.Data[1, 0],
                ContactSubject = spreadsheet.Data[2, 0],
                Message = spreadsheet.Data[3, 0]
            };
            return data;
        }

        /// <summary>
        /// Appends row to the bottom of an MS Excel spreadsheet, with a specified value in its first cell.
        /// </summary>
        /// <param name="value">Velue to be written to the spreadsheet.</param>
        /// <param name="path">Excel spreadsheet document file path.</param>
        /// <returns>
        /// 0 - normal exit
        /// 1 - .xlsx document empty
        /// 2 - failed to get write permissions
        /// </returns>
        public static int AppendToExcelFile(string value, string path)
        {
            // Open Excel document in R/W mode
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, true, new OpenSettings { AutoSave = true, MaxCharactersInPart = 0 }))
            {
                if (spreadsheetDocument.FileOpenAccess != FileAccess.ReadWrite ||
                    spreadsheetDocument.FileOpenAccess != FileAccess.Write)
                {
                    DebugTools.Log($"Couldn't get write permissions.");
                    return 2;
                    throw new NoWriteAccessException();
                }

                // Open and read the first spreadsheet in the document
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                IEnumerable<Row> rows = sheetData.Elements<Row>();
                // Get the last row
                Row lastRow = rows.Last();

                while (rows.Count() > 0)
                {
                    if (lastRow.Elements<Cell>().Any(c => c != null || c.CellValue != null))
                        break;

                    lastRow.Remove();
                    lastRow = rows.Last();
                    DebugTools.Log("Successfully removed blank line before appending reference.");
                }

                // If last row is null, notify that doc is empty.
                if (lastRow == null)
                    return 1;

                // Append a new row with the desired value in its first cell to the bottom of the spreadsheet
                // Append the value to the first cell of the new row at the bottom of the spreadsheet.
                Row newRow = new Row() { RowIndex = lastRow.RowIndex + 1 };
                sheetData.Append(newRow);
                if (newRow.Elements<Cell>().Any())
                    newRow.RemoveChild(newRow.Elements<Cell>().First());
                newRow.PrependChild(new Cell() { CellValue = new CellValue(value) });
                spreadsheetDocument.Save();
                worksheetPart.Worksheet.Save();
                workbookPart.Workbook.Save();
            }

            return 0;
        }

        /// <summary>
        /// Retrieve the value of a cell, given a file name, sheet name, 
        /// and address name.
        /// Method from Microsoft <see href="https://msdn.microsoft.com/en-us/library/office/hh298534.aspx#Anchor_5">docs</see>.
        /// </summary>
        public static string GetCellValue(string fileName,
            string sheetName,
            string addressName)
        {
            string value = null;

            // Open the spreadsheet document for read-only access.
            using (SpreadsheetDocument document =
                SpreadsheetDocument.Open(fileName, false))
            {
                // Retrieve a reference to the workbook part.
                WorkbookPart wbPart = document.WorkbookPart;

                // Find the sheet with the supplied name, and then use that 
                // Sheet object to retrieve a reference to the first worksheet.
                Sheet theSheet = wbPart.Workbook.Descendants<Sheet>().
                  Where(s => s.Name == sheetName).FirstOrDefault();

                // Throw an exception if there is no sheet.
                if (theSheet == null)
                {
                    throw new ArgumentException("sheetName");
                }

                // Retrieve a reference to the worksheet part.
                WorksheetPart wsPart =
                    (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

                // Use its Worksheet property to get a reference to the cell 
                // whose address matches the address you supplied.
                Cell theCell = wsPart.Worksheet.Descendants<Cell>().
                  Where(c => c.CellReference == addressName).FirstOrDefault();

                // If the cell does not exist, return an empty string.
                if (theCell != null)
                {
                    value = theCell.InnerText;

                    // If the cell represents an integer number, you are done. 
                    // For dates, this code returns the serialized value that 
                    // represents the date. The code handles strings and 
                    // Booleans individually. For shared strings, the code 
                    // looks up the corresponding value in the shared string 
                    // table. For Booleans, the code converts the value into 
                    // the words TRUE or FALSE.
                    if (theCell.DataType != null)
                    {
                        switch (theCell.DataType.Value)
                        {
                            case CellValues.SharedString:

                                // For shared strings, look up the value in the
                                // shared strings table.
                                var stringTable =
                                    wbPart.GetPartsOfType<SharedStringTablePart>()
                                    .FirstOrDefault();

                                // If the shared string table is missing, something 
                                // is wrong. Return the index that is in
                                // the cell. Otherwise, look up the correct text in 
                                // the table.
                                if (stringTable != null)
                                {
                                    value =
                                        stringTable.SharedStringTable
                                        .ElementAt(int.Parse(value)).InnerText;
                                }
                                break;

                            case CellValues.Boolean:
                                switch (value)
                                {
                                    case "0":
                                        value = "FALSE";
                                        break;
                                    default:
                                        value = "TRUE";
                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            return value;
        }
    }
}
