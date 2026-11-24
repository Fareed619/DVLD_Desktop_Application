namespace DVLD_PresentationLayer.Applications.Local_License {
    partial class frmAddUpdateLocalDrivingLicense {
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
            this.lblMain = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPagePersonInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlPersonCardWithFilter1 = new DVLD_PresentationLayer.People.Controls.ctrlPersonCardWithFilter();
            this.tabPageApplicationInfo = new System.Windows.Forms.TabPage();
            this.lblDLApplicationID = new System.Windows.Forms.Label();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.lblFees = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.lblAppDate = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.picBoxConfirmPass = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblConfirmPassName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPagePersonInfo.SuspendLayout();
            this.tabPageApplicationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirmPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.BackColor = System.Drawing.SystemColors.Control;
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMain.ForeColor = System.Drawing.Color.Brown;
            this.lblMain.Location = new System.Drawing.Point(171, 30);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(736, 46);
            this.lblMain.TabIndex = 13;
            this.lblMain.Text = "New Local Driving License Application";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPagePersonInfo);
            this.tabControl.Controls.Add(this.tabPageApplicationInfo);
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl.Location = new System.Drawing.Point(18, 114);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1077, 594);
            this.tabControl.TabIndex = 15;
            // 
            // tabPagePersonInfo
            // 
            this.tabPagePersonInfo.Controls.Add(this.btnNext);
            this.tabPagePersonInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tabPagePersonInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPagePersonInfo.Name = "tabPagePersonInfo";
            this.tabPagePersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePersonInfo.Size = new System.Drawing.Size(1069, 565);
            this.tabPagePersonInfo.TabIndex = 0;
            this.tabPagePersonInfo.Text = "Person Info ";
            this.tabPagePersonInfo.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD_PresentationLayer.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.Location = new System.Drawing.Point(906, 505);
            this.btnNext.Name = "btnNext";
            this.btnNext.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnNext.Size = new System.Drawing.Size(130, 43);
            this.btnNext.TabIndex = 13;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(6, 6);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.ShowAddPerson = true;
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(1057, 493);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            this.ctrlPersonCardWithFilter1.PersonSelected += new System.Action<int>(this.ctrlPersonCardWithFilter1_PersonSelected);
            // 
            // tabPageApplicationInfo
            // 
            this.tabPageApplicationInfo.Controls.Add(this.lblDLApplicationID);
            this.tabPageApplicationInfo.Controls.Add(this.cbLicenseClass);
            this.tabPageApplicationInfo.Controls.Add(this.lblFees);
            this.tabPageApplicationInfo.Controls.Add(this.lblCreatedBy);
            this.tabPageApplicationInfo.Controls.Add(this.pictureBox4);
            this.tabPageApplicationInfo.Controls.Add(this.lblAppDate);
            this.tabPageApplicationInfo.Controls.Add(this.label5);
            this.tabPageApplicationInfo.Controls.Add(this.picBoxConfirmPass);
            this.tabPageApplicationInfo.Controls.Add(this.pictureBox3);
            this.tabPageApplicationInfo.Controls.Add(this.pictureBox2);
            this.tabPageApplicationInfo.Controls.Add(this.pictureBox1);
            this.tabPageApplicationInfo.Controls.Add(this.lblConfirmPassName);
            this.tabPageApplicationInfo.Controls.Add(this.label4);
            this.tabPageApplicationInfo.Controls.Add(this.label3);
            this.tabPageApplicationInfo.Controls.Add(this.label2);
            this.tabPageApplicationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageApplicationInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPageApplicationInfo.Name = "tabPageApplicationInfo";
            this.tabPageApplicationInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageApplicationInfo.Size = new System.Drawing.Size(1069, 565);
            this.tabPageApplicationInfo.TabIndex = 1;
            this.tabPageApplicationInfo.Text = "Application Info";
            this.tabPageApplicationInfo.UseVisualStyleBackColor = true;
            // 
            // lblDLApplicationID
            // 
            this.lblDLApplicationID.AutoSize = true;
            this.lblDLApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDLApplicationID.Location = new System.Drawing.Point(333, 117);
            this.lblDLApplicationID.Name = "lblDLApplicationID";
            this.lblDLApplicationID.Size = new System.Drawing.Size(55, 22);
            this.lblDLApplicationID.TabIndex = 11;
            this.lblDLApplicationID.Text = "[???]";
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Location = new System.Drawing.Point(337, 225);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(298, 28);
            this.cbLicenseClass.TabIndex = 10;
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees.Location = new System.Drawing.Point(333, 284);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(32, 22);
            this.lblFees.TabIndex = 9;
            this.lblFees.Text = "??";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreatedBy.Location = new System.Drawing.Point(333, 343);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(32, 22);
            this.lblCreatedBy.TabIndex = 8;
            this.lblCreatedBy.Text = "??";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD_PresentationLayer.Properties.Resources.User_32__2;
            this.pictureBox4.Location = new System.Drawing.Point(261, 338);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(36, 32);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // lblAppDate
            // 
            this.lblAppDate.AutoSize = true;
            this.lblAppDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppDate.Location = new System.Drawing.Point(333, 175);
            this.lblAppDate.Name = "lblAppDate";
            this.lblAppDate.Size = new System.Drawing.Size(32, 22);
            this.lblAppDate.TabIndex = 6;
            this.lblAppDate.Text = "??";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(50, 348);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 22);
            this.label5.TabIndex = 5;
            this.label5.Text = "Created By: ";
            // 
            // picBoxConfirmPass
            // 
            this.picBoxConfirmPass.Image = global::DVLD_PresentationLayer.Properties.Resources.money_32;
            this.picBoxConfirmPass.Location = new System.Drawing.Point(261, 279);
            this.picBoxConfirmPass.Name = "picBoxConfirmPass";
            this.picBoxConfirmPass.Size = new System.Drawing.Size(36, 32);
            this.picBoxConfirmPass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxConfirmPass.TabIndex = 2;
            this.picBoxConfirmPass.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::DVLD_PresentationLayer.Properties.Resources.New_Driving_License_32;
            this.pictureBox3.Location = new System.Drawing.Point(261, 225);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(36, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD_PresentationLayer.Properties.Resources.Calendar_32;
            this.pictureBox2.Location = new System.Drawing.Point(261, 171);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_PresentationLayer.Properties.Resources.Number_32;
            this.pictureBox1.Location = new System.Drawing.Point(261, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(36, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lblConfirmPassName
            // 
            this.lblConfirmPassName.AutoSize = true;
            this.lblConfirmPassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPassName.Location = new System.Drawing.Point(50, 285);
            this.lblConfirmPassName.Name = "lblConfirmPassName";
            this.lblConfirmPassName.Size = new System.Drawing.Size(171, 22);
            this.lblConfirmPassName.TabIndex = 0;
            this.lblConfirmPassName.Text = "Application Fees: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "License Class:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(50, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Application Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(50, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "D.L.Application ID: ";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DVLD_PresentationLayer.Properties.Resources.CloseSmall1;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(811, 729);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnClose.Size = new System.Drawing.Size(130, 43);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::DVLD_PresentationLayer.Properties.Resources.saveSmall;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(961, 728);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.btnSave.Size = new System.Drawing.Size(130, 43);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmAddUpdateLocalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 788);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblMain);
            this.Name = "frmAddUpdateLocalDrivingLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAddUpdateLocalLicense";
            this.Activated += new System.EventHandler(this.frmAddUpdateLocalDrivingLicense_Activated);
            this.Load += new System.EventHandler(this.frmAddUpdateLocalDrivingLicense_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPagePersonInfo.ResumeLayout(false);
            this.tabPageApplicationInfo.ResumeLayout(false);
            this.tabPageApplicationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxConfirmPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPagePersonInfo;
        private System.Windows.Forms.Button btnNext;
        private People.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.TabPage tabPageApplicationInfo;
        private System.Windows.Forms.PictureBox picBoxConfirmPass;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblConfirmPassName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label lblAppDate;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.Label lblDLApplicationID;
        private System.Windows.Forms.ComboBox cbLicenseClass;
    }
}