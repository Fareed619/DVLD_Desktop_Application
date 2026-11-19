using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_SharedDTO {

    public class clsApplicationDTO {


        public int ApplicationID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime LastStatusDate { get; set; }
        public byte ApplicationStatus { get; set; }
        public float PaidFees { get; set; }

        public int ApplicationTypeID { get; set; }
        public int ApplicantPersonID { get; set; }
        public int CreatedByUserID { get; set; }


        public clsApplicationDTO(int appID, DateTime appDate, DateTime lastStatusDate, byte appStatus, float fees, int appTypeID, int applicantPersonID, int createdByUserID) {


            this.ApplicationID = appID;
            this.ApplicationDate = appDate;
            this.LastStatusDate = lastStatusDate;
            this.ApplicationStatus = appStatus;
            this.PaidFees = fees;

            this.ApplicationTypeID = appTypeID;
            this.ApplicantPersonID = applicantPersonID;
            this.CreatedByUserID = createdByUserID;

        }


    }
}
