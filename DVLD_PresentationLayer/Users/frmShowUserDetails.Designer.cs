namespace DVLD_PresentationLayer.Users {
    partial class frmShowUserDetails {
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
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlUserInformation1 = new DVLD_PresentationLayer.Users.Controls.ctrlUserInformation();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(400, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 51);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Details";
            // 
            // ctrlUserInformation1
            // 
            this.ctrlUserInformation1.Location = new System.Drawing.Point(21, 117);
            this.ctrlUserInformation1.Name = "ctrlUserInformation1";
            this.ctrlUserInformation1.Size = new System.Drawing.Size(1063, 578);
            this.ctrlUserInformation1.TabIndex = 2;
            // 
            // frmShowUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 707);
            this.Controls.Add(this.ctrlUserInformation1);
            this.Controls.Add(this.label1);
            this.Name = "frmShowUserDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmShowUserDetails";
            this.Load += new System.EventHandler(this.frmShowUserDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Controls.ctrlUserInformation ctrlUserInformation1;
    }
}