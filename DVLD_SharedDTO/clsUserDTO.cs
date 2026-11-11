using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_SharedDTO {

    public class clsUserDTO {

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        public clsUserDTO(int userID, int personID, string username, string password, bool isActive) {

            this.UserID = userID;
            this.PersonID = personID;
            this.Username = username;
            this.Password = password;
            this.IsActive = isActive;
           
        }

    }
}
