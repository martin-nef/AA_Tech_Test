using AA_Tech_Test.Types;
using AA_Tech_Test.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace AA_Tech_Test
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            new DebugTools(this);
        }

        private void Start_Load(object sender, EventArgs e)
        {
#if DEBUG
            DebugTools.Form.Show();
            Focus();
#endif
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click_2(object sender, EventArgs e)
        {

            // Get textbox value with the .xlsx file path
            string filePath = filePathTextBox.Text;

            if (!File.Exists(filePath))
            {
                errorLabel.Text = "Could not find the file specified.";
                return;
            }

            // Validate file path, and that it's a valid excel file,
            // or disable typing and let the file open dialog to handle it.

            // Read xlsx file into a FormData object
            Spreadsheet spreadsheet = ExcelTools.ReadExcelFile(filePath);
            FormData formData = ExcelTools.SpreadsheetToFormData(spreadsheet);

            // Send data, save the reference
            var reference = WebFormTools.PostFormDataAsync(formData);
            //reference = WebFormTools.PostFormData(formData);

            int code = ExcelTools.AppendToExcelFile(await reference, filePath);
            while (!reference.IsCompleted)
            {
                if (reference.IsCanceled)
                {
                    errorLabel.Text = "Form post cancelled.";
                    return;
                }
                if (reference.IsFaulted)
                {
                    errorLabel.Text = "Form post failed due to an unknown error.";
                    return;
                }
                progressBar1.PerformStep();
                progressBar1.Value = progressBar1.Value % progressBar1.Maximum;
                System.Threading.Thread.Sleep(100);
            }
            string result = reference.GetAwaiter().GetResult();
            outputBox.Text = (result.Length > 0 && result.Length < 30) ? $"reference: {result}" : string.Empty;
            switch (code)
            {
                case 0:
                    break;
                case 1:
                    errorLabel.Text = "File specified was empty.";
                    break;
                case 2:
                    errorLabel.Text = "Failed to get write permissions to the file.";
                    break;
            }

            new ResultDisplay(filePath, this).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Quit the application
            Environment.Exit(0);
        }

        private void fileExtensionLabel_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            filePathTextBox.Text = openFileDialog1.FileName;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void outputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void hiddenButton_DoubleClick(object sender, EventArgs e)
        {
            if (DebugTools.Form.Visible == false)
            {
                DebugTools.Form.Show();
            }
            else
            {
                DebugTools.Form.Hide();
            }
        }

        private void errorLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
