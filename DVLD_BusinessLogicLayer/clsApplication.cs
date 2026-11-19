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

                appTypeID: AppDTO.ApplicationTypeID,
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

               appTypeID: App.ApplicationTypeID,
               applicantPersonID: App.ApplicantPersonID,
               createdByUserID: App.CreatedByUserID

           );

        }


        public enum enMode { AddNew = 0, Update = 1}
        public enum enAppStatus { New = 1, Cancelled = 2, Completed = 3}

        public enMode _Mode;

        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate  { get; set; }
        public enAppStatus ApplicationStatus { get; set; }
        public float PaidFees { get; set; }

        public int ApplicationTypeID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int CreatedByUserID   { get; set; }

        public clsApplicationType ApplicationType;
        public clsPerson Applicant;
        public clsUser User;


        public clsApplication() {

            this.ApplicationID = -1;
            this.ApplicationDate = DateTime.Now;
            this.LastStatusDate = DateTime.MinValue;
            this.ApplicationStatus = enAppStatus.New;
            this.PaidFees = 0;

            this.ApplicationTypeID = -1;
            this.ApplicantPersonID = -1;
            this.CreatedByUserID = -1;

            this.ApplicationType = null;
            this.Applicant = null;
            this.User = null;

            this._Mode = enMode.AddNew;
        }

        private clsApplication(int appID, DateTime appDate, DateTime lastStatusDate, enAppStatus appStatus, float fees, int appTypeID, int applicantPersonID, int createdByUserID) {


            this.ApplicationID = appID;
            this.ApplicationDate = appDate;
            this.LastStatusDate = lastStatusDate;
            this.ApplicationStatus = appStatus;
            this.PaidFees = fees;

            this.ApplicationTypeID = appTypeID;
            this.ApplicantPersonID = applicantPersonID;
            this.CreatedByUserID = createdByUserID;

            this.ApplicationType = clsApplicationType.Find(appTypeID);
            this.Applicant = clsPerson.Find(applicantPersonID);
            this.User = clsUser.Find(createdByUserID);


            this._Mode = enMode.Update;
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




        private bool _AddNewApplication() {

            this.ApplicationID = clsApplicationData.Insert(MapApplicationToDTO(this));

            if (this.ApplicationID != -1) {

                this._Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }




        }

        private bool _UpdateApplication() {

            return clsApplicationData.Update(MapApplicationToDTO(this));
        }

        public bool Save() {

            switch (this._Mode) {

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
