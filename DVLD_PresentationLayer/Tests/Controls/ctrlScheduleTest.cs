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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLD_PresentationLayer.Applications.Controls {
    public partial class ctrlScheduleTest : UserControl {

        public event Action TestAppointmentSaved;

        private void OnTestAppointmentSaved() {
            TestAppointmentSaved?.Invoke();
        }

        private clsTestAppointment _TestAppointment;

        private clsApplication _RetakeApplication = null;


        public bool RetakeApplicationEnabled {
            get {
               return gbRetakeTestInfo.Enabled;
            }
            set {
                gbRetakeTestInfo.Enabled = value;
            }
        }

        public ctrlScheduleTest() {

            InitializeComponent();
        }

        private void ctrlScheduleTest_Load(object sender, EventArgs e) {
            dateTimePicker1.MinDate = DateTime.Now;

        }


        // AddNew Test Appointment
        public void LoadInfoIntoCtrl(int LocalDrivingLicenseApplicationID, clsTestAppointment.enTestType TestTypeID) {

            clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);
            if (LocalDrivingLicenseApplication == null) {
                MessageBox.Show("Local Driving Licesnse ID is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SetDefaultValues();
                return;
            };
            

            _TestAppointment = new clsTestAppointment();
            _SetNewTestAppointmentValues(LocalDrivingLicenseApplication, TestTypeID);
            _FillControlsFromTestAppointmentInfo();



            byte Trials = clsTest.TrialsCount(_TestAppointment.LocalDrivingLicenseApplicationID, (int)_TestAppointment.TestTypeID);
            lblTrial.Text = Trials.ToString();


            if (Trials > 0) {
                gbRetakeTestInfo.Enabled = true;
                _RetakeApplication = new clsApplication();
                _PraperRetakeApplicationInfo();

                lblRAppFees.Text = _RetakeApplication.PaidFees.ToString();
                lblRTotalFees.Text = (_RetakeApplication.PaidFees + _TestAppointment.PaidFees).ToString();

            }
            else {
                gbRetakeTestInfo.Enabled = false;
            }

        }


        // Update Test Appointment
        public void LoadInfoIntoCtrl(int TestAppointmentID) {

            clsTestAppointment TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (TestAppointment == null) {
                MessageBox.Show("Test Appointment is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _SetDefaultValues();
                return;
            }

            _TestAppointment = TestAppointment;
            _FillControlsFromTestAppointmentInfo();


            byte Trials = clsTest.TrialsCount(_TestAppointment.LocalDrivingLicenseApplicationID, (int)_TestAppointment.TestTypeID);
            lblTrial.Text = Trials.ToString();



            if (_TestAppointment.RetakeTestApplicationID != -1) { // There is an application linked with it

                gbRetakeTestInfo.Enabled = true;
                _RetakeApplication = _TestAppointment.RetakeTestApplication;

                lblRAppFees.Text = _RetakeApplication.PaidFees.ToString();
                lblRTotalFees.Text = (_RetakeApplication.PaidFees + _TestAppointment.PaidFees).ToString();

            }
            else {
                gbRetakeTestInfo.Enabled = false;
            }

        }
               




        private void _FillControlsFromTestAppointmentInfo() {

            _SetAppropriateImageAndTitle(_TestAppointment.TestTypeID);

            lblMain.Text = _TestAppointment.TestType.TestTypeTitle;
            lblLDLAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblLClass.Text = _TestAppointment.LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblName.Text = _TestAppointment.LocalDrivingLicenseApplication.Applicant.FullName;
            lblFees.Text = _TestAppointment.TestType.TestTypeFees.ToString();
            dateTimePicker1.Value = _TestAppointment.AppointmentDate;


        }

        private void _SetDefaultValues() {
            lblMain.Text = "???";
            lblLDLAppID.Text = "???";
            lblLClass.Text = "???";
            lblName.Text = "???";
            lblFees.Text = "???";
            dateTimePicker1.Value = DateTime.Now;
            gbRetakeTestInfo.Enabled = false;

        }

        private void _SetNewTestAppointmentValues(clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication, clsTestAppointment.enTestType TestTypeID) {

            _TestAppointment.LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            _TestAppointment.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = TestTypeID;
            _TestAppointment.TestType = clsTestType.Find((clsTestType.enTestType)TestTypeID);
            _TestAppointment.PaidFees = _TestAppointment.TestType.TestTypeFees;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.User.UserID;
            _TestAppointment.AppointmentDate = DateTime.Now;
            _TestAppointment.IsLocked = false;

        }

        private void _SetAppropriateImageAndTitle(clsTestAppointment.enTestType TestTypeID) {

            switch (TestTypeID) {
                case clsTestAppointment.enTestType.VisionTest:
                    picBMain.Image = Properties.Resources.Vision_512;
                    gbSechduleTest.Text = "Vision Test";
                    break;

                case clsTestAppointment.enTestType.WrittenTest:
                    picBMain.Image = Properties.Resources.Written_Test_512;
                    gbSechduleTest.Text = "Written Test";
                    break;

                case clsTestAppointment.enTestType.StreetTest:
                    picBMain.Image = Properties.Resources.Street_Test_32;
                    gbSechduleTest.Text = "Street Test";
                    break;
            }
        }

        private void _PraperRetakeApplicationInfo() {
            _RetakeApplication.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
            _RetakeApplication.ApplicationDate = DateTime.Now;
            _RetakeApplication.ApplicationStatus = clsApplication.enAppStatus.Completed;
            _RetakeApplication.ApplicantPersonID = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_TestAppointment.LocalDrivingLicenseApplicationID).ApplicantPersonID;

            _RetakeApplication.LastStatusDate = DateTime.Now;
            _RetakeApplication.CreatedByUserID = _TestAppointment.CreatedByUserID;
            _RetakeApplication.PaidFees = clsApplicationType.Find((int)_RetakeApplication.ApplicationTypeID).ApplicationTypeFees;

        }

        private void btnSave_Click(object sender, EventArgs e) {
           

            if(_RetakeApplication != null) {

                if (!_RetakeApplication.SaveApplication()) {
                    MessageBox.Show("Retake Application Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblRAppID.Text = _RetakeApplication.ApplicationID.ToString();
                _TestAppointment.RetakeTestApplicationID = _RetakeApplication.ApplicationID;
            }

            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
          

            if (_TestAppointment.Save()) {
                OnTestAppointmentSaved();
                MessageBox.Show("Test Appointment Saved Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Test Appointment Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
