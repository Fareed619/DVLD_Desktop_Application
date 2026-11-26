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

namespace DVLD_PresentationLayer.Tests.Test_Appointments {
    public partial class frmScheduleTest : Form {

        public event Action TestAppointmentSaved;

        private void OnTestSaved() {
            TestAppointmentSaved?.Invoke();
           
        }

        enum enMode { AddNew = 1, Update = 2}

        enMode _Mode = enMode.AddNew;
        clsTestAppointment.enTestType _TestType;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _TestAppointmentID = -1;


        public frmScheduleTest(clsTestAppointment.enTestType testType, int localDrivingLicenseApplicationID) {
            InitializeComponent();
            _TestType = testType;
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _Mode = enMode.AddNew;
        }

        public frmScheduleTest(int testAppointmentID) {
            InitializeComponent();
            _TestAppointmentID = testAppointmentID;
            _Mode = enMode.Update;
        }



        private void frmScheduleTest_Load(object sender, EventArgs e) {

            if(_Mode == enMode.AddNew) {
                ctrlScheduleTest1.LoadInfoIntoCtrl(_LocalDrivingLicenseApplicationID, _TestType);

            }
            else {
                ctrlScheduleTest1.LoadInfoIntoCtrl(_TestAppointmentID);
            }

            ctrlScheduleTest1.TestAppointmentSaved += OnTestAppointmentSaved;
                
        }


        public void OnTestAppointmentSaved() {
            // Saved
            OnTestSaved();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }


    }
}
