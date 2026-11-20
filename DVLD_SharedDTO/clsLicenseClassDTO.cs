using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_SharedDTO {

    public class clsLicenseClassDTO {

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public byte MinAllowedAge { get; set; }
        public byte DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }


        public clsLicenseClassDTO(int licenseClassID, string className, string classDescription, byte minAgeAllowed, byte defaultValidityLength, float classFees) {

            this.LicenseClassID = licenseClassID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinAllowedAge = minAgeAllowed;
            this.DefaultValidityLength = defaultValidityLength;
            this.ClassFees = ClassFees;

        }
    }
}
