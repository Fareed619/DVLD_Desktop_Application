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

namespace DVLD_PresentationLayer.Forms
{
    public partial class frmAddUpdatePerson : Form {

        public event Action<int> PersonSaved;

        protected virtual void OnPersonSaved(int PersonID) {

            PersonSaved?.Invoke(PersonID);
        }
        

        enum enMode { AddNew = 1, Update = 2};
        enMode _Mode = enMode.AddNew;



        clsPerson Person = null;
        private int _PersonID = -1;


        // AddNew Mode
        public frmAddUpdatePerson() {

            InitializeComponent();
            _Mode = enMode.AddNew;

            ctrlAddUpdatePerson1.CloseForm += CloseForm;
            ctrlAddUpdatePerson1.SavedWarning += PersonUpdated;
        }


        // Update Mode
        public frmAddUpdatePerson(int PersonID) {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;
            ctrlAddUpdatePerson1.CloseForm += CloseForm;
            ctrlAddUpdatePerson1.SavedWarning += PersonUpdated;
        }


        private void frmAddUpdatePerson_Load(object sender, EventArgs e) {

            if(this._Mode == enMode.Update) {

                clsPerson PersonInfo = clsPerson.Find(_PersonID);

                if (PersonInfo == null) {

                    MessageBox.Show("Person not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.DialogResult = DialogResult.Cancel;
                    return;
                }

                Person = PersonInfo;

                txtPersonID.Text = Person.PersonID.ToString();
                ctrlAddUpdatePerson1.FillPersonInfoIntoCtrl(Person);
            }
            else {
                Person = new clsPerson();
                ctrlAddUpdatePerson1.SetDefaultPerson(Person);
            }

            ReNameMainLabel();

        }


        private void ReNameMainLabel() { 
            if (this._Mode == enMode.AddNew)
            {
                lblMain.Text = "Add New Person";
            }
            else
            {
                lblMain.Text = "Update Person";
            }

        }
    

        private void CloseForm(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
        }

        private void PersonUpdated(object sender, EventArgs e) {

            _PersonID = Person.PersonID;
            txtPersonID.Text = _PersonID.ToString();
            lblMain.Text = "Update Person";

            OnPersonSaved(_PersonID);



        }

    }
}
