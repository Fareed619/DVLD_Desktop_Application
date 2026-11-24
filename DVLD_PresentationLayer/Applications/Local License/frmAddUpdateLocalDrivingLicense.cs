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

namespace DVLD_PresentationLayer.Applications.Local_License {

    public partial class frmAddUpdateLocalDrivingLicense : Form {

        // Event 
        public event Action LocalApplicationSaved;
        private void OnUserUpdated() {
            LocalApplicationSaved?.Invoke();
        }

        enum enMode { AddNew = 1, Update = 2}



        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID = -1;
        private int _PersonID = -1;

        enMode Mode = enMode.AddNew;


        public frmAddUpdateLocalDrivingLicense() {
            InitializeComponent();

            Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicense(int localApplicationID) {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = localApplicationID;
            Mode = enMode.Update;

        }


        private void frmAddUpdateLocalDrivingLicense_Load(object sender, EventArgs e) {

            _SetTabControlFontSettings();
            _LoadLicesneClassesIntoComboBox();

            if(Mode == enMode.AddNew) {

                this._LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                btnNext.Enabled = false;
                btnSave.Enabled = false;

                _ResetDefaultValues();
            }
            else {
                this._LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(_LocalDrivingLicenseApplicationID);

                if(_LocalDrivingLicenseApplication == null) {
                    MessageBox.Show("local Driving License Application is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                _FillExistingInfoIntoForm();
            }
     

        }

        private void _ResetDefaultValues() {


            ctrlPersonCardWithFilter1.ResetPersonInfo();
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense).ApplicationTypeFees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.User.UserName;
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblMain.Text = "New Local Driving License Application";

            cbLicenseClass.SelectedValue = 3; // set the class Name that its licenseClassID = 3
        }

        private void _PraperLocalApplicationInfo() {

            if (_PersonID == -1) {
                MessageBox.Show("Perosn is not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseApplication.ApplicationType = clsApplicationType.Find((int)clsApplication.enApplicationType.NewDrivingLicense);

            _LocalDrivingLicenseApplication.ApplicationTypeID = clsApplication.enApplicationType.NewDrivingLicense;
            _LocalDrivingLicenseApplication.PaidFees = Convert.ToSingle(lblFees.Text);


            _LocalDrivingLicenseApplication.User = clsUser.Find(lblCreatedBy.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = _LocalDrivingLicenseApplication.User.UserID;

            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            _LocalDrivingLicenseApplication.Applicant = clsPerson.Find(ctrlPersonCardWithFilter1.PersonID);

            _LocalDrivingLicenseApplication.ApplicationDate = Convert.ToDateTime(lblAppDate.Text);
            _LocalDrivingLicenseApplication.ApplicationStatus = clsApplication.enAppStatus.New;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;

            _LocalDrivingLicenseApplication.LicenseClassID = (int)cbLicenseClass.SelectedValue;
        }

        private void _FillExistingInfoIntoForm() {

            lblMain.Text = "Update Local Driving License Application";

            ctrlPersonCardWithFilter1.FilterEnabled = false;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingLicenseApplication.ApplicantPersonID);

            lblDLApplicationID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblCreatedBy.Text = _LocalDrivingLicenseApplication.User.UserName;
            lblAppDate.Text = _LocalDrivingLicenseApplication.ApplicationDate.ToShortDateString();

            cbLicenseClass.SelectedValue = _LocalDrivingLicenseApplication.LicenseClassID; // set the class Name that its licenseClassID = 3
        }

        private void _SetTabControlFontSettings() {
            // Save the original default font
            Font defaultFont = this.Font;

            // Set the custom font for the tab headers
            tabControl.Font = new Font("Segoe UI", 12F, FontStyle.Bold);

            // Re-apply the default font to each TabPage so child controls inherit it
            foreach (TabPage page in tabControl.TabPages) {
                page.Font = defaultFont;
            }
        }

        private void _LoadLicesneClassesIntoComboBox() {

            cbLicenseClass.DataSource = clsLicenseClass.ListLicenseClasses();

            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";

        }

        private bool _CheckIfPersonHasActiveApplication() {
            // 3 - Condition => Prevent the person to create more than one local driving license that has the same license class and the same status.

            int applicationID = clsLocalDrivingLicenseApplication.GetActiveApplicationIDForLicenseClass(_PersonID, (int)cbLicenseClass.SelectedValue, _LocalDrivingLicenseApplication.ApplicationTypeID);

            if (applicationID != -1) {
                MessageBox.Show($"Choose another license class, the selected person already has an active application with id = {applicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                cbLicenseClass.Focus();
                return true;
            }
            else {
                return false;
            }

        }

        private void ctrlPersonCardWithFilter1_PersonSelected(int obj) {
            _PersonID = obj;
            btnNext.Enabled = true;
            btnSave.Enabled = true;
        }




        private void btnNext_Click(object sender, EventArgs e) {

            tabControl.SelectedTab = tabPageApplicationInfo;
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {

            _PraperLocalApplicationInfo();

            if (_CheckIfPersonHasActiveApplication()) return;

            // TODO
            // check if user already has issued license


            //  - Add Local application
            if (_LocalDrivingLicenseApplication.SaveLocalLicense()) {

                OnUserUpdated();
                this.Mode = enMode.Update;
                lblMain.Text = "Update Local Driving License Application";
                lblDLApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                ctrlPersonCardWithFilter1.FilterEnabled = false;

                MessageBox.Show("Local Driving License Application has been created successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                MessageBox.Show("Local Driving License Application Creation has been failed", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void frmAddUpdateLocalDrivingLicense_Activated(object sender, EventArgs e) {

            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
