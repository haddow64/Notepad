namespace Notepad.Forms {
    partial class FindDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDialog));
            this.buttonFindNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelFind = new System.Windows.Forms.Label();
            this.checkboxMatchCase = new System.Windows.Forms.CheckBox();
            this.groupBoxUpDown = new System.Windows.Forms.GroupBox();
            this.radioButtonFindDown = new System.Windows.Forms.RadioButton();
            this.radioButtonFindUp = new System.Windows.Forms.RadioButton();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.groupBoxUpDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonFindNext
            // 
            this.buttonFindNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindNext.Location = new System.Drawing.Point(267, 12);
            this.buttonFindNext.Name = "buttonFindNext";
            this.buttonFindNext.Size = new System.Drawing.Size(75, 23);
            this.buttonFindNext.TabIndex = 4;
            this.buttonFindNext.Text = "&Find Next";
            this.buttonFindNext.UseVisualStyleBackColor = true;
            this.buttonFindNext.Click += new System.EventHandler(this.buttonFindNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(267, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelFind
            // 
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(8, 18);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(56, 13);
            this.labelFind.TabIndex = 0;
            this.labelFind.Text = "Fi&nd what:";
            // 
            // checkboxMatchCase
            // 
            this.checkboxMatchCase.AutoSize = true;
            this.checkboxMatchCase.Location = new System.Drawing.Point(11, 71);
            this.checkboxMatchCase.Name = "checkboxMatchCase";
            this.checkboxMatchCase.Size = new System.Drawing.Size(82, 17);
            this.checkboxMatchCase.TabIndex = 2;
            this.checkboxMatchCase.Text = "Match &case";
            this.checkboxMatchCase.UseVisualStyleBackColor = true;
            // 
            // groupBoxUpDown
            // 
            this.groupBoxUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUpDown.Controls.Add(this.radioButtonFindDown);
            this.groupBoxUpDown.Controls.Add(this.radioButtonFindUp);
            this.groupBoxUpDown.Location = new System.Drawing.Point(145, 42);
            this.groupBoxUpDown.Name = "groupBoxUpDown";
            this.groupBoxUpDown.Size = new System.Drawing.Size(109, 47);
            this.groupBoxUpDown.TabIndex = 3;
            this.groupBoxUpDown.TabStop = false;
            this.groupBoxUpDown.Text = "Direction";
            // 
            // radioButtonFindDown
            // 
            this.radioButtonFindDown.AutoSize = true;
            this.radioButtonFindDown.Checked = true;
            this.radioButtonFindDown.Location = new System.Drawing.Point(51, 19);
            this.radioButtonFindDown.Name = "radioButtonFindDown";
            this.radioButtonFindDown.Size = new System.Drawing.Size(53, 17);
            this.radioButtonFindDown.TabIndex = 1;
            this.radioButtonFindDown.TabStop = true;
            this.radioButtonFindDown.Text = "&Down";
            this.radioButtonFindDown.UseVisualStyleBackColor = true;
            // 
            // radioButtonFindUp
            // 
            this.radioButtonFindUp.AutoSize = true;
            this.radioButtonFindUp.Location = new System.Drawing.Point(6, 19);
            this.radioButtonFindUp.Name = "radioButtonFindUp";
            this.radioButtonFindUp.Size = new System.Drawing.Size(39, 17);
            this.radioButtonFindUp.TabIndex = 0;
            this.radioButtonFindUp.TabStop = true;
            this.radioButtonFindUp.Text = "&Up";
            this.radioButtonFindUp.UseVisualStyleBackColor = true;
            // 
            // textBoxFind
            // 
            this.textBoxFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFind.Location = new System.Drawing.Point(78, 16);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(176, 20);
            this.textBoxFind.TabIndex = 1;
            this.textBoxFind.TextChanged += new System.EventHandler(this.controlTextBox_TextChanged);
            this.textBoxFind.Enter += new System.EventHandler(this.controlTextBox_Enter);
            // 
            // FindDialog
            // 
            this.AcceptButton = this.buttonFindNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(354, 102);
            this.Controls.Add(this.textBoxFind);
            this.Controls.Add(this.groupBoxUpDown);
            this.Controls.Add(this.checkboxMatchCase);
            this.Controls.Add(this.labelFind);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonFindNext);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Find";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindDialog_FormClosing);
            this.Load += new System.EventHandler(this.FindDialog_Load);
            this.groupBoxUpDown.ResumeLayout(false);
            this.groupBoxUpDown.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFindNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.CheckBox checkboxMatchCase;
        private System.Windows.Forms.GroupBox groupBoxUpDown;
        private System.Windows.Forms.RadioButton radioButtonFindDown;
        private System.Windows.Forms.RadioButton radioButtonFindUp;
        private System.Windows.Forms.TextBox textBoxFind;
    }
}