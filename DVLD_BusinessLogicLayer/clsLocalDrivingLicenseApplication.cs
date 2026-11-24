using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLogicLayer {

    public class clsLocalDrivingLicenseApplication : clsApplication{

        public enum enMode { AddNew = 0, Update = 1 }

        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClass LicenseClass;


        public clsLocalDrivingLicenseApplication() {

            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }



        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID, int licenseClassID, int appID, DateTime appDate, DateTime lastStatusDate, enAppStatus appStatus, float fees, enApplicationType appTypeID, int applicantPersonID, int createdByUserID ):base(appID, appDate, lastStatusDate, appStatus, fees, appTypeID, applicantPersonID, createdByUserID) {

            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.LicenseClassID = licenseClassID;


            this.LicenseClass = clsLicenseClass.Find(this.LicenseClassID);

            Mode = enMode.Update;
        }



        static public DataTable ListLocalDrivingLicenseAppsView() {

            return clsLocalDrivingLicenseApplicationsData.ListLocalDrivingLicenseAppsView();
        }

        static public DataTable FilterBy(string ColumnName, string Value) {

            return clsLocalDrivingLicenseApplicationsData.FilterBy(ColumnName, Value);
        }

        static public DataTable FilterBy(string ColumnName, int Value) {

            return clsLocalDrivingLicenseApplicationsData.FilterBy(ColumnName, Value);
        }

        static public clsLocalDrivingLicenseApplication FindByLocalDrivingLicenseID(int localDrivingLicenseApplicationID) {

            int applicationID = -1, licenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationByID(localDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);

            clsApplication application = clsApplication.Find(applicationID);

            if (isFound && application != null) {

                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, licenseClassID, application.ApplicationID, application.ApplicationDate, application.LastStatusDate, application.ApplicationStatus, application.PaidFees, application.ApplicationTypeID, application.ApplicantPersonID, application.CreatedByUserID);

            }
            else {
                return null;
            }
            
        }

        static public clsLocalDrivingLicenseApplication FindByApplicationID(int applicationID) {

            int localDrivingLicesneID = -1, licenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationsData.GetLocalDrivingLicenseApplicationByApplicationID(applicationID, ref localDrivingLicesneID, ref licenseClassID);

            clsApplication application = clsApplication.Find(applicationID);

            if (isFound && application != null) {

                return new clsLocalDrivingLicenseApplication(localDrivingLicesneID, licenseClassID, application.ApplicationID, application.ApplicationDate, application.LastStatusDate, application.ApplicationStatus, application.PaidFees, application.ApplicationTypeID, application.ApplicantPersonID, application.CreatedByUserID);

            }
            else {
                return null;
            }
            
        }

        public bool DeleteLocalDrivingApplication() {


            if (!clsLocalDrivingLicenseApplicationsData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID)) return false;

            if (!base.Delete()) return false;

            return true;
        }

        private bool _AddNewLocalDrivingLicenseApplication() {

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);

            if(this.LocalDrivingLicenseApplicationID != -1) {

                this.Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }

        }

        private bool _UpdateLocalDrivingLicenseApplication() {

            return clsLocalDrivingLicenseApplicationsData.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }


        public bool SaveLocalLicense() {

            base.Mode = (clsApplication.enMode)Mode;

            // Saves application ID
            if (!base.SaveApplication()) {
                return false;
            }

            switch (this.Mode) {

                case enMode.AddNew:
                    return _AddNewLocalDrivingLicenseApplication();

                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();

                default:
                    return false;
            }
        }



    }
}
