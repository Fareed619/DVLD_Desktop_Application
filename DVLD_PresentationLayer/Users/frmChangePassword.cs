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
            _User = clsUser.Find(_UserID);

            ctrlUserInformation1.LoadUserInformation(_UserID);
            txtCurrentPass.Focus();
        }



        private void _CheckIsRequired(Control ctrl, string Message) {

            if (string.IsNullOrEmpty(ctrl.Text.Trim())) {

                errorProvider1.SetError(ctrl, Message);
            }
            else {
                errorProvider1.SetError(ctrl, string.Empty);

            }
        }

        private void _CheckPasswordsMatch(Control Password, Control ConfirmPassword) {

            if (Password.Text.Trim() != ConfirmPassword.Text.Trim()) {

                errorProvider1.SetError(ConfirmPassword, "Confirm Passwor Doesn't match the password");
            }
            else {
                errorProvider1.SetError(ConfirmPassword, string.Empty);
            }

        }



        private void btnSave_Click(object sender, EventArgs e) {

            if (!this.ValidateChildren()) {
                MessageBox.Show("Please fix the errors on the form before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop here if anything is invalid
            }

            string CurrentPassword = txtCurrentPass.Text.Trim();
            string NewPassword = txtNewPass.Text.Trim();

            if (_User.ChangePassword(CurrentPassword, NewPassword)) { 
                MessageBox.Show("Passowrd has been changed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else {
                MessageBox.Show("Changing password has been Failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            


}

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

  

        private void txtCurrentPass_Validating(object sender, CancelEventArgs e) {

            _CheckIsRequired(txtCurrentPass, "Current Password is REQUIRED");
        }

        private void txtNewPass_Validating(object sender, CancelEventArgs e) {

            _CheckIsRequired(txtNewPass, "New Password is REQUIRED");

        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e) {

            _CheckIsRequired(txtConfirmPass, "Confirm Password is REQUIRED");
            _CheckPasswordsMatch(txtNewPass, txtConfirmPass);

        }
    }
}
