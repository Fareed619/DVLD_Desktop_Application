using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DVLD_SharedDTO {

    public class clsTestTypeDTO {


        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public float TestTypeFees { get; set; }


        public clsTestTypeDTO(int id, string title, string description, float fees) {
            this.TestTypeID = id;
            this.TestTypeTitle = title;
            this.TestTypeDescription = description;
            this.TestTypeFees = fees;

        }

    }

}
