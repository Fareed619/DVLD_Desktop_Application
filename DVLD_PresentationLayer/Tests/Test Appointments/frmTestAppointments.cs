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
    public partial class frmTestAppointments : Form {

        public event Action DataChanged;
        private void _OnDataChanged() {
            DataChanged?.Invoke();
        }

        private clsTestAppointment.enTestType _TestType = clsTestAppointment.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _RecordsCount {
            get {
                return dgvTestAppointment.Rows.Count;
            }
        }


        public frmTestAppointments(clsTestAppointment.enTestType testType, int localDrivingLicenseApplicationID) {
            InitializeComponent();

            _TestType = testType;
            _LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        private void frmTestAppointments_Load(object sender, EventArgs e) {

            _SetDefaultValuesBasedOnTestType();
            ctrlShowApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicaton(_LocalDrivingLicenseApplicationID);
            _LoadTestAppointmentsIntoDGV();
            _SetDGVSettings();
        }


        private void _SetDefaultValuesBasedOnTestType() {

            switch (_TestType) {

                case clsTestAppointment.enTestType.VisionTest:
                    lblMain.Text = "Vision Test Appointment";
                    picBMain.Image = Properties.Resources.Vision_512;

                    break;

                case clsTestAppointment.enTestType.WrittenTest:
                    lblMain.Text = "Written Test Appointment";
                    picBMain.Image = Properties.Resources.Written_Test_512;
                    break;

                case clsTestAppointment.enTestType.StreetTest:
                    lblMain.Text = "Street Test Appointment";
                    picBMain.Image = Properties.Resources.Street_Test_32;
                    break;

                default:
                    this.Close();
                    break;
            }

        }

        private void _RefreshAllData() {
            ctrlShowApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicaton(_LocalDrivingLicenseApplicationID);
            _LoadTestAppointmentsIntoDGV();
        }
        private void _LoadTestAppointmentsIntoDGV() {

            dgvTestAppointment.DataSource = clsTestAppointment.ListTestAppointments(this._LocalDrivingLicenseApplicationID, _TestType);
        }
        private void _SetDGVSettings() {

            if (_RecordsCount > 0) {

                dgvTestAppointment.Columns[0].Width = 200;
                dgvTestAppointment.Columns[0].HeaderText = "Appointment ID";

                dgvTestAppointment.Columns[1].Width = 200;
                dgvTestAppointment.Columns[1].HeaderText = "Appointment Date";

                dgvTestAppointment.Columns[2].Width = 200;
                dgvTestAppointment.Columns[2].HeaderText = "Paid Fees";

                dgvTestAppointment.Columns[3].Width = 150;
                dgvTestAppointment.Columns[3].HeaderText = "Is Locked";


                // Font 
                dgvTestAppointment.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
                dgvTestAppointment.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            }
        }
        private void _OnTestAppointmentScheduled() {
            _OnDataChanged();
            _RefreshAllData();
        }
        private void _OnTestCompleted() {
            _OnDataChanged();
            _RefreshAllData();
        }


        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void dgvTestAppointment_DataSourceChanged(object sender, EventArgs e) {
            lblRecords.Text = $"#Records: {_RecordsCount}";
        }

        private void dgvTestAppointment_SelectionChanged(object sender, EventArgs e) {

            if (dgvTestAppointment.SelectedRows.Count > 0) {
                bool ISLocked = (bool)dgvTestAppointment.CurrentRow.Cells["IsLocked"].Value;

                if (ISLocked) {
                    takeTestToolStripMenuItem.Enabled = false;
                }
                else {
                    takeTestToolStripMenuItem.Enabled = true;

                }

            }
        }


        private void btnAddAppointment_Click(object sender, EventArgs e) {

            // Check if this user has already an active appointment
            if (clsTestAppointment.DoesLocalApplicationHaveActiveAppointment(_LocalDrivingLicenseApplicationID) != null) {
                MessageBox.Show($"This Person already has an active {_TestType.ToString()}  Appointment", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  
            }

            // Check also if user passed this test 
            if(clsTestAppointment.IsTestCompleted(_LocalDrivingLicenseApplicationID, _TestType)) {
                MessageBox.Show($"This Person already passed {_TestType.ToString()} ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            frmScheduleTest frm = new frmScheduleTest(_TestType, _LocalDrivingLicenseApplicationID);
            frm.TestAppointmentSaved += _OnTestAppointmentScheduled;
            frm.ShowDialog();

        }

        private void updateTestAppointmentToolStripMenuItem_Click(object sender, EventArgs e) {
            int testAppointmentID = (int)dgvTestAppointment.CurrentRow.Cells["TestAppointmentID"].Value;

            frmScheduleTest frm = new frmScheduleTest(testAppointmentID);
            frm.TestAppointmentSaved += _OnTestAppointmentScheduled;
            frm.ShowDialog();

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e) {

            int testAppointmentID = (int)dgvTestAppointment.CurrentRow.Cells["TestAppointmentID"].Value;

            frmTakeTest frm = new frmTakeTest(testAppointmentID);
            frm.TestCompleted += _OnTestCompleted;
            frm.ShowDialog();
           
        }

       
    }
}
