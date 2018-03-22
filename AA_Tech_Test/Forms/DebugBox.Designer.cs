namespace AA_Tech_Test
{
    partial class DebugBox
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
            this.DebugOutputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // debugOutput
            // 
            this.DebugOutputRichTextBox.CausesValidation = false;
            this.DebugOutputRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DebugOutputRichTextBox.Location = new System.Drawing.Point(12, 12);
            this.DebugOutputRichTextBox.Name = "debugOutput";
            this.DebugOutputRichTextBox.ReadOnly = true;
            this.DebugOutputRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.DebugOutputRichTextBox.Size = new System.Drawing.Size(260, 237);
            this.DebugOutputRichTextBox.TabIndex = 0;
            this.DebugOutputRichTextBox.Text = "";
            this.DebugOutputRichTextBox.TextChanged += new System.EventHandler(this.debugOutput_TextChanged);
            this.DebugOutputRichTextBox.Leave += new System.EventHandler(this.debugOutput_Leave);
            this.DebugOutputRichTextBox.MouseLeave += new System.EventHandler(this.debugOutput_Leave);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.DebugOutputRichTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(12);
            this.panel1.Size = new System.Drawing.Size(284, 261);
            this.panel1.TabIndex = 1;
            // 
            // DebugBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.panel1);
            this.Name = "DebugBox";
            this.Text = "Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugBox_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox DebugOutputRichTextBox;
        private System.Windows.Forms.Panel panel1;
    }
}