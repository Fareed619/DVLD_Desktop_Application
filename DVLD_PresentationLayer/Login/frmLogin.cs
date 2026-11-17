using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.Login {

    public partial class frmLogin : Form {

        clsLogin LoginInfo;
        
        public frmLogin() {
            InitializeComponent();
        }


        private void frmLogin_VisibleChanged(object sender, EventArgs e) {

            if(this.Visible) {

                string Username = "";
                string Password = "";

                bool isFound = clsUtils.GetLoginInfoFromTxt(ref Username, ref Password);

                // User asked for remembering him
                if (isFound) {
                    txtUsername.Text = Username;
                    txtPassword.Text = Password;
                    ckbRememberMe.Checked = true;
                }
                else {
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    ckbRememberMe.Checked = false;

                }
            }
        }

        private void _OpenMainForm() {

            frmMain Main = new frmMain(this);

            this.Hide();

            // close the application
            if (Main.ShowDialog() == DialogResult.Cancel) {
                this.Close();
            }
            Main.Close(); 

        }

        private void btnLogin_Click(object sender, EventArgs e) {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();


            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) {
                MessageBox.Show("All Fields are requried");
                return;
            }

            LoginInfo = new clsLogin(username, password);

            string ErrorMessage = "";
            
            if(!LoginInfo.LogIn(ref ErrorMessage)) {
           
                MessageBox.Show(ErrorMessage, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else {
                if (ckbRememberMe.Checked) {
                    // Store user's info in a text file
                    clsUtils.StoreLoginInfoInTxt(username, password);
                }
                else {
                    clsUtils.EmptyLoginInfoTxt();
                }


                clsGlobal.CurrentUser = LoginInfo;

                _OpenMainForm();
            }
        
        }

        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}
