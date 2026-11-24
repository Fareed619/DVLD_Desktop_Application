using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {

    public class clsLicenseClass {

        static private clsLicenseClass MapDTOToLicenseClass(clsLicenseClassDTO LicenseClassDTO) {

            return new clsLicenseClass(

                licenseClassID: LicenseClassDTO.LicenseClassID,
                className: LicenseClassDTO.ClassName,
                classDescription: LicenseClassDTO.ClassDescription,
                minAgeAllowed: LicenseClassDTO.MinAllowedAge,
                defaultValidityLength: LicenseClassDTO.DefaultValidityLength,
                classFees: LicenseClassDTO.ClassFees

            );
            
        }



        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte  MinAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }


        public clsLicenseClass (int licenseClassID, string className, string classDescription, byte minAgeAllowed, byte defaultValidityLength, float classFees) {

            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinAllowedAge = minAgeAllowed;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = ClassFees;

        }

        static public clsLicenseClass Find(int licenseClassID) {

            clsLicenseClassDTO  LicenseClassDTO = clsLicenseClassData.Find(licenseClassID);

            if(LicenseClassDTO != null) {
                return MapDTOToLicenseClass(LicenseClassDTO);
            }
            else {
                return null;
            }
            
        }

        static public clsLicenseClass Find(string className) {

            clsLicenseClassDTO LicenseClassDTO = clsLicenseClassData.Find(className);

            if (LicenseClassDTO != null) {
                return MapDTOToLicenseClass(LicenseClassDTO);
            }
            else {
                return null;
            }
        }

        static public DataTable ListLicenseClasses() {

            return clsLicenseClassData.ListLicenseClasses();
        }

    }
}
