using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {
    public class clsTest {

        static private clsTest _MapDTOToTest(clsTestDTO DTO) {

            return new clsTest(
              testID: DTO.TestID,
              testAppointmentID: DTO.TestAppointmentID,
              testResult: DTO.TestResult,
              notes: DTO.Notes,
              createdByUserID: DTO.CreatedByUserID
            );


        }
        static private clsTestDTO _MapTestToDTO(clsTest Test) {

            return new clsTestDTO(

              testID: Test.TestID,
              testAppointmentID: Test.TestAppointmentID,
              testResult: Test.TestResult,
              notes: Test.Notes,
              createdByUserID: Test.CreatedByUserID
            );


        }



        private enum enMode { AddNew = 1, Update = 2}
        private enMode _Mode = enMode.AddNew;


        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTestAppointment TestAppointment;
        public clsUser CreatedByUser;


        // AddNew
        public clsTest() {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            this._Mode = enMode.AddNew;
        }

        //Update
        private clsTest(int testID, int testAppointmentID, bool testResult, string notes, int createdByUserID) {

            this.TestID = testID;
            this.TestAppointmentID = testAppointmentID;
            this.TestResult = testResult;
            this.Notes = notes;
            this.CreatedByUserID = createdByUserID;

            this._Mode = enMode.Update;
        }

        static public clsTest FindTest(int testID) {
            clsTestDTO DTO = clsTestData.Find(testID);
            if(DTO == null) {
                return null;
            }
            else {
                return _MapDTOToTest(DTO);
            }
        }
        static public bool Delete(int testID) {
            return clsTestData.Delete(testID);
        }

        static public byte TrialsCount(int localDrivingLicenseApplicationID, int testTypeID) {

            return clsTestData.TrialsCount(localDrivingLicenseApplicationID, testTypeID);
        }

        static  public byte PassedTestsCount(int localDrivingLicenseApplicationID) {

            return clsTestData.PassedTestsCount(localDrivingLicenseApplicationID);
        }

        private bool _AddNewTest() {

            this.TestID = clsTestData.AddNewTest(_MapTestToDTO(this));

            return this.TestID != -1;
        }
           
        private bool _UpdateTest() {

            return clsTestData.UpdateTest(_MapTestToDTO(this));
        }

        


        public bool Save() {

            switch (this._Mode) {
                case enMode.AddNew:
                    if (_AddNewTest()) {
                        this._Mode = enMode.Update;
                        clsTestAppointment.SetAppointmentLocked(this.TestAppointmentID);
                        return true;
                    }
                    else {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTest();

                default:
                    return false;
            }
        }
    }
}
