using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {

    public class clsApplicationType{


        public enum enMode { AddNew = 0, Update = 2}

        public enMode Mode = enMode.AddNew;


        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }
        public float ApplicationTypeFees { get; set; }


        public clsApplicationType() {
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationTypeFees = 0;

            this.Mode = enMode.AddNew;
        }

        private clsApplicationType(int applicationTypeID, string applicationTypeTitle, float applicationTypeFees) {

            this.ApplicationTypeID = applicationTypeID;
            this.ApplicationTypeTitle = applicationTypeTitle;
            this.ApplicationTypeFees = applicationTypeFees;

            this.Mode = enMode.Update;
        }



        static public DataTable ListApplicationTypes() {

            return clsApplicationTypesData.ListApplicationTypes();
        }

        static public clsApplicationType Find(int ApplicationTypeID) {

            string Title = "";
            float Fees = 0;

            if(clsApplicationTypesData.Find(ApplicationTypeID, ref Title, ref Fees)) {

                return new clsApplicationType(ApplicationTypeID, Title, Fees);
            }
            else {
                return null;
            }
        }

        static public clsApplicationType Find(string ApplicationTypeTitle) { 

            int ID = -1;
            float Fees = 0;

            if(clsApplicationTypesData.Find(ApplicationTypeTitle, ref ID, ref Fees)) {

                return new clsApplicationType(ID, ApplicationTypeTitle, Fees);
            }
            else {
                return null;
            }
        }


        private bool _AddNewApplicationType() {

            this.ApplicationTypeID = clsApplicationTypesData.InsertApplicationType(this.ApplicationTypeFees, this.ApplicationTypeTitle);

            if (this.ApplicationTypeID != -1) {

                this.Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }
        }

        private bool _UpdateApplicationType() {

            return clsApplicationTypesData.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeFees, this.ApplicationTypeTitle);
        }

        public bool Save() {

            switch (this.Mode) {

                case enMode.AddNew:

                    return _AddNewApplicationType();

                case enMode.Update:

                    return _UpdateApplicationType();

                default:
                    return false;
            }
                
        }

    }
       
            



}
