using DVLD_DataAccessLayer;
using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer {

  


    public class clsPerson {

        static private clsPerson _MapPersonDTOToPerson(clsPersonDTO PersonDTO) {

            return new clsPerson(
                personID: PersonDTO.PersonID,
                nationalNo: PersonDTO.NationalNo,
                firstName: PersonDTO.FirstName,
                secondName: PersonDTO.SecondName,
                thirdName: PersonDTO.ThirdName,
                lastName: PersonDTO.LastName,
                dateOfBirth: PersonDTO.DateOfBirth,
                gender: PersonDTO.Gender,
                address: PersonDTO.Address,
                email: PersonDTO.Email,
                phone: PersonDTO.Phone,
                imagePath: PersonDTO.ImagePath,
                nationalityCountryID: PersonDTO.NationalityCountryID
            );
        }

        static private clsPersonDTO _MapPersonToPersonDTO(clsPerson Person) {

            return new clsPersonDTO(
                personID: Person.PersonID,
                nationalNo: Person.NationalNo,
                firstName: Person.FirstName,
                secondName: Person.SecondName,
                thirdName: Person.ThirdName,
                lastName: Person.LastName,
                dateOfBirth: Person.DateOfBirth,
                gender: Person.Gender,
                address: Person.Address,
                email: Person.Email,
                phone: Person.Phone,
                imagePath: Person.ImagePath,
                nationalityCountryID: Person.NationalityCountryID
            );
        }


        public enum enMode  { 
            AddNew = 1,
            Update = 2
        }

        private enMode _Mode = enMode.AddNew;

        public int PersonID { get; set; } = -1;
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

        public clsCountry CountryInfo;


        public clsPerson(){
            this.PersonID = -1;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.DateOfBirth = DateTime.Now;
            this.Gender = 0;
            this.NationalNo = string.Empty;
            this.Address = string.Empty;
            this.Email = string.Empty;
            this.Phone = string.Empty;
            this.ImagePath = string.Empty;
            this.NationalityCountryID = -1;    

            this._Mode = enMode.AddNew;
        }

        private clsPerson(int personID, string nationalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, byte gender, string address, string email, string phone, string imagePath, int nationalityCountryID) {

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
            this.CountryInfo = clsCountry.Find(nationalityCountryID);

            this._Mode = enMode.Update;
        }


        // Read
        static public clsPerson Find(int ID) {

            clsPersonDTO PersonDTO = clsPersonData.Find(ID);

            if(PersonDTO == null) return null;

            else {
                return _MapPersonDTOToPerson(PersonDTO);
            }
        }

        static public clsPerson Find(string NationalNo) {

            clsPersonDTO PersonDTO = clsPersonData.Find(NationalNo);

            if(PersonDTO == null) return null;

            else {
                return _MapPersonDTOToPerson(PersonDTO);
            }
        }

        static public bool Exists(string NationalNo) {

            return clsPersonData.Exists(NationalNo);
        }

        static public DataTable ListPeopleView() {

            return clsPersonData.ListPeopleView();
        }

        static public DataTable ListAllPeople() {

            return clsPersonData.ListAllPeople();
        }

        static public void DisplayPeople(DataTable People) {

            // 1. Print Column Headers
            foreach (DataColumn column in People.Columns)
            {
                Console.Write($"{column.ColumnName}\t"); // Separated by tabs
            }
            Console.WriteLine();
            Console.WriteLine(new string('-', 50)); // Divider line

            // 2. Print Rows
            foreach (DataRow row in People.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write($"{item}\t");
                }
                Console.WriteLine();
            }
        }

        static public DataTable FilterBy(string ColumnName, string Value) {

            string Condition = $"{ColumnName} LIKE '%{Value}%'";

            return clsPersonData.FilterBy(Condition);
        }

        // Delete
        static public bool Delete(int PersonID) {

            return clsPersonData.DeletePerson(PersonID);
        }

        // Add
        private bool _AddNewPerson() {

            int ID = clsPersonData.Insert(_MapPersonToPersonDTO(this));

            if(ID != -1) {
                this.PersonID = ID;
                this._Mode = enMode.Update;
                return true;
            }
            else {
                PersonID = -1;
                return false;
            }
        }


        // Update
        private bool UpdatePerson() {

            return clsPersonData.UpdatePerson(_MapPersonToPersonDTO(this));
        }


        public bool Save() {

            switch (this._Mode) {
                case enMode.AddNew:
                    return _AddNewPerson();
               
                case enMode.Update:
                    return UpdatePerson();

                default:
                    return false;
            }
        }



    }
}
