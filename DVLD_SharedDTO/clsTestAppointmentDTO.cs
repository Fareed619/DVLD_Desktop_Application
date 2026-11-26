using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_SharedDTO {
    public class clsTestAppointmentDTO {

        // Properties
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }


        public clsTestAppointmentDTO(int testAppointmentID, DateTime appointmentDate, int testTypeID, int localDrivingLicenseApplicationID, float fees, int createdByUser, bool isLocked, int retakeTestApplicationID = -1) {
            this.TestAppointmentID = testAppointmentID;
            this.AppointmentDate = appointmentDate;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.PaidFees = fees;
            this.CreatedByUserID = createdByUser;
            this.IsLocked = isLocked;
            this.RetakeTestApplicationID = retakeTestApplicationID;

        }
           



    }
}
