namespace Notepad.Forms {
    partial class About {
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelSubTitle = new System.Windows.Forms.Label();
            this.linkLabelHaddow = new System.Windows.Forms.LinkLabel();
            this.linkLabelGIT = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(160, 42);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Notepad";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOk.Location = new System.Drawing.Point(120, 130);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelSubTitle
            // 
            this.labelSubTitle.AutoSize = true;
            this.labelSubTitle.Location = new System.Drawing.Point(16, 51);
            this.labelSubTitle.Name = "labelSubTitle";
            this.labelSubTitle.Size = new System.Drawing.Size(87, 13);
            this.labelSubTitle.TabIndex = 4;
            this.labelSubTitle.Text = "Graeme Haddow";
            // 
            // linkLabelHaddow
            // 
            this.linkLabelHaddow.AutoSize = true;
            this.linkLabelHaddow.Location = new System.Drawing.Point(117, 83);
            this.linkLabelHaddow.Name = "linkLabelHaddow";
            this.linkLabelHaddow.Size = new System.Drawing.Size(80, 13);
            this.linkLabelHaddow.TabIndex = 5;
            this.linkLabelHaddow.TabStop = true;
            this.linkLabelHaddow.Text = "haddow64.com";
            this.linkLabelHaddow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHaddow_LinkClicked);
            // 
            // linkLabelGIT
            // 
            this.linkLabelGIT.AutoSize = true;
            this.linkLabelGIT.Location = new System.Drawing.Point(117, 106);
            this.linkLabelGIT.Name = "linkLabelGIT";
            this.linkLabelGIT.Size = new System.Drawing.Size(78, 13);
            this.linkLabelGIT.TabIndex = 6;
            this.linkLabelGIT.TabStop = true;
            this.linkLabelGIT.Text = "haddow64 GIT";
            this.linkLabelGIT.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGIT_LinkClicked);
            // 
            // About
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOk;
            this.ClientSize = new System.Drawing.Size(207, 165);
            this.Controls.Add(this.linkLabelGIT);
            this.Controls.Add(this.linkLabelHaddow);
            this.Controls.Add(this.labelSubTitle);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Notepad";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelSubTitle;
        private System.Windows.Forms.LinkLabel linkLabelHaddow;
        private System.Windows.Forms.LinkLabel linkLabelGIT;
    }
}