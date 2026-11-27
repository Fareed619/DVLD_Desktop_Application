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

namespace DVLD_PresentationLayer.Tests {
    public partial class frmTakeTest : Form {

        private int _TestAppointmentID = -1;
        private clsTestAppointment _TestAppointment;

        private clsTest _Test;

        
        public frmTakeTest(int TestAppointmentID) {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e) {
            clsTestAppointment TestAppointment = clsTestAppointment.Find(_TestAppointmentID);

            if(TestAppointment == null) {
                MessageBox.Show("Test Appointment was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            _Test = new clsTest();
            _TestAppointment = TestAppointment;
            rbFail.Checked = true;
            _SetValuesIntoControls();

        }

        private void _SetValuesIntoControls() {
            lblLDLAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblLClass.Text = _TestAppointment.LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblName.Text = _TestAppointment.LocalDrivingLicenseApplication.Applicant.FullName;
            lblTrial.Text = clsTest.TrialsCount(_TestAppointment.LocalDrivingLicenseApplicationID, 
                (int)_TestAppointment.TestTypeID).ToString();

            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            lblTestID.Text = "Not Taken Yet";

            _SetAppropriateImageAndTitle(_TestAppointment.TestTypeID);
        }

        private void _SetAppropriateImageAndTitle(clsTestAppointment.enTestType TestTypeID) {

            switch (TestTypeID) {
                case clsTestAppointment.enTestType.VisionTest:
                    picBMain.Image = Properties.Resources.Vision_512;
                    gbTestInfo.Text = "Vision Test";
                    break;

                case clsTestAppointment.enTestType.WrittenTest:
                    picBMain.Image = Properties.Resources.Written_Test_512;
                    gbTestInfo.Text = "Written Test";
                    break;

                case clsTestAppointment.enTestType.StreetTest:
                    picBMain.Image = Properties.Resources.Street_Test_32;
                    gbTestInfo.Text = "Street Test";
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {

            _Test.TestAppointmentID = _TestAppointmentID;

            if (rbFail.Checked) {
                _Test.TestResult = false;
            }
            else {
                _Test.TestResult = true;
            }
            _Test.Notes = lblNotes.Text;
            _Test.CreatedByUserID = clsGlobal.CurrentUser.User.UserID;

            if (_Test.Save()) {
                lblTestID.Text = _Test.TestID.ToString();
                MessageBox.Show("Test Added Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else {
                MessageBox.Show("Test Creation Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
