using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Controls;
using DVLD_PresentationLayer.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.Applications.Controls {
    public partial class ctrlShowApplicationInfo : UserControl {

        private int _PersonID = -1;

        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID = -1;
        public int LocalDrivingLicenseApplicationID {
            get {
                return _LocalDrivingLicenseApplicationID;
            }
        }


        public ctrlShowApplicationInfo() {
            InitializeComponent();
        }

        private void ctrlShowApplicationInfo_Load(object sender, EventArgs e) {

            groupBox1.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            groupBox2.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);


        }


        private void _FillInfoIntoControls() {
            _PersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;

            lblLDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseCLass.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblPassedTests.Text = $"{0}/3";

            lblID.Text = _LocalDrivingLicenseApplication.ApplicationID.ToString();
            lblStatus.Text = _LocalDrivingLicenseApplication.ApplicationStatusText;
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblType.Text = _LocalDrivingLicenseApplication.ApplicationType.ApplicationTypeTitle;
            lblApplicant.Text = _LocalDrivingLicenseApplication.Applicant.FullName;
            lblDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = _LocalDrivingLicenseApplication.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _LocalDrivingLicenseApplication.User.UserName;

        }

        private void _ResetDefaultValues() {

            lblLDLAppID.Text = "???";
            lblLicenseCLass.Text = "???";
            lblPassedTests.Text = "0";

            lblID.Text = "???";
            lblStatus.Text = "???";
            lblFees.Text = "???";
            lblType.Text = "???";
            lblApplicant.Text = "???";
            lblDate.Text = "???";
            lblStatusDate.Text = "???";
            lblCreatedBy.Text = "???";
        }
    
        public bool LoadApplicationInfoByApplicationID(int applicationID) {


            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(applicationID);

            if (LocalDrivingLicenseApplication == null) {
                _ResetDefaultValues();
                MessageBox.Show("Local Driving License Application is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _FillInfoIntoControls();
            return true;
        }

        public bool LoadApplicationInfoByLocalDrivingLicenseApplicaton(int localDrivingLicenseApplicationID) {

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(localDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null) {
                _ResetDefaultValues();
                MessageBox.Show("Local Driving License Application is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;

            _FillInfoIntoControls();
            return true;
        }

  

        private void linkLblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            frmPersonDetails frm = new frmPersonDetails(_PersonID);
            frm.ShowDialog();
        }

        private void linklblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            //TODO
        }
    }
}
