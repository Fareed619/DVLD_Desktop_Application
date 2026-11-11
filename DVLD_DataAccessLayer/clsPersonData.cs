using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {

    static public class clsPersonData {

        private static string _GetNullableString(SqlDataReader reader, string column) {

            return reader[column] == DBNull.Value
                ? string.Empty
                : reader[column].ToString();
        }

        private static void _AddNullableParameter(SqlCommand command, string parameterName, object value) {

            if (value == null || (value is string strValue && strValue == ""))
            {
                command.Parameters.AddWithValue(parameterName, DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(parameterName, value);
            }
        }

        static private clsPersonDTO _MapDataReaderToPersonDTO(SqlDataReader reader) {

            return new clsPersonDTO(
                personID: (int)reader["PersonID"],
                nationalNo: (string)reader["NationalNo"],
                firstName: (string)reader["FirstName"],
                secondName: (string)reader["SecondName"],
                thirdName: _GetNullableString(reader, "ThirdName"),
                lastName: (string)reader["LastName"],
                dateOfBirth: (DateTime)reader["DateOfBirth"],
                gender: (byte)reader["Gendor"],
                address: (string)reader["Address"],
                email: _GetNullableString(reader, "Email"),
                phone: (string)reader["Phone"],
                imagePath: _GetNullableString(reader, "ImagePath"),
                nationalityCountryID: (int)reader["NationalityCountryID"]
            );

        }

        static public clsPersonDTO Find(int PersonID) {

            clsPersonDTO Person = null;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE PersonID = @PersonID;";

            SqlCommand Command = new SqlCommand(query, Connection);
            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {
                    Person = _MapDataReaderToPersonDTO(reader);

                }
                else {
                    Person = null;
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                Person = null;

            }
            finally
            {
                Connection.Close();
            }

            return Person;
        }

        static public clsPersonDTO Find(string NationalNo) {

            clsPersonDTO Person = null;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {
                    Person = _MapDataReaderToPersonDTO(reader);

                }
                else {
                    Person = null;
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                Person = null;

            }
            finally
            {
                Connection.Close();
            }

            return Person;
        }

        static public int Insert(clsPersonDTO Person) {

            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO People(NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath)
                             VALUES(@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", Person.NationalNo);
            Command.Parameters.AddWithValue("@FirstName", Person.FirstName);
            Command.Parameters.AddWithValue("@SecondName", Person.SecondName);

            _AddNullableParameter(Command, "@ThirdName", Person.ThirdName);

            Command.Parameters.AddWithValue("@LastName", Person.LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
            Command.Parameters.AddWithValue("@Gendor", Person.Gender);
            Command.Parameters.AddWithValue("@Address", Person.Address);
            Command.Parameters.AddWithValue("@Phone", Person.Phone);

            _AddNullableParameter(Command, "@Email", Person.Email);

            Command.Parameters.AddWithValue("@NationalityCountryID", Person.NationalityCountryID);

            _AddNullableParameter(Command, "@ImagePath", Person.ImagePath);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedPerson)) {

                    PersonID = insertedPerson;
                }
                else
                {
                    PersonID = -1;
                }

                
            }
            catch (Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                PersonID = -1;

            }
            finally
            {
                Connection.Close();
            }


            return PersonID;
        }

        static public DataTable ListPeopleView() {

            DataTable PeopleList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM PeopleView";

            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {

                    PeopleList.Load(reader);
                }



                reader.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                PeopleList = null;
            }
            finally
            {
                Connection.Close();
            }

            return PeopleList;

        }

        static public DataTable ListAllPeople() {

            DataTable PeopleList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT 
	                        PersonID,NationalNo, FirstName,SecondName, ThirdName, LastName, Address,
	                        Nationality = CountryName, Phone,
	                        CASE
		                        WHEN Gendor <> 0 THEN 'Female'
		                        ELSE 'Male'
	                        END AS Gender,
	                        Email, ImagePath
                            FROM People
                            INNER JOIN Countries
	                            ON People.NationalityCountryID = Countries.CountryID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {

                    PeopleList.Load(reader);
                }



                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                PeopleList = null;
            }
            finally {
                Connection.Close();
            }

            return PeopleList;

        }

        static public bool UpdatePerson(clsPersonDTO Person) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE People
                             SET NationalNo = @NationalNo, FirstName = @FirstName, SecondName = @SecondName, ThirdName = @ThirdName,
                             LastName = @LastName, DateOfBirth = @DateOfBirth, Gendor = @Gender, Address = @Address, Phone = @Phone,
                             Email = @Email, NationalityCountryID = @NationalityCountryID, ImagePath = @ImagePath
                             WHERE PersonID = @PersonID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", Person.PersonID);

            Command.Parameters.AddWithValue("@NationalNo", Person.NationalNo);
            Command.Parameters.AddWithValue("@FirstName", Person.FirstName);
            Command.Parameters.AddWithValue("@SecondName", Person.SecondName);

            _AddNullableParameter(Command, "@ThirdName", Person.ThirdName);

            Command.Parameters.AddWithValue("@LastName", Person.LastName);
            Command.Parameters.AddWithValue("@DateOfBirth", Person.DateOfBirth);
            Command.Parameters.AddWithValue("@Gender", Person.Gender);
            Command.Parameters.AddWithValue("@Address", Person.Address);
            Command.Parameters.AddWithValue("@Phone", Person.Phone);

            _AddNullableParameter(Command, "@Email", Person.Email);

            Command.Parameters.AddWithValue("@NationalityCountryID", Person.NationalityCountryID);

            _AddNullableParameter(Command, "@ImagePath", Person.ImagePath);

            try
            {
                Connection.Open();

                rowAffacted = Command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                rowAffacted = 0;

            }
            finally
            {
                Connection.Close();
            }


            return (rowAffacted > 0);
        }

        static public bool DeletePerson(int PersonID) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);


            try
            {
                Connection.Open();

                rowAffacted = Command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                rowAffacted = 0;

            }
            finally
            {
                Connection.Close();
            }


            return (rowAffacted > 0);
        }

        static public DataTable FilterBy(string Condition) {

            DataTable FilteredTable = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = $"SELECT * FROM PeopleView WHERE {Condition};";

            SqlCommand Command = new SqlCommand(query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                FilteredTable.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                FilteredTable = null;
            }
            finally
            {
                Connection.Close();
            }
            return FilteredTable;
        }

         static public bool Exists(int PersonID) {

            bool exists = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT 1 FROM People WHERE PersonID = @PersonID;";
            

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try {
                Connection.Open();

                object obj = Command.ExecuteScalar();

                if(obj != null)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }

            }
            catch(Exception Ex) {
                exists = false;
            }
            finally {
                Connection.Close();
            }

            return exists;
        }

        static public bool Exists(string NationalNo) {

            bool exists = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT 1 FROM People WHERE NationalNo = @NationalNo;";
            

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try {
                Connection.Open();

                object obj = Command.ExecuteScalar();

                if(obj != null)
                {
                    exists = true;
                }
                else
                {
                    exists = false;
                }

            }
            catch(Exception Ex) {
                exists = false;
            }
            finally {
                Connection.Close();
            }

            return exists;
        }

    }

}

