using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AA_Tech_Test.Types
{
    public class Spreadsheet
    {
        /// <summary>
        /// Data[<see cref="int">int</see> row, <see cref="int">int</see> column]
        /// </summary>
        public string[,] Data { get; set; }
        public int RowCount { get
            {
                return Data.GetLength(0);
            } }

        public int ColumnCount { get
            {
                return Data.GetLength(1);
            } }

        public Spreadsheet(int rows, int cols)
        {
            Data = new string[rows, cols];
        }

        public override string ToString()
        {
            string representation = string.Empty;
            int row;
            int col;

            representation += "col\t";
            for (col = 0; col < ColumnCount; col++)
            {
                representation += $"{col}\t";
            }
            representation += "\n";
            for (row = 0; row < RowCount; row++)
            {
                string rowPrefix = $"row {row}\t";
                representation += rowPrefix;
                for (col = 0; col < ColumnCount; col++)
                {
                    representation += $"{Data[row, col]}";
                }
                representation += "\n";
            }
            return representation;
        }

        public string[] Lines {
            get
            {
                string[] newLineChars = new string[] { "\r\n", "\n\r", "\n" };
                return ToString().Split(newLineChars, StringSplitOptions.None);
            }
        }
    }
}
