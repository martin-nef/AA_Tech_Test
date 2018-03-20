using AA_Tech_Test.Types;
using AA_Tech_Test.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AA_Tech_Test
{
    public partial class ResultDisplay : Form
    {
        string SpreadsheetPath;
        Spreadsheet spreadsheet;
        Form Root;
        public ResultDisplay(string path, Form root)
        {
            InitializeComponent();
            Root = root;
            SpreadsheetPath = path;
            Text = Path.GetFileName(path);

            // Read the spreadsheet document file
            spreadsheet = ExcelTools.ReadExcelFile(path);
            if (resultTable.RowCount < spreadsheet.RowCount)
            {
                resultTable.RowCount = spreadsheet.RowCount;
            }
            if (resultTable.ColumnCount < spreadsheet.ColumnCount)
            {
                resultTable.ColumnCount = spreadsheet.ColumnCount;
            }

            // Display the spreadsheet
            int row;
            int col;
            for (row = 0; row < spreadsheet.RowCount; row++)
            {
                for (col = 0; col < spreadsheet.ColumnCount; col++)
                {
                    // Create a new label with some text at position row, col.
                    Label newLabel = new Label();
                    newLabel.Text = spreadsheet.Data[row, col];
                    resultTable.Controls.Add(newLabel, col, row);
                }
            }
        }

        private void ResultDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Root != null)
            {
                Root.Show();
            }
            Dispose();
        }
    }
}
