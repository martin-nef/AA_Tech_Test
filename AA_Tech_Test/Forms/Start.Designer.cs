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
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.fileSubmitButton = new System.Windows.Forms.Button();
            this.instructionsLabel = new System.Windows.Forms.Label();
            this.CancelButton_ = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.browseButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.userOutputDisplayPanel = new System.Windows.Forms.Panel();
            this.UserMessageBox = new System.Windows.Forms.TextBox();
            this.autoScrollCheckBox = new System.Windows.Forms.CheckBox();
            this.fileInputLabel = new System.Windows.Forms.Label();
            this.userOutputDisplayPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filePathTextBox.Location = new System.Drawing.Point(15, 165);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(331, 21);
            this.filePathTextBox.TabIndex = 1;
            this.filePathTextBox.Text = "C:\\Users\\Martin\\Desktop\\";
            // 
            // fileSubmitButton
            // 
            this.fileSubmitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fileSubmitButton.Location = new System.Drawing.Point(297, 200);
            this.fileSubmitButton.Name = "fileSubmitButton";
            this.fileSubmitButton.Size = new System.Drawing.Size(75, 25);
            this.fileSubmitButton.TabIndex = 2;
            this.fileSubmitButton.Text = "Submit";
            this.fileSubmitButton.UseVisualStyleBackColor = true;
            this.fileSubmitButton.Click += new System.EventHandler(this.fileSubmitButton_Click);
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
            this.CancelButton_.AccessibleName = "";
            this.CancelButton_.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton_.Location = new System.Drawing.Point(216, 201);
            this.CancelButton_.Name = "cancelButton";
            this.CancelButton_.Size = new System.Drawing.Size(75, 23);
            this.CancelButton_.TabIndex = 5;
            this.CancelButton_.Text = "Cancel";
            this.CancelButton_.UseVisualStyleBackColor = true;
            this.CancelButton_.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog.DefaultExt = "xlsx";
            this.openFileDialog.Filter = "Excel file|*.xlsx";
            this.openFileDialog.Title = "Please select your excel file.";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(349, 164);
            this.browseButton.Margin = new System.Windows.Forms.Padding(0);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(23, 23);
            this.browseButton.TabIndex = 7;
            this.browseButton.Text = "..";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar.Location = new System.Drawing.Point(15, 201);
            this.progressBar.Name = "progressBar1";
            this.progressBar.Size = new System.Drawing.Size(195, 23);
            this.progressBar.Step = 25;
            this.progressBar.TabIndex = 11;
            // 
            // errorDisplayPanel
            // 
            this.userOutputDisplayPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userOutputDisplayPanel.AutoScroll = true;
            this.userOutputDisplayPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.userOutputDisplayPanel.CausesValidation = false;
            this.userOutputDisplayPanel.Controls.Add(this.UserMessageBox);
            this.userOutputDisplayPanel.Location = new System.Drawing.Point(12, 67);
            this.userOutputDisplayPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.userOutputDisplayPanel.MinimumSize = new System.Drawing.Size(80, 16);
            this.userOutputDisplayPanel.Name = "errorDisplayPanel";
            this.userOutputDisplayPanel.Size = new System.Drawing.Size(360, 69);
            this.userOutputDisplayPanel.TabIndex = 12;
            // 
            // userMessageBox
            // 
            this.UserMessageBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.UserMessageBox.CausesValidation = false;
            this.UserMessageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserMessageBox.ForeColor = System.Drawing.Color.Black;
            this.UserMessageBox.Location = new System.Drawing.Point(0, 0);
            this.UserMessageBox.MinimumSize = new System.Drawing.Size(70, 18);
            this.UserMessageBox.Multiline = true;
            this.UserMessageBox.Name = "userMessageBox";
            this.UserMessageBox.ReadOnly = true;
            this.UserMessageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UserMessageBox.ShortcutsEnabled = false;
            this.UserMessageBox.Size = new System.Drawing.Size(360, 69);
            this.UserMessageBox.TabIndex = 13;
            this.UserMessageBox.WordWrap = false;
            this.UserMessageBox.TextChanged += new System.EventHandler(this.userMessageBox_TextChanged);
            // 
            // autoScrollCheckBox
            // 
            this.autoScrollCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autoScrollCheckBox.AutoSize = true;
            this.autoScrollCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.autoScrollCheckBox.Checked = true;
            this.autoScrollCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoScrollCheckBox.Location = new System.Drawing.Point(289, 139);
            this.autoScrollCheckBox.Name = "autoScrollCheckBox";
            this.autoScrollCheckBox.Size = new System.Drawing.Size(81, 19);
            this.autoScrollCheckBox.TabIndex = 14;
            this.autoScrollCheckBox.Text = "AutoScroll";
            this.autoScrollCheckBox.UseVisualStyleBackColor = true;
            this.autoScrollCheckBox.CheckedChanged += new System.EventHandler(this.ToggleAutoScroll);
            // 
            // fileInputLabel
            // 
            this.fileInputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fileInputLabel.Location = new System.Drawing.Point(12, 144);
            this.fileInputLabel.Margin = new System.Windows.Forms.Padding(3);
            this.fileInputLabel.Name = "fileInputLabel";
            this.fileInputLabel.Size = new System.Drawing.Size(165, 15);
            this.fileInputLabel.TabIndex = 15;
            this.fileInputLabel.Text = "Enter your Excel file lcoation: ";
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 237);
            this.Controls.Add(this.autoScrollCheckBox);
            this.Controls.Add(this.fileInputLabel);
            this.Controls.Add(this.userOutputDisplayPanel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.CancelButton_);
            this.Controls.Add(this.instructionsLabel);
            this.Controls.Add(this.fileSubmitButton);
            this.Controls.Add(this.filePathTextBox);
            this.Font = new System.Drawing.Font("Arial", 8.5F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimumSize = new System.Drawing.Size(400, 243);
            this.Name = "Start";
            this.Text = "File Input";
            this.Load += new System.EventHandler(this.Start_Load);
            this.userOutputDisplayPanel.ResumeLayout(false);
            this.userOutputDisplayPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox filePathTextBox;
        private Button fileSubmitButton;
        private Label instructionsLabel;
        public Button CancelButton_;
        private OpenFileDialog openFileDialog;
        private Button browseButton;
        private ProgressBar progressBar;
        private Panel userOutputDisplayPanel;
        private Label fileInputLabel;
        public TextBox UserMessageBox;
        private CheckBox autoScrollCheckBox;
    }
}

