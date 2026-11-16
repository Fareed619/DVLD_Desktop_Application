using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer {

    public partial class frmUsers : Form {

        private DataTable _AllUsers;
        private DataView _dvUsers;

        public frmUsers() {
            InitializeComponent();
        }


        private int RecordsCount {
            get {
                return dgvUsers.Rows.Count;
            }
        }
      

        private void frmUsers_Load(object sender, EventArgs e) {

            _LoadUsersIntoDataGridView();

            _SetDGVSettings();

            _AddItemsTocomboBoxFilter();
            comboBoxFilter.SelectedItem = "None";

            _AddItemsTocbIsActiveFilter();
            cbIsActiveFilter.SelectedItem = "All";
        }

        private void _LoadUsersIntoDataGridView() {
            _AllUsers = clsUser.ListUsers();
            _dvUsers = _AllUsers.DefaultView;

            dgvUsers.DataSource = _dvUsers;
        }

        private void _SetDGVSettings() {
            dgvUsers.Columns[0].Width = 100;
            dgvUsers.Columns[0].HeaderText = "User ID";

            dgvUsers.Columns[1].Width = 100;
            dgvUsers.Columns[1].HeaderText = "Person ID";

            dgvUsers.Columns[2].Width = 220;
            dgvUsers.Columns[2].HeaderText = "Full Name";

            dgvUsers.Columns[3].Width = 100;
            dgvUsers.Columns[3].HeaderText = "User Name";


            dgvUsers.Columns[4].Width = 100;
            dgvUsers.Columns[4].HeaderText = "Is Active";


            // Font 
            dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            dgvUsers.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
        }

        private void _AddItemsTocomboBoxFilter() {

            comboBoxFilter.Items.Add("None");
            comboBoxFilter.Items.Add("User ID");
            comboBoxFilter.Items.Add("Person ID");
            comboBoxFilter.Items.Add("Full Name");
            comboBoxFilter.Items.Add("User Name");
            comboBoxFilter.Items.Add("Is Active");

        }

        private void _AddItemsTocbIsActiveFilter() {
            cbIsActiveFilter.Items.Add("All");
            cbIsActiveFilter.Items.Add("Yes");
            cbIsActiveFilter.Items.Add("No");

        }

        // Filter Section

        private void _FilterUser(string FilterColumn, string FilterValue) {

            if (FilterColumn == "Person ID") {

                _FilterNubmer("PersonID", textBoxFilter.Text.Trim());

            }
            else if (FilterColumn == "User ID") {

                _FilterNubmer("UserID", textBoxFilter.Text.Trim());

            }
            else if (FilterColumn == "Full Name") {

                _FilterString("FullName", textBoxFilter.Text.Trim());

            }
            else {
                _FilterString("UserName", textBoxFilter.Text.Trim());
            }

        }

        private void _FilterNubmer(String Column, string FilterText) {

            if (string.IsNullOrEmpty(FilterText)) {
                // Clear the filter if the text box is empty
                _dvUsers.RowFilter = string.Empty;
            }
            else {
                // Escape single quotes to prevent syntax errors
                string safeFilter = FilterText.Replace("'", "''");

                // Convert the numeric UserID column to a String and match partial digits
                _dvUsers.RowFilter = $"CONVERT({Column}, 'System.String') LIKE '{safeFilter}%'";
            }

        }

        private void _FilterBoolean(String Column, byte FilterBool) {

            _dvUsers.RowFilter = $"{Column} = {FilterBool}";
        }

        private void _FilterString(string Column, string FilterText) {

            _dvUsers.RowFilter = $"{Column} LIKE '%{FilterText}%'";

        }

        private void _ResetFilter() {

            _dvUsers.RowFilter = string.Empty;
        }

        // ********* End **********


        private void FrmAddUpdateUser_UserAddedUpdated() {
            _LoadUsersIntoDataGridView();
        }


        // Context Menu stripe Events
        private void addUserToolStripMenuItem_Click(object sender, EventArgs e) {
            frmAddUpdateUser frmaddUser = new frmAddUpdateUser();
            frmaddUser.UserAddedUpdated += FrmAddUpdateUser_UserAddedUpdated;
            frmaddUser.ShowDialog();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e) {

            int userId = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

            frmAddUpdateUser frmUpdateUser = new frmAddUpdateUser(userId);
            frmUpdateUser.UserAddedUpdated += FrmAddUpdateUser_UserAddedUpdated;
            frmUpdateUser.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {

            int userId = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

            if(MessageBox.Show("Are you sure that you wanna delete this User", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {

                if (clsUser.DeleteUser(userId)) {
                    _LoadUsersIntoDataGridView();
                    MessageBox.Show("User Has been deleted Successfully");
                }
                else {
                    MessageBox.Show("Deleting has been Failed");

                }
            }
            else {
                MessageBox.Show("Deleting Process has been Canceld");
            }


        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e) {

            int userId = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

            frmShowUserDetails frmUserDetails = new frmShowUserDetails(userId);
            frmUserDetails.ShowDialog();
        
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e) {
            int userId = (int)dgvUsers.CurrentRow.Cells["UserID"].Value;

            frmChangePassword chagnePassword = new frmChangePassword(userId);
            chagnePassword.ShowDialog();

        }

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e) {


            if(comboBoxFilter.SelectedIndex != 0 && comboBoxFilter.SelectedIndex != 5) {
                textBoxFilter.Visible = true;
                cbIsActiveFilter.Visible = false;

                textBoxFilter.Focus();
            }
            else if(comboBoxFilter.SelectedIndex == 5) {
                cbIsActiveFilter.Visible = true;
                textBoxFilter.Visible = false;

                cbIsActiveFilter.Focus();
            }
            else {
                textBoxFilter.Visible = false;
                cbIsActiveFilter.Visible = false;
            }

        }

        // ********* End **********


        // General Events 
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e) {
            frmAddUpdateUser frmaddUser = new frmAddUpdateUser();
            frmaddUser.UserAddedUpdated += FrmAddUpdateUser_UserAddedUpdated;
            frmaddUser.ShowDialog();
        }

        private void cbIsActiveFilter_SelectedIndexChanged(object sender, EventArgs e) {

            if (cbIsActiveFilter.SelectedIndex == 1) {
                _FilterBoolean("IsActive", 1);
            }
            else if (cbIsActiveFilter.SelectedIndex == 2) {
                _FilterBoolean("IsActive", 0);
            }
            else {
                _ResetFilter();
            }
            
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e) {
            _FilterUser(comboBoxFilter.SelectedItem.ToString(), textBoxFilter.Text.Trim());
        }

        private void dgvUsers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) {
            lblRecords.Text = $"# {RecordsCount.ToString()} Records ";
        }

     

        private void textBoxFilter_KeyPress(object sender, KeyPressEventArgs e) {
            if ((comboBoxFilter.SelectedIndex == 1 || comboBoxFilter.SelectedIndex == 2) && !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) {
                e.Handled = true;//Block the character
            }
        }


        // ********* End **********

    }
}
