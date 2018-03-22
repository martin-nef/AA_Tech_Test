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

            /// Initialise utilities

            // Creates a debug winform, so we can log anything that happens to it
            // using DebugTools.Log(string).
            DebugTools.Initialise(this);
            ExcelTools.Initialise(this);
        }

        private void Start_Load(object sender, EventArgs e)
        {
#if DEBUG
            // Show the debug log form if the program was built in debug build configuration.
            DebugTools.WinForm.Show();
#endif
        }

        /// <summary>
        /// Update the progress bar, reducing the step size every time so it never reaches 100.
        /// </summary>
        public void UpdateProgressBar()
        {
            progressBar.Value = 100 - (progressBar.Value / 10);
        }

        public void ResetProgressBar()
        {
            progressBar.Value = 0;
        }

        public void FillProgressBar()
        {
            progressBar.Value = 100;
        }

        private void fileSubmitButton_Click(object sender, EventArgs e)
        // TODO: make a public event handler for messages meant for the user,
        //       and replace any references to the user message box (errorLabel)
        //       by firing off an event instead.
        {
            Spreadsheet spreadsheet;
            FormData formData;

            // Make sure this can only be ran once (for neatness I guess)
            Button goButton = ((Button)sender);
            goButton.Enabled = false;

            // Make sure the progress bar is at 0 to start with.
            ResetProgressBar();

            // Get textbox value with the .xlsx file path
            string filePath = filePathTextBox.Text;

            // Check that the file exists.
            if (!File.Exists(filePath))
            {
                UserMessageBox.Text = UserMessageBox.Text + "Could not find the file specified.\r\n";
                goButton.Enabled = true;
                return;
            }
            
            /// Read xlsx file into a FormData object
            try
            {
                spreadsheet = ExcelTools.ReadExcelFile(filePath);
            }
            catch (FileFormatException)
            {
                string tempMessage = "The selected file is either corrupt, or not a valid Microsoft Excel document.\r\n";
                UserMessageBox.Text += tempMessage;
                    DebugTools.Log(tempMessage);
                goButton.Enabled = true;
                return;
            }
            formData = ExcelTools.SpreadsheetToFormData(spreadsheet);

            /// Submit the form with data read, saving the reference
            var reference = WebFormTools.PostFormDataAsync(formData);

            /// Wait for the form submission to finish, updating the loading bar in the meantime,
            /// but let the user know if something happens during submission / reference retreival.
            while (!reference.IsCompleted)
            {
                if (reference.IsCanceled)
                {
                    string tempMessage = "Form submission cancelled.\r\n";
                    UserMessageBox.Text += tempMessage;
                    DebugTools.Log(tempMessage);
                    goButton.Enabled = true;
                    return;
                }
                if (reference.IsFaulted)
                {
                    string tempMessage = "Form submission failed due to an unknown error.\r\n";
                    UserMessageBox.Text += tempMessage;
                    DebugTools.Log(tempMessage);
                    goButton.Enabled = true;
                    return;
                }

                // Update the progress bar.
                UpdateProgressBar();

                // Make sure that the form doesn't hang while waiting.
                Refresh();
                Application.DoEvents();
            }

            // Change the progress bar to 100% and change the go button to show that we are done
            FillProgressBar();
            goButton.Enabled = true;

            int errorCode = 0;
            string mockReference = "MockReference - 12345678";
            foreach (int attempt in new[] { 1, 2 })
            {
                try
                {
                    if (attempt == 1)
                    {
                        errorCode = ExcelTools.AppendToExcelFile(reference.GetAwaiter().GetResult(), filePath);
                        break; // no need to try again for attempt no. 2
                    }
                    else
                    {
                        errorCode = ExcelTools.AppendToExcelFile(mockReference, filePath);
                        break;
                    }
                }
                catch (InvalidReferenceException error)
                {
                    string errorMessage = $"{error.Message}\r\nFor demo purposes a mock reference was used: {mockReference}\r\n";
                    UserMessageBox.Text += errorMessage;
                    DebugTools.Log(errorMessage);
                }
                catch (Exception error)
                {
                    string tempMessage = $"An error has occured while saving the submission reference. Additional Info:\r\n{error.Message}\r\n";
                    UserMessageBox.Text += tempMessage;
                    DebugTools.Log(tempMessage);
                }
            }

            switch (errorCode)
            {
                case 0:
                    break;
                case 1:
                    string errorMessage = "File specified was empty.\r\n";
                    UserMessageBox.Text += errorMessage;
                    DebugTools.Log(errorMessage);
                    goButton.Enabled = true;
                    return;
                case 2:
                    errorMessage = "Failed to get write permissions to the file.\r\n";
                    UserMessageBox.Text += errorMessage;
                    DebugTools.Log(errorMessage);
                    goButton.Enabled = true;
                    break;
            }

            goButton.Enabled = true;
            ExcelTools.DisplatResults(filePath, this);
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            filePathTextBox.Text = openFileDialog.FileName;
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void userMessageBox_TextChanged(object sender, EventArgs e)
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
