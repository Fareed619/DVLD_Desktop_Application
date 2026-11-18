using DVLD_PresentationLayer.Applications;
using DVLD_PresentationLayer.Login;
using DVLD_PresentationLayer.Tests;
using DVLD_PresentationLayer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer
{
    public partial class frmMain : Form {

        private frmLogin _FrmLogin;

        public frmMain(frmLogin LoginForm) {

            InitializeComponent();

            _FrmLogin = LoginForm;
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e) {
            frmPeople frm = new frmPeople();
            frm.ShowDialog();

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e) {
            frmDrivers frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e) {
            frmUsers frm = new frmUsers();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e) {

            clsGlobal.CurrentUser = null;
            _FrmLogin.Show();
            //this.Close();
            this.DialogResult = DialogResult.OK;
           
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e) {
            int UserID = clsGlobal.CurrentUser.User.UserID;

            frmShowUserDetails UserDetails = new frmShowUserDetails(UserID);
            UserDetails.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e) {
            int UserID = clsGlobal.CurrentUser.User.UserID;

            frmChangePassword ChangePassword = new frmChangePassword(UserID);
            ChangePassword.ShowDialog();
        }


        // Applications
        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e) {
            frmApplicationTypes applicationTypes = new frmApplicationTypes();
            applicationTypes.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e) {
            frmTestTypes testTypes = new frmTestTypes();
            testTypes.ShowDialog();
        }
    }
}
