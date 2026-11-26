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
            //dateTimePicker1.MinDate = DateTime.Now;
            
        }

        public void LoadInfoIntoCtrl(int LocalDrivingLicenseApplicationID, clsTestAppointment.enTestType TestType) {

            _TestAppointment = new clsTestAppointment();
            _SetTestAppointmentDefaultValues( LocalDrivingLicenseApplicationID, TestType);


            clsLocalDrivingLicenseApplication LocalApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(LocalDrivingLicenseApplicationID);
            if (LocalApplication == null) return;

            lblMain.Text = TestType.ToString();
            lblLDLAppID.Text = LocalApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLClass.Text = LocalApplication.LicenseClass.ClassName;
            lblName.Text = LocalApplication.Applicant.FullName;
            lblFees.Text = clsTestType.Find((clsTestType.enTestType)TestType).TestTypeFees.ToString();

            //TODO 
            lblTrial.Text = "0";
            gbRetakeTestInfo.Enabled = false;


        }

        public void LoadInfoIntoCtrl(int TestAppointmentID) {

            clsTestAppointment TestAppointment = clsTestAppointment.Find(TestAppointmentID);

            if (TestAppointment == null) return;

            _TestAppointment = TestAppointment;

            lblMain.Text = TestAppointment.TestType.TestTypeTitle;

            lblLDLAppID.Text = TestAppointment.LocalDrivingLicenseApplicationID.ToString();

            lblLClass.Text = TestAppointment.LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblName.Text = TestAppointment.LocalDrivingLicenseApplication.Applicant.FullName;
            lblFees.Text = clsTestType.Find((clsTestType.enTestType)TestAppointment.TestTypeID).TestTypeFees.ToString();
            dateTimePicker1.Value = TestAppointment.AppointmentDate;

            //TODO 
            lblTrial.Text = "0";
            gbRetakeTestInfo.Enabled = false;

        }


        private void _SetTestAppointmentDefaultValues(int LocalDrivingLicenseApplicationID, clsTestAppointment.enTestType TestTypeID) {
            _TestAppointment.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestAppointment.TestTypeID = TestTypeID;
            _TestAppointment.PaidFees = clsTestType.Find((clsTestType.enTestType)TestTypeID).TestTypeFees;
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.User.UserID;

        }


        private void btnSave_Click(object sender, EventArgs e) {

            _TestAppointment.AppointmentDate = dateTimePicker1.Value;
            _TestAppointment.IsLocked = false;


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
