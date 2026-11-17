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
    public partial class frmChangePassword : Form {
        
        private int  _UserID = -1;
        private clsUser _User;

        public frmChangePassword(int UserID) {
            InitializeComponent();

            _UserID = UserID;
        }

        private void frmChangePassword_Load(object sender, EventArgs e) {
            _ResetDefaultValues();

            _User = clsUser.Find(_UserID);

            if(_User == null) {
                MessageBox.Show("User Is not Found", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserInformation1.LoadUserInformation(_UserID);
            txtCurrentPass.Focus();
        }


        private void _ResetDefaultValues() {
            ctrlUserInformation1.ResetUserInformation();

            txtCurrentPass.Text = "";
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
        }

        private bool _CheckIsRequired(Control ctrl, string Message, CancelEventArgs e) {

            if (string.IsNullOrEmpty(ctrl.Text.Trim())) {

                e.Cancel = true;
                errorProvider1.SetError(ctrl, Message);
                return false;
            }
            else {
                errorProvider1.SetError(ctrl, string.Empty);
                return true;

            }
        }

        private bool _CheckPasswordsMatch(Control Password, Control ConfirmPassword, CancelEventArgs e) {

            if (Password.Text.Trim() != ConfirmPassword.Text.Trim()) {

                e.Cancel = true;
                errorProvider1.SetError(ConfirmPassword, "Confirm Passwor Doesn't match the password");
                return false;
            }
            else {
                errorProvider1.SetError(ConfirmPassword, string.Empty);
                return true;
            }

        }



        private void btnSave_Click(object sender, EventArgs e) {

            if (!this.ValidateChildren()) {
                MessageBox.Show("Please fix the errors on the form before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop here if anything is invalid
            }

            string CurrentPassword = txtCurrentPass.Text.Trim();
            string NewPassword = txtNewPass.Text.Trim();

            string ErrorMessage = "";

            if (_User.ChangePassword(CurrentPassword, NewPassword, ref ErrorMessage)) { 
                MessageBox.Show("Passowrd has been changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else {
                MessageBox.Show(ErrorMessage, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            


}

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

  

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e) {

            _CheckIsRequired(txtCurrentPass, "Current Password is REQUIRED", e);
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e) {

            _CheckIsRequired(txtNewPass, "New Password is REQUIRED", e);

        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e) {

            if (!_CheckIsRequired(txtConfirmPass, "Confirm Password is REQUIRED", e)) return;

            _CheckPasswordsMatch(txtNewPass, txtConfirmPass, e);

        }

    }
}
