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

        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;
        private int _LocalDrivingLicenseApplicationID = -1;
        private int _RecordsCount {
            get {
                return dgvTestAppointment.Rows.Count;
            }
        }


        public frmTestAppointments(clsTestType.enTestType testType, int localDrivingLicenseApplicationID) {
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

                case clsTestType.enTestType.VisionTest:
                    lblMain.Text = "Vision Test Appointment";
                    picBMain.Image = Properties.Resources.Vision_512;

                    break;

                case clsTestType.enTestType.WrittenTest:
                    lblMain.Text = "Written Test Appointment";
                    picBMain.Image = Properties.Resources.Written_Test_512;
                    break;

                case clsTestType.enTestType.StreetTest:
                    lblMain.Text = "Street Test Appointment";
                    picBMain.Image = Properties.Resources.Street_Test_32;
                    break;

                default:
                    this.Close();
                    break;
            }

        }
        private void _LoadTestAppointmentsIntoDGV() {

            dgvTestAppointment.DataSource = clsTestAppointment.ListTestAppointments(this._LocalDrivingLicenseApplicationID);
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


        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        private void dgvTestAppointment_DataSourceChanged(object sender, EventArgs e) {
            lblRecords.Text = $"#Records: {_RecordsCount}";
        }

        private void btnAddAppointment_Click(object sender, EventArgs e) {

            // Check if this user has already an active appointment
            if (clsTestAppointment.DoesLocalApplicationHaveActiveAppointment(_LocalDrivingLicenseApplicationID) != null) {
                MessageBox.Show("This Person already has an active test appointment", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  
            }


            frmScheduleTest frm = new frmScheduleTest((clsTestAppointment.enTestType)_TestType, _LocalDrivingLicenseApplicationID);
            frm.TestAppointmentSaved += _OnTestAppointmentSaved;
            frm.ShowDialog();

        }

        private void updateTestAppointmentToolStripMenuItem_Click(object sender, EventArgs e) {
            int testAppointmentID = (int)dgvTestAppointment.CurrentRow.Cells["TestAppointmentID"].Value;

            frmScheduleTest frm = new frmScheduleTest(testAppointmentID);
            frm.TestAppointmentSaved += _OnTestAppointmentSaved;
            frm.ShowDialog();

        }

        private void _OnTestAppointmentSaved() {
            _LoadTestAppointmentsIntoDGV();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e) {
            int testAppointmentID = (int)dgvTestAppointment.CurrentRow.Cells["TestAppointmentID"].Value;

            frmTakeTest frm = new frmTakeTest(testAppointmentID);
            frm.ShowDialog();
            _LoadTestAppointmentsIntoDGV();
        }

    }
}
