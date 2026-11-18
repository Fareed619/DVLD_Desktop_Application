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

            return new clsTestType((enTestType)TestTypeDTO.TestTypeID, TestTypeDTO.TestTypeTitle, TestTypeDTO.TestTypeDescription, TestTypeDTO.TestTypeFees);
        }

        static private clsTestTypeDTO MapTestTypeToDTO(clsTestType TestType) {

            return new clsTestTypeDTO((int)TestType.TestTypeID, TestType.TestTypeTitle, TestType.TestTypeDescription, TestType.TestTypeFees);
        }



        public enum enMode { AddNew = 0, Update = 1}
        public enMode _Mode = enMode.AddNew;

        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3};



        public enTestType TestTypeID { get; set; }
        public string TestTypeTitle{ get; set; }
        public string TestTypeDescription{ get; set; }
        public float  TestTypeFees{ get; set; }


        public clsTestType() {

            this.TestTypeID = enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;

            this._Mode = enMode.AddNew;
        }

        private clsTestType(enTestType id, string title, string description, float fees) {

            this.TestTypeID = id;
            this.TestTypeTitle = title;
            this.TestTypeDescription = description;
            this.TestTypeFees = fees;

            this._Mode = enMode.Update;
        }


        static public DataTable ListTestTypes() {

            return clsTestTypesData.ListTestTypes();
        }

        static public clsTestType Find(clsTestType.enTestType testTypeID) {

            clsTestTypeDTO TestTypeDTO = clsTestTypesData.Find((int)testTypeID);

            if (TestTypeDTO == null) return null;

            return MapDTOToTestType(TestTypeDTO);
        }

        private bool _AddNewTestType() {

            int ID = clsTestTypesData.Insert(MapTestTypeToDTO(this));

            if(ID != -1) {

                this.TestTypeID = (enTestType)ID;

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
