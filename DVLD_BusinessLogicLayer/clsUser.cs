using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.PersonID = PersonID;
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

        static public DataTable ListUsers() {

            return clsUserData.ListUsers();

        }

        private bool _AddNewUser() {

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




    }
}
