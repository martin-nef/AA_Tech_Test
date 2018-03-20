using AA_Tech_Test;
using AA_Tech_Test.Types;
using AA_Tech_Test.Exceptions;
using AA_Tech_Test.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Tests
{
    [TestClass]
    public class ExcelToolsTests
    {
        
        string testFile1 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../test.xlsx"));
        string testFile2 = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../test2.xlsx"));

        [TestMethod]
        public void ReadTest()
        {
                        Spreadsheet spreadsheet = ExcelTools.ReadExcelFile(testFile2);
            if (spreadsheet.Data[0, 0] == "A1" &&
                spreadsheet.Data[0, 1] == "B1" &&
                spreadsheet.Data[1, 0] == "A2" &&
                spreadsheet.Data[1, 1] == "B2" &&
                spreadsheet.Data[2, 0] == "A3" &&
                spreadsheet.Data[2, 1] == "B3" &&
                spreadsheet.Data[3, 0] == "A4" &&
                spreadsheet.Data[3, 1] == "B4")
                return;

            throw new Exception("Data read doesn't match data on the test spreadsheet.");
        }

        [TestMethod]
        public void AppendTest()
        {
            File.Copy(testFile1, testFile2, true);
            Spreadsheet spreadsheet = ExcelTools.ReadExcelFile(testFile2);
            int code = ExcelTools.AppendToExcelFile("__TEST__", testFile2);
            switch (code)
            {
                case 0:
                    return;
                case 1:
                    throw new InvalidSpreadsheetFormatException();
                case 2:
                    throw new NoWriteAccessException();
            }
            if (spreadsheet == ExcelTools.ReadExcelFile(testFile2))
            {
                throw new NoWriteAccessException();
            }
            if (!ExcelTools.ReadExcelFile(testFile2).Data.ToString().Contains("__TEST__"))
            {
                throw new NoWriteAccessException();
            }
        }
    }
}
