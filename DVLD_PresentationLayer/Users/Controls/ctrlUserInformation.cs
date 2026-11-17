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

namespace DVLD_PresentationLayer.Users.Controls {
    public partial class ctrlUserInformation : UserControl {

        clsUser _User;

        public ctrlUserInformation() {
            InitializeComponent();
        }

        private void ctrlUserInformation_Load(object sender, EventArgs e) {

        }

        public void LoadUserInformation(int userID) {

            _User = clsUser.Find(userID);

            if(_User == null) {
                ResetUserInformation();
                MessageBox.Show($"User with User ID: {userID} is not found");
                return;
            }


            // Load Info
            ctrlPersonInformation1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";

        }

        public void ResetUserInformation() {

            ctrlPersonInformation1.SetDefaultInfo();

            lblUserID.Text = "";
            lblUserName.Text ="";
            lblIsActive.Text = "";
        }
    }
}
