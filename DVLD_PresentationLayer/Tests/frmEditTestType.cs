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

namespace DVLD_PresentationLayer.Tests {
    public partial class frmEditTestType : Form {

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        private clsTestType _TestType;

        public event Action InfoUpdated;

        private void _OnInfoUpdated() {

            InfoUpdated?.Invoke();
        }



        public frmEditTestType(clsTestType.enTestType testTypeId) {
            InitializeComponent();

            _TestTypeID = testTypeId;
        }

        private void frmEditTestType_Load(object sender, EventArgs e) {

            _TestType = clsTestType.Find(_TestTypeID);

            if (_TestType == null) {

                MessageBox.Show("Test Type is not found", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            _FillTestTypeInfo();
        }

        private void _FillTestTypeInfo() {

            txtID.Text = Convert.ToInt32(_TestType.TestTypeID).ToString();
            txtTitle.Text = _TestType.TestTypeTitle;
            txtDescription.Text = _TestType.TestTypeDescription;
            txtFees.Text = _TestType.TestTypeFees.ToString();

            txtTitle.Focus();
        }

        private void _SetErrorProvider(Control control, string message) {

            errorProvider1.SetError(control, message);
        }

        private bool _IsControlRequired(Control control, CancelEventArgs e) {

            if (string.IsNullOrWhiteSpace(control.Text.Trim())) {

                e.Cancel = true;
                _SetErrorProvider(control, $"{control.Tag} is Required");
                return false;

            }
            else {
                _SetErrorProvider(control, string.Empty);
                return true;

            }

        }

        private bool IsNumber(string input) {
            return float.TryParse(input, out _);
        }



        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }


        private void txtTitle_Validating(object sender, CancelEventArgs e) {
            _IsControlRequired(txtTitle, e);
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e) {
            _IsControlRequired(txtDescription, e);

        }

        private void txtFees_Validating(object sender, CancelEventArgs e) {
            _IsControlRequired(txtFees, e);

            // IsNumber 
            if (!IsNumber(txtFees.Text)) {
                e.Cancel = true;
                _SetErrorProvider(txtFees, $"Value Must be a number");
            }
            else {
                _SetErrorProvider(txtFees, "");

            }

        }


        private void btnSave_Click(object sender, EventArgs e) {
            if (!this.ValidateChildren()) {
                MessageBox.Show("All Fields Are Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string title = txtTitle.Text;
            string Description = txtDescription.Text;
            float fees = Convert.ToSingle(txtFees.Text);
            



            _TestType.TestTypeTitle = title;
            _TestType.TestTypeDescription = Description;
            _TestType.TestTypeFees = fees;

            if (_TestType.Save()) {

                _OnInfoUpdated();
                MessageBox.Show("Application Type Info has been Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Something went wrong, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
