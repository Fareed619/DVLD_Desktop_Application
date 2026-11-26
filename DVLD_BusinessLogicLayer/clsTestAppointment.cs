using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {
    public class clsTestAppointment {

        static private clsTestAppointment _MapDTOToTestAppointment(clsTestAppointmentDTO DTO) {

            return new clsTestAppointment(

              testAppointmentID: DTO.TestAppointmentID,
              appointmentDate: DTO.AppointmentDate,
              testTypeID: (enTestType)DTO.TestTypeID,
              localDrivingLicenseApplicationID: DTO.LocalDrivingLicenseApplicationID,
              fees: DTO.PaidFees,
              createdByUser: DTO.CreatedByUserID,
              isLocked: DTO.IsLocked,
              retakeTestApplicationID: DTO.RetakeTestApplicationID
            );


        }

        static private clsTestAppointmentDTO _MapTestAppointmentToDTO(clsTestAppointment TestAppointment) {

            return new clsTestAppointmentDTO(

              testAppointmentID: TestAppointment.TestAppointmentID,
              appointmentDate: TestAppointment.AppointmentDate,
              testTypeID: (int)TestAppointment.TestTypeID,
              localDrivingLicenseApplicationID: TestAppointment.LocalDrivingLicenseApplicationID,
              fees: TestAppointment.PaidFees,
              createdByUser: TestAppointment.CreatedByUserID,
              isLocked: TestAppointment.IsLocked,
              retakeTestApplicationID: TestAppointment.RetakeTestApplicationID
            );


        }

        enum enMode { AddNew = 1, Update = 2}
        enMode _Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };

        // Properties
        public int TestAppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public enTestType TestTypeID { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        // Composition
        public clsTestType TestType;
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication;
        public clsUser CreatedByUser;
        public clsApplication RetakeTestApplication;


        // AddNew Constructor
        public clsTestAppointment() {
            this.TestAppointmentID = -1;
            this.AppointmentDate = DateTime.Now;
            this.TestTypeID = enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;

            this._Mode = enMode.AddNew;
        }

        // Update Constructor
        private clsTestAppointment(int testAppointmentID, DateTime appointmentDate, enTestType testTypeID, int localDrivingLicenseApplicationID, float fees, int createdByUser, bool isLocked, int retakeTestApplicationID = -1) {
            this.TestAppointmentID = testAppointmentID;
            this.AppointmentDate = appointmentDate;
            this.TestTypeID = testTypeID;
            this.LocalDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.PaidFees = fees;
            this.CreatedByUserID = createdByUser;
            this.IsLocked = isLocked;
            this.RetakeTestApplicationID = retakeTestApplicationID;

            this.TestType = clsTestType.Find((clsTestType.enTestType)this.TestTypeID);
            this.LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingLicenseID(this.LocalDrivingLicenseApplicationID);
            this.CreatedByUser = clsUser.Find(this.CreatedByUserID);
            this.RetakeTestApplication = clsApplication.Find(this.RetakeTestApplicationID);

            this._Mode = enMode.Update;

        }


        static public DataTable ListTestAppointments(int localDrivingLicenseApplicationID) {

            return clsTestAppointmentsData.ListTestAppointments(localDrivingLicenseApplicationID);
        }

        static public clsTestAppointment Find(int testAppointmentID) {

            clsTestAppointmentDTO DTO = clsTestAppointmentsData.Find(testAppointmentID);

            if(DTO != null) {

                return _MapDTOToTestAppointment(DTO);
            }
            else {
                return null;
            }
        }


        private bool _AddNewTestAppointment() {

            this.TestAppointmentID = clsTestAppointmentsData.AddNew(_MapTestAppointmentToDTO(this));

            if (this.TestAppointmentID != -1) {
                this._Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }
        }


        // Update
        private bool _UpdateTestAppointment() {

            return clsTestAppointmentsData.Update(this.TestAppointmentID, this.AppointmentDate);
        }


        public bool Save() {

            switch (this._Mode) {
                case enMode.AddNew:
                    return _AddNewTestAppointment();

                case enMode.Update:
                    return _UpdateTestAppointment();

                default:
                    return false;
            }
        }

    }
}
