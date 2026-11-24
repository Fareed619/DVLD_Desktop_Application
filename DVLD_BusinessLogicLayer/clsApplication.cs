using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_BusinessLogicLayer.clsTestType;

namespace DVLD_BusinessLogicLayer {

    public class clsApplication {

        static private clsApplication MapDTOToApplication(clsApplicationDTO AppDTO) {

            return new clsApplication(
                appID: AppDTO.ApplicationID,
                appDate: AppDTO.ApplicationDate,
                lastStatusDate: AppDTO.LastStatusDate,
                appStatus: (enAppStatus)AppDTO.ApplicationStatus,
                fees: AppDTO.PaidFees,

                appTypeID:(enApplicationType) AppDTO.ApplicationTypeID,
                applicantPersonID: AppDTO.ApplicantPersonID,
                createdByUserID: AppDTO.CreatedByUserID

            );
        }

        static private clsApplicationDTO MapApplicationToDTO(clsApplication App) {

            return new clsApplicationDTO(

               appID: App.ApplicationID,
               appDate: App.ApplicationDate,
               lastStatusDate: App.LastStatusDate,
               appStatus: (byte)App.ApplicationStatus,
               fees: App.PaidFees,

               appTypeID: (int) App.ApplicationTypeID,
               applicantPersonID: App.ApplicantPersonID,
               createdByUserID: App.CreatedByUserID

           );

        }


        public enum enMode { AddNew = 0, Update = 1}
        public enum enAppStatus { New = 1, Cancelled = 2, Completed = 3}

        public enum enApplicationType {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3, ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enMode Mode;

        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate  { get; set; }
        public float PaidFees { get; set; }
        public enAppStatus ApplicationStatus { get; set; }

        public string ApplicationStatusText {

            get {
                switch (ApplicationStatus) {

                    case enAppStatus.New:
                        return "New";

                    case enAppStatus.Cancelled:
                        return "Cancelled";

                    case enAppStatus.Completed:
                        return "Completed";

                    default:
                        return "Unkown";

                }
            }
        }

        public enApplicationType ApplicationTypeID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int CreatedByUserID   { get; set; }

        public clsApplicationType ApplicationType;
        public clsPerson Applicant;
        public clsUser User;


        public clsApplication() {

            this.ApplicationID = -1;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.Now;
            this.ApplicationStatus = enAppStatus.New;
            this.PaidFees = 0;

            this.ApplicationTypeID = enApplicationType.NewDrivingLicense;
            this.ApplicantPersonID = -1;
            this.CreatedByUserID = -1;

            this.ApplicationType = null;
            this.Applicant = null;
            this.User = null;

            this.Mode = enMode.AddNew;
        }

        protected clsApplication(int appID, DateTime appDate, DateTime lastStatusDate, enAppStatus appStatus, float fees, enApplicationType appTypeID, int applicantPersonID, int createdByUserID) {


            this.ApplicationID = appID;
            this.ApplicationDate = appDate;
            this.LastStatusDate = lastStatusDate;
            this.ApplicationStatus = appStatus;
            this.PaidFees = fees;

            this.ApplicationTypeID = appTypeID;
            this.ApplicantPersonID = applicantPersonID;
            this.CreatedByUserID = createdByUserID;

            this.ApplicationType = clsApplicationType.Find((int)appTypeID);
            this.Applicant = clsPerson.Find(applicantPersonID);
            this.User = clsUser.Find(createdByUserID);


            this.Mode = enMode.Update;
        }



        static public clsApplication Find(int AppID) {

            clsApplicationDTO AppDTO = clsApplicationData.Find(AppID);

            if(AppDTO != null) {

                return MapDTOToApplication(AppDTO);
            }
            else{
                return null;
            }
        }


        static public DataTable ListApplications() {

            return clsApplicationData.ListApplications();
        }

        public bool Cancel() {

            return clsApplicationData.UpdateStatus(this.ApplicationID, 2);
        }

        public bool SetComplete() {

            return clsApplicationData.UpdateStatus(this.ApplicationID, 3);
        }

        public bool Delete() {

            return clsApplicationData.Delete(this.ApplicationID);
        }


        static public int GetActiveApplicationIDForLicenseClass(int PersonID, int LicenseClassID, enApplicationType AppTypeID) {
            // New Status = 1
            return clsApplicationData.GetApplicationIDForLicenseClass(PersonID, LicenseClassID, (int)AppTypeID, 1);
        }


        private bool _AddNewApplication() {

            this.ApplicationID = clsApplicationData.Insert(MapApplicationToDTO(this));

            if (this.ApplicationID != -1) {

                this.Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }




        }

        private bool _UpdateApplication() {

            return clsApplicationData.Update(MapApplicationToDTO(this));
        }

        public bool SaveApplication() {

            switch (this.Mode) {

                case enMode.AddNew:
                    return _AddNewApplication();

                case enMode.Update:
                    return _UpdateApplication();

                default:
                    return false;
            }
        }
    }
}
