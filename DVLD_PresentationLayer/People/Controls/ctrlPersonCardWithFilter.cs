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

namespace DVLD_PresentationLayer.People.Controls {

    public partial class ctrlPersonCardWithFilter : UserControl {


        public event Action<int> PersonSelected;
        protected virtual void OnPersonSelected(int PersonID) {

            Action<int> handler = PersonSelected;
            if(handler != null) {
                handler(PersonID);
            }

            // Second Way (Simpler)
            //PersonSelected?.Invoke(PersonID);
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson {
            get {
                return _ShowAddPerson;
            }
            set {
                _ShowAddPerson = value;
                btnAddNew.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled {
            get {
                return _FilterEnabled;
            }
            set {
                _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;


            }
        }

        public int PersonID {
            get {
                return ctrlPersonInformation1.PersonID; 
            }
        }

        public clsPerson SelectedPerson {
            get {
                return ctrlPersonInformation1.SelecedPerson;
            }
        }

        public ctrlPersonCardWithFilter() {

            InitializeComponent();
        }


        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e) {

            cbFilterBy.Items.Add("None");
            cbFilterBy.Items.Add("Person ID");
            cbFilterBy.Items.Add("National No.");

            cbFilterBy.SelectedIndex = 2;
            FilterFocus();


            groupBox1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            gbFilter.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            ctrlPersonInformation1.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);



        }

        public void LoadPersonInfo(int personID) {

            cbFilterBy.SelectedItem = "Person ID";
            txtFilterValue.Text = personID.ToString();
            gbFilter.Enabled = false;

            FindPerson();
        }

        public void ResetPersonInfo() {

            //cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = "";
            ctrlPersonInformation1.SetDefaultInfo();
        }

        private void FindPerson() {
            string FilterValue = txtFilterValue.Text.Trim();
            string SelectedFilter = cbFilterBy.SelectedItem.ToString();

            if (string.IsNullOrEmpty(FilterValue) || SelectedFilter == "None") {
                MessageBox.Show("Please Specify the wnated value");
                return;
            }

            switch (SelectedFilter) {

                case "Person ID":

                    ctrlPersonInformation1.LoadPersonInfo(int.Parse(FilterValue));
                    break;

                case "National No.":

                    ctrlPersonInformation1.LoadPersonInfo(FilterValue);
                    break;

                default:
                    return;
            }

            if (PersonID != -1 && _FilterEnabled) {
                OnPersonSelected(PersonID);
            }

        }


        private void PersonSavedHandler(int PersonID) {

            ctrlPersonInformation1.LoadPersonInfo(PersonID);
            cbFilterBy.SelectedItem = "Person ID";
            txtFilterValue.Text = PersonID.ToString();

            if (PersonID != -1) {
                OnPersonSelected(PersonID);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) {

            FindPerson();


        }
  
        private void btnAddNew_Click(object sender, EventArgs e) {

            frmAddUpdatePerson frmAddPerson = new frmAddUpdatePerson();

            frmAddPerson.PersonSaved += PersonSavedHandler;

            frmAddPerson.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e) {
            if(cbFilterBy.SelectedIndex != 0) {
                txtFilterValue.Focus();
            }
        }

        public void FilterFocus() {

            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e) {

            // Check if the pressed key is specifically Enter
            if (e.KeyChar == (char)Keys.Return) {
                FindPerson();

            }
        }
    }
}
