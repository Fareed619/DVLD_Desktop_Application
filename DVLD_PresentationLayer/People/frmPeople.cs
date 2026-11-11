using DVLD_BusinessLogicLayer;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_PresentationLayer
{
    public partial class frmPeople : Form {

     
        private int PeopleCount {
            get {
                return dgvPeople.Rows.Count;
            }
        }

        public frmPeople() {

            InitializeComponent();
        }


        private void frmPeople_Load(object sender, EventArgs e) {

            _LoadPeopleIntoDataGridView();   // Direct Filter
            _AddItemsToComboBoxFilter();
        }
 

        // Read People Info
        private void _LoadPeopleIntoDataGridView() {
            // Load the list of people into the DataGridView          
            dgvPeople.DataSource = clsPerson.ListPeopleView();
        }

        // Direct Filter
        private void _FilterDataGridView() {

            string ColumnName = comboBoxFilter.SelectedItem?.ToString();
            string FilterStringValue = textBoxFilter.Text.Trim();

            if (string.IsNullOrEmpty(FilterStringValue)) {

                _LoadPeopleIntoDataGridView();
                return;
            }


            dgvPeople.DataSource = clsPerson.FilterBy(ColumnName, FilterStringValue);


        }

        private void _AddItemsToComboBoxFilter() {

            // Add items to the ComboBox for filtering
            comboBoxFilter.Items.Add("None");
            comboBoxFilter.Items.Add("PersonID");
            comboBoxFilter.Items.Add("NationalNo");
            comboBoxFilter.Items.Add("FirstName");
            comboBoxFilter.Items.Add("SecondName");
            comboBoxFilter.Items.Add("ThirdName");
            comboBoxFilter.Items.Add("LastName");
            comboBoxFilter.Items.Add("Gender");
            comboBoxFilter.Items.Add("DateOfBirth");
            comboBoxFilter.Items.Add("Nationality");
            comboBoxFilter.Items.Add("Phone");
            comboBoxFilter.Items.Add("Email");

            comboBoxFilter.SelectedIndex = 0;
            
        }


        // Event Handlers   

        private void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e) {

            // Get the selected item
            string SelectedFilter = comboBoxFilter.SelectedItem?.ToString();
            textBoxFilter.Visible = (SelectedFilter != "None");

            if(textBoxFilter.Visible) textBoxFilter.Focus();         
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e) {

            _FilterDataGridView(); // Direct Filter

        }


        // Allow Only Numbers when Person ID is seleected as the filter
        private void textBoxFilter_KeyPress(object sender, KeyPressEventArgs e) {

            string SelectedFilter = comboBoxFilter.SelectedItem?.ToString();
            if(SelectedFilter == "PersonID" ) {

                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true; // Cancels the key press
                }
            }


        }


        private void dataGridView1_DataSourceChanged(object sender, EventArgs e) {

            // Update the label with the count of people in the DataGridView
            lblRecords.Text = $"#Records: {PeopleCount}";
        }

        // Custom Button to close the form 
        private void button1_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void PersonSavedHandler(int PersonID) {
            _LoadPeopleIntoDataGridView();
        }

        private void PersonUpdated() {
            _LoadPeopleIntoDataGridView();
        }

        // Add New Person
        private void btnAddPerson_Click(object sender, EventArgs e) {

            frmAddUpdatePerson addPersonForm = new frmAddUpdatePerson();
            addPersonForm.PersonSaved += PersonSavedHandler;
            addPersonForm.ShowDialog();

          

        }
    
        // Update Person
        private void editToolStripMenuItem_Click(object sender, EventArgs e) {

            int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

            frmAddUpdatePerson frmUpdatePerson = new frmAddUpdatePerson(PersonID);
            frmUpdatePerson.PersonSaved += PersonSavedHandler;
            frmUpdatePerson.ShowDialog();

           

        }

        // Delete Person
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) {

            if(MessageBox.Show("Are you sure that you want to delete this person?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) {
                return;
            }


            int PersonID = dgvPeople.CurrentRow.Cells["PersonID"].Value != null ? Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value) : -1;

            if (clsPerson.Delete(PersonID)) {

                MessageBox.Show("Person deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _LoadPeopleIntoDataGridView();
            }
            else
            {
                MessageBox.Show("Failed to delete person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
    }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e) {

            int PersonID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

            frmPersonDetails personDetails = new frmPersonDetails(PersonID);
            personDetails.PersonUpdateCompleted += PersonUpdated;
            personDetails.ShowDialog();
        }

       
    }
}
