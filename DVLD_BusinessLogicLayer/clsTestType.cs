using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {

    public class clsTestType {

        static private clsTestType MapDTOToTestType(clsTestTypeDTO TestTypeDTO) {

            return new clsTestType(TestTypeDTO.TestTypeID, TestTypeDTO.TestTypeTitle, TestTypeDTO.TestTypeDescription, TestTypeDTO.TestTypeFees);
        }

        static private clsTestTypeDTO MapTestTypeToDTO(clsTestType TestType) {

            return new clsTestTypeDTO(TestType.TestTypeID, TestType.TestTypeTitle, TestType.TestTypeDescription, TestType.TestTypeFees);
        }



        public enum enMode { AddNew = 0, Update = 1}
        public enMode _Mode = enMode.AddNew;


        public int    TestTypeID { get; set; }
        public string TestTypeTitle{ get; set; }
        public string TestTypeDescription{ get; set; }
        public float  TestTypeFees{ get; set; }


        public clsTestType() {

            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;

            this._Mode = enMode.AddNew;
        }

        private clsTestType(int id, string title, string description, float fees) {

            this.TestTypeID = id;
            this.TestTypeTitle = title;
            this.TestTypeDescription = description;
            this.TestTypeFees = fees;

            this._Mode = enMode.Update;
        }


        static public DataTable ListTestTypes() {

            return clsTestTypesData.ListTestTypes();
        }

        static public clsTestType Find(int testTypeID) {

            clsTestTypeDTO TestTypeDTO = clsTestTypesData.Find(testTypeID);

            if (TestTypeDTO == null) return null;

            return MapDTOToTestType(TestTypeDTO);
        }

        private bool _AddNewTestType() {

            this.TestTypeID = clsTestTypesData.Insert(MapTestTypeToDTO(this));

            if(this.TestTypeID != -1) {

                this._Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }




        }
        
        private bool _UpdateTestType() {

            return clsTestTypesData.Update(MapTestTypeToDTO(this));
        }

        public bool Save() {

            switch (this._Mode) {

                case enMode.AddNew:
                    return _AddNewTestType();

                case enMode.Update:
                    return _UpdateTestType();

                default:
                    return false;
            }
        }


    }
}
