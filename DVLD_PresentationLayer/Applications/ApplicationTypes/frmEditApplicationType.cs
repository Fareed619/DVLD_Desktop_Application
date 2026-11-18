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
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_PresentationLayer.Applications.ApplicationTypes {

    public partial class frmEditApplicationType : Form {

        private int _ApplicationTypeID = -1;
        private clsApplicationType _ApplicationType;

        public event Action InfoUpdated;

        private void _OnInfoUpdated() {

            InfoUpdated?.Invoke();
        }



        public frmEditApplicationType(int ApplicationTypeID) {
            InitializeComponent();

            _ApplicationTypeID = ApplicationTypeID;
        }


        private void frmEditApplicationType_Load(object sender, EventArgs e) {

            _ApplicationType = clsApplicationType.Find(_ApplicationTypeID);

            if (_ApplicationType == null) {

                MessageBox.Show("Application Type is not found", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _FillApplicationTypeInfo();
        }


        private void _FillApplicationTypeInfo() {

            txtID.Text = _ApplicationType.ApplicationTypeID.ToString();
            txtTitle.Text = _ApplicationType.ApplicationTypeTitle;
            txtFees.Text = _ApplicationType.ApplicationTypeFees.ToString();

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

        private void btnSave_Click(object sender, EventArgs e) {

            if (!this.ValidateChildren()) {
                MessageBox.Show("All Fields Are Required", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string title = txtTitle.Text;
            float fees = Convert.ToSingle(txtFees.Text);



            _ApplicationType.ApplicationTypeTitle = title;
            _ApplicationType.ApplicationTypeFees = fees;

            if (_ApplicationType.Save()) {

                _OnInfoUpdated();
                MessageBox.Show("Application Type Info has been Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Something went wrong, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void txtTitle_Validating(object sender, CancelEventArgs e) {
            _IsControlRequired(txtTitle, e);
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
    }
}
