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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AA_Tech_Test.Utilities
{
    /// <summary>
    /// Collection of tools, built to read excel files and to be able to appenda a string to one.
    /// Uses the first sheet in the specified document, ignores the rest.
    /// </summary>
    public static class ExcelTools
    // TODO: make this work with large excel files (with several workbook parts I think)
    {
        private static Start Parent;
        private static ResultDisplay WinForm;

        private static ResultDisplay GetWinForm(string filePath)
        {
            return new ResultDisplay(filePath, Parent);
        }

        public static void Initialise(Start root)
        {
            if (Parent == null)
            {
                Parent = root;
            }
        }

        // TODO: instead of entirely locking out writing while reading,
        //       have a dictionary with file pathes as keys and lock objects as values,
        //       so that there's a different lock for each file; 
        //       and make that dictionary only accessible synchronously.
        //       Check for potential deadlocks!
        private static object writeLock = new object();

        /// <summary>
        /// Blocks other I/O that this module does. Namely <see cref="ReadExcelFile">
        /// ReadExcelFile</see> and <see cref="AppendToExcelFile">AppendToExcelFile</see>
        /// This only uses the first spreadsheet in the document.
        /// </summary>
        /// <param name="path">Path to the Excel spreadsheet.</param>
        /// <returns></returns>
        public static Spreadsheet ReadExcelFile(string path, string sheetName = null)
        {
            // Make sure that we have the fuill path, just in case
            path = Path.GetFullPath(path);

            // Initialise the return variable
            Spreadsheet spreadsheet;

            // Make sure we can't write anything to a file while reading from one, 
            // since these methods are usually called on the same file.
            lock (writeLock)
            {
                // Open the Excel document in read-only mode
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, false))
                {
                    // Locals
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Elements<Row>();
                    int rowCount = rows.Count();
                    int colCount = rows.First().Elements<Cell>().Count();
                    int colIndex;
                    int rowIndex;
                    spreadsheet = new Spreadsheet(rowCount, colCount);

                    if (rowCount < 4)
                    {
                        // Since we assume there are at least 4 rows, continuing would cause an error.
                        throw new InvalidSpreadsheetFormatException();
                    }

                    for (rowIndex = 0; rowIndex < rowCount; rowIndex++)
                    {
                        for (colIndex = 0; colIndex < colCount; colIndex++)
                        {
                            spreadsheet.Data[rowIndex, colIndex] = GetCellValue(rowIndex, colIndex, spreadsheetDocument);

                            // This can take a while, so make sure that the form doesn't hang while waiting.
                            Parent.Update();
                            Parent.UpdateProgressBar();
                            Application.DoEvents();
                        }
                    }
                    Parent.FillProgressBar();
                }
            }
            return spreadsheet;
        }

        /// <summary>
        /// Get a name from A1 or (0,0); an email from A2 or (0,0); a subject from A3 or (0,0);
        /// and get a message from A4 or (0,0);
        /// </summary>
        /// <param name="spreadsheet"></param>
        /// <returns></returns>
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
        /// Based on GetCellValue method by Microsoft 
        /// <see href="https://msdn.microsoft.com/en-us/library/office/hh298534.aspx#Anchor_5">
        /// docs</see>.
        /// </summary>
        /// <param name="address">Example "A2", "C12"</param>
        /// <param name=""></param>
        /// <returns></returns>
        private static string GetCellValue(int rowIndex, int colIndex, SpreadsheetDocument document, string sheetName = null)
        {
            string value = null;

            // Retrieve a reference to the workbook part.
            WorkbookPart wbPart = document.WorkbookPart;

            Sheet theSheet;
            if (sheetName == null)
            {
                // Get the first sheet, and then use that Sheet object to retrieve a reference
                // to the first worksheet.
                theSheet = wbPart.Workbook.Descendants<Sheet>().FirstOrDefault();
            }
            else
            {
                // Find the sheet with the supplied name, and then use that Sheet object
                // to retrieve a reference to the first worksheet.
                theSheet = wbPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName)
                    .FirstOrDefault();
            }

            // Throw an exception if there is no sheet.
            if (theSheet == null)
            {
                throw new ArgumentException("There are no sheets in the selected Excel document.");
            }

            // Retrieve a reference to the worksheet part.
            WorksheetPart wsPart =
                (WorksheetPart)(wbPart.GetPartById(theSheet.Id));

            // Use its Worksheet property to get a reference to the cell 
            // whose address matches the address you supplied.
            Cell cell;
            try
            {
                cell = wsPart.Worksheet.Descendants<Row>().ElementAt(rowIndex).Descendants<Cell>().ElementAt(colIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                cell = null;
            }

            // If the cell does not exist, return an empty string.
            if (cell != null)
            {
                value = cell.InnerText;

                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that 
                // represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code 
                // looks up the corresponding value in the shared string 
                // table. For Booleans, the code converts the value into 
                // the words TRUE or FALSE.
                if (cell.DataType != null)
                {
                    switch (cell.DataType.Value)
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

            return value;
        }

        internal static void DisplatResults(string filePath, Form root)
        {
            if (WinForm != null)
            {
                WinForm.Dispose();
            }
            WinForm = GetWinForm(filePath);
            WinForm.Show();
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
            // Wait for all reading to be done.
            lock (writeLock)
            {
                // Open Excel document in R/W mode
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(path, true))
                {
                    if (spreadsheetDocument.FileOpenAccess != FileAccess.ReadWrite ||
                        spreadsheetDocument.FileOpenAccess != FileAccess.Write)
                    {
                        DebugTools.Log($"Couldn't get write permissions.");
                        //return 2;
                    }

                    // Open and read the first spreadsheet in the document
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    IEnumerable<Row> rows = sheetData.Elements<Row>();
                    // Get the last row
                    Row lastRow = rows.Last();

                    // Remove some empty rows at the bottom of the file, before appending anything.
                    while (rows.Count() > 0)
                    {
                        if (lastRow.Elements<Cell>().Any(c => c != null || c.CellValue != null))
                            break;

                        lastRow.Remove();
                        lastRow = rows.Last();
                        DebugTools.Log("Successfully removed a blank line from the end of file before appending the reference.");
                    }

                    // If last row is null, notify that doc is empty.
                    if (lastRow == null)
                        return 1;

                    // Append a new row with the desired value in its first cell to the bottom of the spreadsheet
                    // Append the value to the first cell of the new row at the bottom of the spreadsheet.
                    Row newRow = new Row() { RowIndex = lastRow.RowIndex + 1 };
                    if (newRow.Elements<Cell>().Any())
                        newRow.RemoveChild(newRow.Elements<Cell>().First());
                    newRow.PrependChild(new Cell() { CellValue = new CellValue(value) });
                    sheetData.Append(newRow);
                    spreadsheetDocument.Save();
                }
            }

            return 0;
        }
    }
}
