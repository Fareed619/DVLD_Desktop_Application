using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {
    public class clsDriver {

        enum enMode { AddNew = 1, Update = 2 }
        enMode _Mode = enMode.AddNew;

        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        // Composition
        clsPerson Person;
        clsUser CreatedByUser;


        public clsDriver() {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;

            this._Mode = enMode.AddNew;
        }

        private clsDriver(int driverID, int personID, int createdByUserID, DateTime createdDate) {
            this.DriverID = driverID;
            this.PersonID = personID;
            this.CreatedByUserID = createdByUserID;
            this.CreatedDate = createdDate;

            this.Person = clsPerson.Find(this.PersonID);
            this.CreatedByUser = clsUser.Find(this.CreatedByUserID);

            this._Mode = enMode.Update;
        }


        static public bool DoesDriverExist(int driverID) {
            return clsDriverData.DoesDriverExist(driverID);
        }

        static public clsDriver Find(int DirverID) {
            int personID = -1, createdByUserID = -1;
            DateTime createdDate = DateTime.Now;

            bool isFound = clsDriverData.Find(DirverID, ref personID, ref createdByUserID, ref createdDate);

            if (!isFound) {
                return null;
            }
            else {
                return new clsDriver(DirverID, personID, createdByUserID, createdDate);
            }
            
        }

        static public DataTable ListDrivers() {

            return clsDriverData.ListDrivers();

        }

        static public bool DeleteDriver(int ID) {

            return clsDriverData.Delete(ID);
        }

        private bool _AddNewDriver() {


            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            if (this.DriverID != -1) {
                this._Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }
        }

        private bool _UpdateDriver() {

            return clsDriverData.Update(this.DriverID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public bool Save() {

            switch (this._Mode) {

                case enMode.AddNew:
                    return _AddNewDriver();

                case enMode.Update:
                    return _UpdateDriver();

            }

            return false;
        }

    }
}
