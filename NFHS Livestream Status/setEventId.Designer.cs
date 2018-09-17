namespace NFHS_Livestream_Status
{
    partial class setEventId
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(setEventId));
            this.eventTb = new System.Windows.Forms.TextBox();
            this.enterBtn = new System.Windows.Forms.Button();
            this.invalidEvntLbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventTb
            // 
            this.eventTb.Location = new System.Drawing.Point(13, 13);
            this.eventTb.Name = "eventTb";
            this.eventTb.Size = new System.Drawing.Size(168, 20);
            this.eventTb.TabIndex = 0;
            // 
            // enterBtn
            // 
            this.enterBtn.Location = new System.Drawing.Point(187, 13);
            this.enterBtn.Name = "enterBtn";
            this.enterBtn.Size = new System.Drawing.Size(78, 20);
            this.enterBtn.TabIndex = 1;
            this.enterBtn.Text = "Enter";
            this.enterBtn.UseVisualStyleBackColor = true;
            this.enterBtn.Click += new System.EventHandler(this.enterBtn_Click);
            // 
            // invalidEvntLbl
            // 
            this.invalidEvntLbl.AutoSize = true;
            this.invalidEvntLbl.Location = new System.Drawing.Point(12, 36);
            this.invalidEvntLbl.Name = "invalidEvntLbl";
            this.invalidEvntLbl.Size = new System.Drawing.Size(35, 13);
            this.invalidEvntLbl.TabIndex = 2;
            this.invalidEvntLbl.Text = "label1";
            this.invalidEvntLbl.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(253, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Seach by School ID";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // setEventId
            // 
            this.AcceptButton = this.enterBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 89);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.invalidEvntLbl);
            this.Controls.Add(this.enterBtn);
            this.Controls.Add(this.eventTb);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "setEventId";
            this.Text = "Set Event ID";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.setEventId_FormClosing);
            this.Load += new System.EventHandler(this.setEventId_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox eventTb;
        private System.Windows.Forms.Button enterBtn;
        private System.Windows.Forms.Label invalidEvntLbl;
        private System.Windows.Forms.Button button1;
    }
}