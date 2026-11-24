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

namespace DVLD_PresentationLayer.Applications.Local_License {

    public partial class frmLocalDrivingLicenseApplications : Form {



        public int _RecordsCount {
            get {
                return dgvLocalApplications.Rows.Count;
            }
        }

        public frmLocalDrivingLicenseApplications() {

            InitializeComponent();
        }

 
        private void frmLocalDrivingLicenseApplications_Load(object sender, EventArgs e) {
            _LoadLocalDrivingLicesneListIntoDGV();
            _SetDGVSettings();
            _SetComboBoxFilterOptions();

        }


        private void _LoadLocalDrivingLicesneListIntoDGV() {
            dgvLocalApplications.DataSource = clsLocalDrivingLicenseApplication.ListLocalDrivingLicenseAppsView();
        }

        private void _SetDGVSettings() {

            if (_RecordsCount > 0) {

                dgvLocalApplications.Columns[0].Width = 150;
                dgvLocalApplications.Columns[0].HeaderText = "L.D.LApp ID";

                dgvLocalApplications.Columns[1].Width = 200;
                dgvLocalApplications.Columns[1].HeaderText = "Driving Class";

                dgvLocalApplications.Columns[2].Width = 140;
                dgvLocalApplications.Columns[2].HeaderText = "National No.";

                dgvLocalApplications.Columns[3].Width = 200;
                dgvLocalApplications.Columns[3].HeaderText = "Full Name";

                dgvLocalApplications.Columns[4].Width = 170;
                dgvLocalApplications.Columns[4].HeaderText = "Application Date";

                dgvLocalApplications.Columns[5].Width = 150;
                dgvLocalApplications.Columns[5].HeaderText = "Passed Tests";

                dgvLocalApplications.Columns[6].Width = 100;
                dgvLocalApplications.Columns[6].HeaderText = "Status";


                // Font 
                dgvLocalApplications.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
                dgvLocalApplications.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            }

        }

        private void _SetComboBoxFilterOptions() {

            DataTable table = new DataTable("Filters");

            table.Columns.Add("FilterColumn", typeof(string));
            table.Columns.Add("FilterValue", typeof(string));

            table.Rows.Add("None", "None");
            table.Rows.Add("L.D.LApp ID", "LocalDrivingLicenseApplicationID");
            table.Rows.Add("Driving Class", "DrivingClass");
            table.Rows.Add("National No.", "NationalNo");
            table.Rows.Add("Full Name", "FullName");
            table.Rows.Add("Application Date", "ApplicationDate");
            table.Rows.Add("Passed Tests", "PassedTests");
            table.Rows.Add("Status", "Status");

            cbFilterBy.DisplayMember = "FilterColumn";
            cbFilterBy.ValueMember = "FilterValue";
            cbFilterBy.DataSource = table;

            cbFilterBy.SelectedValue = "None";
        }

        private void _ApplyFillter() {

            string ColumnName = cbFilterBy.SelectedValue.ToString().Trim();
            string Value = txtFilterValue.Text.Trim();

            if (string.IsNullOrEmpty(Value)) {
                _LoadLocalDrivingLicesneListIntoDGV();
                return;
            }

            if (ColumnName == "LocalDrivingLicenseApplicationID") {

                dgvLocalApplications.DataSource = clsLocalDrivingLicenseApplication.FilterBy(ColumnName, Convert.ToInt32(Value));
            }
            else {
                dgvLocalApplications.DataSource = clsLocalDrivingLicenseApplication.FilterBy(ColumnName, Value);
            }



        }

        private void _RefreshFilter() {
            cbFilterBy.SelectedValue = "None";
            txtFilterValue.Text = string.Empty;
        }

        public void OnLocalApplicationSaved() {
            _LoadLocalDrivingLicesneListIntoDGV();
            _RefreshFilter();
        }




        private void btnAddLocalApplication_Click(object sender, EventArgs e) {
            frmAddUpdateLocalDrivingLicense addNewLocalLicense = new frmAddUpdateLocalDrivingLicense();
            addNewLocalLicense.LocalApplicationSaved += OnLocalApplicationSaved;
            addNewLocalLicense.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e) {



            if (cbFilterBy.SelectedValue.ToString() != "None") {

                txtFilterValue.Visible = true;
                txtFilterValue.Focus();

            }
            else {
                txtFilterValue.Visible = false;
                _LoadLocalDrivingLicesneListIntoDGV();

            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e) {
            _ApplyFillter();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e) {
            
            if(cbFilterBy.SelectedValue.ToString() == "LocalDrivingLicenseApplicationID" || cbFilterBy.SelectedValue.ToString() == "PassedTests") {

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                    e.Handled = true;
                }

            }
            
        }

        private void dgvLocalApplications_DataSourceChanged(object sender, EventArgs e) {
            lblRecords.Text = $"#Records: {_RecordsCount}";
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e) {

            if(MessageBox.Show("Are you sure that you wanna CANCEL this Application ???", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {

                int localID = (int)dgvLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;

                clsLocalDrivingLicenseApplication localDrivingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(localID);


                if (localDrivingApplication.Cancel()) {
                    MessageBox.Show("Application Cancelled Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _LoadLocalDrivingLicesneListIntoDGV();
                }
                else {
                    MessageBox.Show("Changing status failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else {
                MessageBox.Show("Proccess has been Cancelled", "Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e) {

            if (MessageBox.Show("Are you sure that you wanna Delete this Application ???", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {

                clsLocalDrivingLicenseApplication localDrivingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID((int)dgvLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

                if (localDrivingApplication == null) return;

                if (localDrivingApplication.DeleteLocalDrivingApplication()) {

                    MessageBox.Show("Application Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _LoadLocalDrivingLicesneListIntoDGV();
                }
                else {
                    MessageBox.Show("Changing status failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else {
                MessageBox.Show("Proccess has been Cancelled", "Cancellation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e) {

            int LDLAppID = (int)dgvLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value;

            frmShowLocalDrivingLicenseApplicationInfo frm = new frmShowLocalDrivingLicenseApplicationInfo(LDLAppID);
            frm.ShowDialog();
        }

        private void dgvLocalApplications_SelectionChanged(object sender, EventArgs e) {

            if(dgvLocalApplications.SelectedRows.Count > 0) {
                string ApplicationStatus = (string)dgvLocalApplications.CurrentRow.Cells["Status"].Value;

                if(ApplicationStatus != "NEW") {
                    cancelToolStripMenuItem.Enabled = false;
                }
                else {
                    cancelToolStripMenuItem.Enabled = true;

                }
            }
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e) {
            frmAddUpdateLocalDrivingLicense frm = new frmAddUpdateLocalDrivingLicense((int)dgvLocalApplications.CurrentRow.Cells["LocalDrivingLicenseApplicationID"].Value);

            frm.LocalApplicationSaved += OnLocalApplicationSaved;

            frm.ShowDialog();
        }

    }
}
