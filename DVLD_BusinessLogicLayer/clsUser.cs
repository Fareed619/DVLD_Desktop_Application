using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace DVLD_BusinessLogicLayer {

    public class clsUser {

        static private clsUserDTO MapUserToUserDTO(clsUser User) {

            return new clsUserDTO(
                    userID: User.UserID,
                    personID: User.PersonID,
                    username: User.UserName,
                    password: User.Password,
                    isActive: User.IsActive
            );
        }

        static private clsUser MapUserDTOToUser(clsUserDTO UserDTO) {

            return new clsUser(
                userID: UserDTO.UserID,
                personID: UserDTO.PersonID,
                username: UserDTO.Username,
                password: UserDTO.Password,
                isActive: UserDTO.IsActive
            );
        }


        enum enMode { AddNew = 1, Update = 2};

        enMode _Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }


        public clsUser() {

            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = false;

            this._Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string username, string password, bool isActive) {

            this.UserID = userID;
            this.PersonID = personID;
            this.UserName = username;
            this.Password = password;
            this.IsActive = isActive;

            this._Mode = enMode.Update;
        }


        static public clsUser Find(int ID) {

            clsUserDTO UserDTO = clsUserData.Find(ID);

            if(UserDTO == null) {
                return null;
            }

            return MapUserDTOToUser(UserDTO);
        }

        static public clsUser Find(string username) {

            clsUserDTO UserDTO = clsUserData.Find(username);

            if (UserDTO == null) {
                return null;
            }

            return MapUserDTOToUser(UserDTO);
        }


        static public bool DoesPersonExist(int PersonID) {

            return clsUserData.DoesPersonExistInUsersTable(PersonID);
        }

        static public DataTable ListUsers() {

            return clsUserData.ListUsers();

        }

        static public bool DeleteUser(int ID) {

            return clsUserData.DeleteUser(ID);
        }

        private bool _AddNewUser() {

            this.Password = clsPasswordHelper.HashPassword(this.Password);
           

            int ID = clsUserData.AddUser(MapUserToUserDTO(this));

            if(ID != -1) {
                this.UserID = ID;
                this._Mode = enMode.Update;
                return true;
            }
            else {
                return false;
            }
        }

        private bool _UpdateUser() {

           return clsUserData.UpdateUser(MapUserToUserDTO(this));
        }

        public bool Save() {

            switch (this._Mode) {

                case enMode.AddNew:
                    return _AddNewUser();

                case enMode.Update:
                    return _UpdateUser();
                  
            }

            return false;
        }

        public bool ChangePassword(string OldPassword, string NewPassword) {

            //var sw = Stopwatch.StartNew();


            // 1 - hash the old password and verify if it matches the stored hash.
            bool DoesMatch = clsPasswordHelper.VerifyPassword(OldPassword, this.Password);
            if (!DoesMatch) return false;
           

            //sw.Stop();
            //Debug.WriteLine($"Verification: {sw.ElapsedMilliseconds} ms");
            //sw.Restart();


            // 2 - hash the new password and store it in database
            string NewHash = clsPasswordHelper.HashPassword(NewPassword);

            //sw.Stop();
            //Debug.WriteLine($"New password hashing: {sw.ElapsedMilliseconds} ms");

            // 3 -  Save it
            if( clsUserData.ChangePassword(NewHash, this.UserID)) {
                this.Password = NewHash;
                return true;
            }
            else {
                return false;
            }
        }

        


    }
}
