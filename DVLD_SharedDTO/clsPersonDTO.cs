using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_SharedDTO {

    public class clsPersonDTO {

        public int PersonID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string ThirdName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string NationalNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public int NationalityCountryID { get; set; }


        public clsPersonDTO(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, byte gender, string address, string email, string phone, string imagePath, int nationalityCountryID){

            this.PersonID = personID;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.NationalNo = nationalNo;
            this.Address = address;
            this.Email = email;
            this.Phone = phone;
            this.ImagePath = imagePath;
            this.NationalityCountryID = nationalityCountryID;
        }

    }
}
