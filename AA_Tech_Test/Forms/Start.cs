using AA_Tech_Test.Exceptions;
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
        private bool autoScroll = true;

        public Start()
        {
            InitializeComponent();
            new DebugTools(this);
        }

        private void Start_Load(object sender, EventArgs e)
        {
#if DEBUG
            errorLabel.Text = "1\r\n2\r\n3\r\n4\r\n5\r\n";
            DebugTools.Form.Show();
            Focus();
#endif
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click_2(object sender, EventArgs e)
        {
            Spreadsheet spreadsheet;
            FormData formData;

            // Get textbox value with the .xlsx file path
            string filePath = filePathTextBox.Text;

            if (!File.Exists(filePath))
            {
                errorLabel.Text = errorLabel.Text + "Could not find the file specified.\r\n";
                return;
            }

            // Validate file path, and that it's a valid excel file,
            // or disable typing and let the file open dialog to handle it.

            // Read xlsx file into a FormData object
            try
            {
                spreadsheet = ExcelTools.ReadExcelFile(filePath);
            }
            catch (FileFormatException error)
            {
                errorLabel.Text += "The selected file is either corrupt, or not a valid Microsoft Excel document.\r\n";
                return;
            }
            formData = ExcelTools.SpreadsheetToFormData(spreadsheet);

            // Send data, save the reference
            var reference = WebFormTools.PostFormDataAsync(formData);

            int code = 0;
            try
            {
                code = ExcelTools.AppendToExcelFile(await reference, filePath);
                outputBox.Text += ((await reference).Length > 0 && (await reference).Length < 40) ? $"reference: {(await reference)}" : string.Empty;
            }
            catch (Exception error)
            {
                errorLabel.Text += $"An error has occured while saving the submission reference. Additional Info:\r\n{error.Message}\r\n";
            }

            while (!reference.IsCompleted)
            {
                if (reference.IsCanceled)
                {
                    errorLabel.Text += "Form submission cancelled.\r\n";
                    return;
                }
                if (reference.IsFaulted)
                {
                    errorLabel.Text += "Form submission failed due to an unknown error.\r\n";
                    return;
                }
                switch (reference.Status)
                {
                    case TaskStatus.Canceled:
                        errorLabel.Text += "Form submission cancelled.\r\n" + errorLabel.Text;
                        return;
                    case TaskStatus.Faulted:
                        errorLabel.Text += "Form submission failed due to an unknown error.\r\n";
                        return;
                    case TaskStatus.Created:
                    case TaskStatus.RanToCompletion:
                    case TaskStatus.Running:
                    case TaskStatus.WaitingForActivation:
                    case TaskStatus.WaitingForChildrenToComplete:
                    case TaskStatus.WaitingToRun:
                        break;
                    default:
                        throw new UnknownException("Form submission failed");
                }
                progressBar1.PerformStep();
                progressBar1.Step = (int)(3.00 * progressBar1.Step / 4.00);
                System.Threading.Thread.Sleep(100);
            }

            switch (code)
            {
                case 0:
                    break;
                case 1:
                    errorLabel.Text += "File specified was empty.\r\n";
                    return;
                case 2:
                    errorLabel.Text += "Failed to get write permissions to the file.\r\n";
                    return;
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

        private void errorLabel_TextChanged(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(TextBox))
            {
                throw new TypeMismatchException(expected: typeof(TextBox), got: sender.GetType());
            }

            TextBox errorTextBox = ((TextBox)sender);
            if (errorTextBox.Text == string.Empty)
            {
                errorTextBox.Parent.Visible = false;
            }
            else
            {
                errorTextBox.Parent.Visible = true;
            }
            
            if (autoScroll)
            {
                errorTextBox.SelectionStart = errorTextBox.Text.Length;
                errorTextBox.ScrollToCaret();
            }
        }

        public void ToggleAutoScroll(object sender, EventArgs e)
        {
            autoScroll = !autoScroll;
        }
    }
}
