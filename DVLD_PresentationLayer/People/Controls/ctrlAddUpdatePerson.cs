using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Classes;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_PresentationLayer.Controls {

    public partial class ctrlAddUpdatePerson : UserControl {

        public event EventHandler CloseForm;
        public event EventHandler SavedWarning;



        enum enMode { AddNew = 1, Update = 2 };
        enMode _Mode = enMode.AddNew;

        private clsPerson _Person = null;

        // existing in the database
        private string _OldImagePath = "";
        private string _ImagePath = "";

        public ctrlAddUpdatePerson() {

            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        private void ctrlAddUpdatePerson_Load(object sender, EventArgs e) {

            // Date Of Birth Boundries
            _SetLimitsToDateOfBirth(18, 90);
          
            // Load all countries into the combobox
            _LoadCountriesIntoComboBox();

            // Syria is the default country
            //cmbCountry.SelectedIndex = cmbCountry.FindString("Syria") ;
            cmbCountry.SelectedValue = 169; 
        }

        public void FillPersonInfoIntoCtrl(clsPerson PersonInfo) {
            this._Mode = enMode.Update;
            _Person = PersonInfo;

            txtFirstName.Text = PersonInfo.FirstName;
            txtSecondName.Text = PersonInfo.SecondName;
            txtThirdName.Text = PersonInfo.ThirdName;
            txtLastName.Text = PersonInfo.LastName;

            txtNationalNo.Text = PersonInfo.NationalNo;
            txtNationalNo.ReadOnly = true;

            txtPhone.Text = PersonInfo.Phone;
            txtEmail.Text = PersonInfo.Email;
            txtAddress.Text = PersonInfo.Address;

            dateTimePicker1.Value = PersonInfo.DateOfBirth;
            cmbCountry.SelectedValue = PersonInfo.NationalityCountryID;

            if (PersonInfo.Gender == 0)
                rbMale.Checked = true;
            else
                rbFemale.Checked = true;

            _OldImagePath = PersonInfo.ImagePath;
            _ImagePath = _OldImagePath;

            if (!string.IsNullOrEmpty(_ImagePath) && File.Exists(_ImagePath)) { 
                pictureBoxProfilePhoto.ImageLocation = _ImagePath;
                linklblRemoveImage.Visible = true;
            }


        }

        public void SetDefaultPerson(clsPerson Person) {
            _Person = Person;
        }

        private void _LoadCountriesIntoComboBox()
        {

            cmbCountry.DataSource = clsCountry.GetAllCountries();
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";

        }

        private void _SetLimitsToDateOfBirth(int MinYear, int MaxYear) {

            //DateTime Before18Years = new DateTime(DateTime.Now.Year - MinYear, DateTime.Now.Month, DateTime.Now.Day);
            //DateTime Before90Years = new DateTime(DateTime.Now.Year - MaxYear, DateTime.Now.Month, DateTime.Now.Day);

            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.MinDate = DateTime.Now.AddYears(-90);

            dateTimePicker1.Value = dateTimePicker1.MaxDate;
        }

        private void _SetImageBasedOnGender() {

            if (rbMale.Checked == true) {
                pictureBoxProfilePhoto.Image = Properties.Resources.male;
            }
            else {
                pictureBoxProfilePhoto.Image = Properties.Resources.female;
            }
        }

        private void SetErrorProvider(Control control, string message) {

            errorProvider1.SetError(control, message);
        }

        private bool IsControlRequired(Control control, CancelEventArgs e){

            if (string.IsNullOrWhiteSpace(control.Text.Trim()))
            {

                e.Cancel = true;
                SetErrorProvider(control, $"{control.Tag} is Required");
                return false;

            }
            else
            {
                SetErrorProvider(control, string.Empty);
                return true;
           
            }

        }

        private void DoesNationalNoExists(Control control, CancelEventArgs e) {

 
             if (this._Mode == enMode.AddNew && clsPerson.Exists(control.Text))
            {

                e.Cancel = true;
                SetErrorProvider(control, "National Number EXISTS, try another one");
            }
            else if(this._Mode == enMode.Update && !clsPerson.Exists(control.Text)) {

                e.Cancel = true;
                SetErrorProvider(control, "National Number Doesn't match the an exsisting one.");
            }
            else
            {
                SetErrorProvider(control, string.Empty);
            }

        }

        private string GetImagePath()
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {

                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    return ofd.FileName;
                }
            }
            return string.Empty;
        }

        private string SaveImageInFolder(string SourcePath) {

            string DestinationFolder = @"C:\Users\User\Desktop\FareedDVLD\ProfileImages\";

            if (!Directory.Exists(DestinationFolder))
                Directory.CreateDirectory(DestinationFolder);

            string Extension = Path.GetExtension(SourcePath);

            string NewFileName = clsUtils.GenerateNewGuid() + Extension;

            string DestinationPath = Path.Combine(DestinationFolder, NewFileName);

            File.Copy(SourcePath, DestinationPath);

            return DestinationPath;
        }

        private void HandleDeleteOldImage() {

            if (!string.IsNullOrEmpty(_OldImagePath) && File.Exists(_OldImagePath)) {

                File.Delete(_OldImagePath);
            }

            _OldImagePath = _ImagePath;

        }


        // Events to validate the Inputs
        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {

            IsControlRequired(txtFirstName, e);
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            IsControlRequired(txtSecondName, e);

        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            IsControlRequired(txtLastName, e);

        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            IsControlRequired(txtPhone, e);
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            IsControlRequired(txtAddress, e);
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e) {

            if (!IsControlRequired(txtNationalNo, e)) return;

            DoesNationalNoExists(txtNationalNo, e);

        }
           
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            string Email = txtEmail.Text.Trim();

            if (!string.IsNullOrWhiteSpace(Email) && !clsValidation.ValidateEmail(Email))
            {

                e.Cancel = true;
                SetErrorProvider(txtEmail, "Invalid email format");
            }
            else
            {
                SetErrorProvider(txtEmail, string.Empty);
            }

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            _SetImageBasedOnGender();
          
        }

        private void linklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


            _ImagePath = GetImagePath();

            if (!string.IsNullOrEmpty(_ImagePath.Trim()))
            {
                pictureBoxProfilePhoto.ImageLocation = _ImagePath;
                linklblRemoveImage.Visible = true;

            }
        }

        private void linklblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            _ImagePath = string.Empty;
            pictureBoxProfilePhoto.ImageLocation = _ImagePath;
            linklblRemoveImage.Visible = false;

            _SetImageBasedOnGender();

        }

        private void btnSave_Click(object sender, EventArgs e) {


            if (!this.ValidateChildren()) {
                MessageBox.Show("Please fix the errors on the form before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Stop here if anything is invalid
            }

            _ImagePath = SaveImageInFolder(_ImagePath);

            _Person.NationalNo = txtNationalNo.Text.Trim();
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.Gender = rbMale.Checked ? (byte)0 : (byte)1;
            _Person.Address = txtAddress.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.ImagePath = _ImagePath;
            _Person.NationalityCountryID = (int)cmbCountry.SelectedValue;


            if(_Person.Save()) {             

                HandleDeleteOldImage();
                _Mode = enMode.Update;
                txtNationalNo.ReadOnly = true;


                SavedWarning?.Invoke(this, e);
                MessageBox.Show("Person has been Saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else {
                MessageBox.Show("Unexpected behaviour happend.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }



        }

        private void btnClose_Click(object sender, EventArgs e) {

            CloseForm?.Invoke(this, e);
        }

      
    }
}