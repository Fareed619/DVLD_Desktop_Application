using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_BusinessLogicLayer {

    public class clsLocalDrivingLicenseApplication {

        enum enMode { AddNew = 0, Update = 1}



        enMode _Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        clsApplication Application;
        clsLicenseClass LicenseClass;


        public clsLocalDrivingLicenseApplication() {

            this.LocalDrivingLicenseApplicationID = -1;
            this.ApplicationID = -1;
            this.LicenseClassID = -1;

            this._Mode = enMode.AddNew;

        }


        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID,  int applicationID,  int licenseClassID) {

            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.ApplicationID = applicationID;
            this.LicenseClassID = licenseClassID;


            this.Application = clsApplication.Find(this.ApplicationID);
            this.LicenseClass = clsLicenseClass.Find(this.LicenseClassID);

            this._Mode = enMode.Update;
        }



        static public DataTable ListLocalDrivingLicenseAppsView() {

            return clsLocalDrivingLicenseApplicationsData.ListLocalDrivingLicenseAppsView();
        }

        static public clsLocalDrivingLicenseApplication Find(int localDrivingLicenseApplicationID) {

            int applicationID = -1, licenseClassID = -1;

            bool isFound = clsLocalDrivingLicenseApplicationsData.Find(localDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);

            if (isFound) {

                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID, applicationID, licenseClassID);

            }
            else {
                return null;
            }
            
        }

        private bool _AddNewLocalDrivingLicenseApplication() {

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsData.Insert(this.ApplicationID, this.LicenseClassID);

            if(this.LocalDrivingLicenseApplicationID != -1) {

                this._Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }

        }

        public bool Save() {

            switch (this._Mode) {

                case enMode.AddNew:
                    return _AddNewLocalDrivingLicenseApplication();

                case enMode.Update:
                    return false;

                default:
                    return false;
            }
        }

    }
}
