using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Forms;
using DVLD_PresentationLayer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.Controls {

    public partial class ctrlPersonInformation : UserControl {


        public event Action WorkCompleted;


        private clsPerson _Person;
        private int _PersonID = -1;


        public int PersonID {
            get {
                return _PersonID;
            }
        }

        public clsPerson SelecedPerson {
            get {
                return _Person;
            }
        }

        public ctrlPersonInformation() {

            InitializeComponent();
        }

        private void ctrlPersonInformation_Load(object sender, EventArgs e) {
            _SetDefaultInfo();
        }

        public void LoadPersonInfo(int ID) {

            _PersonID = ID;
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null) {
                _SetDefaultInfo();
                MessageBox.Show("Person is not found");
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblEmail.Text = _Person.Email;



            lblCountry.Text = _Person.CountryInfo.CountryName;

            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;

            if (_Person.Gender == 0) {
                lblGender.Text = "Male";
                picBoxProfilePhoto.Image = Resources.Male_512;
                pbGender.Image = Resources.man;

            }
            else {
                lblGender.Text = "Female";
                picBoxProfilePhoto.Image = Resources.Female_512;
                pbGender.Image = Resources.woman;

            }

            if (!string.IsNullOrEmpty(_Person.ImagePath)) {

                if (File.Exists(_Person.ImagePath)) {
                    picBoxProfilePhoto.ImageLocation = _Person.ImagePath;
                }
                else {
                    MessageBox.Show("Image is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }



        }

        public void LoadPersonInfo(string NationalNo) {

            _Person = clsPerson.Find(NationalNo);
           

            if (_Person == null) {
                MessageBox.Show("Person is not found");
                return;
            }

            _PersonID = _Person.PersonID;

            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd/MM/yyyy");
            lblEmail.Text = _Person.Email;



            lblCountry.Text = _Person.CountryInfo.CountryName;

            lblAddress.Text = _Person.Address;
            lblPhone.Text = _Person.Phone;

            if (_Person.Gender == 0) {
                lblGender.Text = "Male";
                picBoxProfilePhoto.Image = Resources.Male_512;
                pbGender.Image = Resources.man;

            }
            else {
                lblGender.Text = "Female";
                picBoxProfilePhoto.Image = Resources.Female_512;
                pbGender.Image = Resources.woman;

            }

            if (!string.IsNullOrEmpty(_Person.ImagePath)) {

                if (File.Exists(_Person.ImagePath)) {
                    picBoxProfilePhoto.ImageLocation = _Person.ImagePath;
                }
                else {
                    MessageBox.Show("Image is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }



        }

        private void _SetDefaultInfo() {

            _PersonID = -1;

            lblPersonID.Text = "[???]";
            lblNationalNo.Text = "[???]";
            lblName.Text = "[???]";
            lblDateOfBirth.Text = "[???]";
            lblEmail.Text = "[???]";


            lblCountry.Text = "[???]";

            lblAddress.Text = "[???]";
            lblPhone.Text = "[???]";

            lblGender.Text = "Male";
            picBoxProfilePhoto.Image = Resources.Male_512;
            pbGender.Image = Resources.man;
          
        }

        private void PersonSavedHandler(int PersonID) {
            LoadPersonInfo(PersonID);
            WorkCompleted?.Invoke();
        }


        private void linklblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {

            if(_Person == null) {
                MessageBox.Show("There is no selected Person");
                return;
            }


            frmAddUpdatePerson frmUpdatePerson = new frmAddUpdatePerson(_Person.PersonID);
            frmUpdatePerson.PersonSaved += PersonSavedHandler;
            frmUpdatePerson.ShowDialog();

        }


    }
}
