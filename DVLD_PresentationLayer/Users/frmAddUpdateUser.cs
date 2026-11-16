using DVLD_BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.Users {

    public partial class frmAddUpdateUser : Form {

        public event Action UserAddedUpdated;

        private void OnUserSaved() {
            UserAddedUpdated?.Invoke();
        }

        enum enMode { AddNew = 1, Update = 2};

        private enMode _Mode = enMode.AddNew;

        private int _PersonID = -1;
        private int _UserID = -1;
        private clsUser _User;



        // Add New
        public frmAddUpdateUser() {

            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        // Update
        public frmAddUpdateUser(int userID) {

            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = userID;
        }


        private void frmAddUpdateUser_Load(object sender, EventArgs e) {

            // Save the original default font
            Font defaultFont = this.Font;

            // Set the custom font for the tab headers
            tabControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // Re-apply the default font to each TabPage so child controls inherit it
            foreach (TabPage page in tabControl.TabPages) {
                page.Font = defaultFont;
            }


            if (_Mode == enMode.AddNew) {

                _User = new clsUser();
                lblMain.Text = "Add New User";
                txtUsername.Focus();
                btnNext.Enabled = false;
            }
            else {

                _User = clsUser.Find(_UserID);
                _PersonID = _User.PersonID;

                if (_User == null) {

                    MessageBox.Show("User is not found");
                    return;
                }            

                _SetUpdateMode();
                _FillUserDetails();
            }     

        }

        private void _FillUserDetails() {

            ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);

            txtUserID.Text = _User.UserID.ToString();
            txtUsername.Text = _User.UserName;
            ckbIsActive.Checked = _User.IsActive;
        }

           
        private void _SetErrorProvider(Control control, string message) {

            errorProvider1.SetError(control, message);
        }

        private bool _IsControlRequired(Control control, CancelEventArgs e) {

            if (string.IsNullOrWhiteSpace(control.Text.Trim())) {

                e.Cancel = true;
                _SetErrorProvider(control, $"{control.Tag} is Required");
                return false;

            }
            else {
                _SetErrorProvider(control, string.Empty);
                return true;

            }

        }

        private void _SetUpdateMode() {

            lblMain.Text = "Update User";
            this._Mode = enMode.Update;

            txtPassword.ReadOnly = true;
            txtPassword.ReadOnly = true;
            txtPassword.BorderStyle = BorderStyle.None;
 
            txtConfirmPass.Visible = false;
            lblConfirmPassName.Visible = false;
            picBoxConfirmPass.Visible = false;

            txtPassword.Text = "*************";

            ctrlPersonCardWithFilter1.FilterEnabled = false;
        }

        private bool _CheckIfPersonALreadyLinkedWithUser() {

            if (clsUser.DoesPersonExist(_PersonID)) {

                MessageBox.Show("Person is Already linked with another USER,\nChoose another PERSON", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.ResetPersonInfo();
                return false;
            }
            else {
                return true;
            }

        }

        private bool _CheckUserInfoBeforeSaving() {

            if (!this.ValidateChildren()) {
                MessageBox.Show("Please fix the errors on the form before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Stop here if anything is invalid
            }

            if (_PersonID == -1) {
                MessageBox.Show("Please Select A person To link with", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_Mode == enMode.AddNew) {
                this._User.Password = txtPassword.Text.Trim();
                this._User.PersonID = _PersonID;

            }

            this._User.UserName = txtUsername.Text.Trim();
            this._User.IsActive = ckbIsActive.Checked;

            return true;
        }

        private void ctrlPersonCardWithFilter1_PersonSelected(int obj) {

            _PersonID = obj;

            if (_CheckIfPersonALreadyLinkedWithUser()) {

                btnNext.Enabled = true;
            }
        }

            

        private void txtUsername_Validating(object sender, CancelEventArgs e) {

            _IsControlRequired(txtUsername, e);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e) {
            if (this._Mode == enMode.Update) return;
            _IsControlRequired(txtPassword, e);

        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e) {
            if (this._Mode == enMode.Update) return;

            _IsControlRequired(txtConfirmPass, e);
            if(txtConfirmPass.Text.Trim() != txtPassword.Text.Trim()) {
                _SetErrorProvider(txtConfirmPass, "Password Confimration doesn't match the password");
            }
            else {
                _SetErrorProvider(txtConfirmPass, string.Empty);
            }

        }



        private void btnNext_Click(object sender, EventArgs e) {
            tabControl.SelectedTab = tabPageLoginInfo;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {

            if (!_CheckUserInfoBeforeSaving()) return;
            

            if (_User.Save()) {

                OnUserSaved();

                _SetUpdateMode();

                txtUserID.Text = _User.UserID.ToString();
                _UserID = _User.UserID;
          
                MessageBox.Show("User has been Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Saving the user has been Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }




        // ****** Login Info Events ******
    }
}
