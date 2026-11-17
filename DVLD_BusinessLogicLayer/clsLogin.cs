using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {

    public class clsLogin {

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsLoggedIn = false;
        public clsUser User;


        public clsLogin(string username, string password) {

            this.UserName = username;
            this.Password = password;
            this.IsLoggedIn = false;
        }

        public bool LogIn(ref string ErrorMessage) {

            this.User = clsUser.Find(this.UserName);

            // 1 - Check if user exists
            if(this.User == null) {
                ErrorMessage = "User is not found";
                this.IsLoggedIn = false;
                return false;
            }

            // 2 - Check the username
            if(this.UserName != this.User.UserName) {
                ErrorMessage = "Invalid Credentials";
                this.IsLoggedIn = false;
                return false;
            }

            // 3 - Check the password
            bool DoesMatch = clsPasswordHelper.VerifyPassword(this.Password, this.User.Password);

            if(!DoesMatch) {
                ErrorMessage = "Invalid Credentials";
                this.IsLoggedIn = false;
                return false;
            }

            // 4 - Check the user's activeniss
            if (!this.User.IsActive) {
                ErrorMessage = "Your Account is not Active, Please Contact Your Admin!";
                this.IsLoggedIn = false;
                return false;
            }


            this.IsLoggedIn = true;
            ErrorMessage = string.Empty;
            return true;
        }

        public bool SignOut() {
            this.IsLoggedIn = false;
            return true;
        }


    }
}
