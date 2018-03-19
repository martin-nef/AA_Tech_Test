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

namespace AA_Tech_Test
{
    public partial class Start
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileInputLabel = new System.Windows.Forms.Label();
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.fileSubmitButton = new System.Windows.Forms.Button();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.hiddenButton = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // fileInputLabel
            // 
            this.fileInputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fileInputLabel.AutoSize = true;
            this.fileInputLabel.Location = new System.Drawing.Point(12, 114);
            this.fileInputLabel.Name = "fileInputLabel";
            this.fileInputLabel.Size = new System.Drawing.Size(165, 15);
            this.fileInputLabel.TabIndex = 0;
            this.fileInputLabel.Text = "Enter your Excel file lcoation: ";
            this.fileInputLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filePathTextBox.Location = new System.Drawing.Point(15, 132);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(331, 21);
            this.filePathTextBox.TabIndex = 1;
            this.filePathTextBox.Text = "C:\\Users\\Martin\\Desktop\\";
            this.filePathTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // fileSubmitButton
            // 
            this.fileSubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSubmitButton.Location = new System.Drawing.Point(297, 167);
            this.fileSubmitButton.Name = "fileSubmitButton";
            this.fileSubmitButton.Size = new System.Drawing.Size(75, 25);
            this.fileSubmitButton.TabIndex = 2;
            this.fileSubmitButton.Text = "Submit";
            this.fileSubmitButton.UseVisualStyleBackColor = true;
            this.fileSubmitButton.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // instructionsLabel
            // 
            this.instructionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instructionsLabel.AutoEllipsis = true;
            this.instructionsLabel.Location = new System.Drawing.Point(12, 9);
            this.instructionsLabel.Name = "instructionsLabel";
            this.instructionsLabel.Size = new System.Drawing.Size(360, 34);
            this.instructionsLabel.TabIndex = 4;
            this.instructionsLabel.Text = "Please provide an Excel file with a name, an email, a subject and a message, resp" +
    "ectively, each on a separate line.\r\n";
            this.instructionsLabel.UseMnemonic = false;
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleName = "";
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(216, 168);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "xlsx";
            this.openFileDialog1.Filter = "Excel file|*.xlsx";
            this.openFileDialog1.Title = "Please select your excel file.";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Maroon;
            this.errorLabel.Location = new System.Drawing.Point(12, 96);
            this.errorLabel.MinimumSize = new System.Drawing.Size(70, 18);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(70, 18);
            this.errorLabel.TabIndex = 6;
            this.errorLabel.Click += new System.EventHandler(this.errorLabel_Click);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(349, 131);
            this.browseButton.Margin = new System.Windows.Forms.Padding(0);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(23, 23);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "..";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // outputBox
            // 
            this.outputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputBox.CausesValidation = false;
            this.outputBox.Location = new System.Drawing.Point(15, 63);
            this.outputBox.MinimumSize = new System.Drawing.Size(70, 18);
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(331, 18);
            this.outputBox.TabIndex = 9;
            this.outputBox.WordWrap = false;
            // 
            // hiddenButton
            // 
            this.hiddenButton.CausesValidation = false;
            this.hiddenButton.Location = new System.Drawing.Point(336, 33);
            this.hiddenButton.Name = "hiddenButton";
            this.hiddenButton.Size = new System.Drawing.Size(10, 10);
            this.hiddenButton.TabIndex = 10;
            this.hiddenButton.DoubleClick += new System.EventHandler(this.hiddenButton_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 168);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(195, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 11;
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 204);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.hiddenButton);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.fileSubmitButton);
            this.Controls.Add(this.filePathTextBox);
            this.Controls.Add(this.fileInputLabel);
            this.Font = new System.Drawing.Font("Arial", 8.5F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimumSize = new System.Drawing.Size(400, 243);
            this.Name = "Start";
            this.Text = "File Input";
            this.Load += new System.EventHandler(this.Start_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label fileInputLabel;
        private TextBox filePathTextBox;
        private Button fileSubmitButton;
        private Label instructionsLabel;
        public Button cancelButton;
        private OpenFileDialog openFileDialog1;
        private Button browseButton;
        private Label hiddenButton;
        public Label errorLabel;
        public TextBox outputBox;
        private ProgressBar progressBar1;
    }
}

