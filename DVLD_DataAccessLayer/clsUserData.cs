using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {

    static public class clsUserData {

        static public DataTable ListUsers() {

            DataTable Users = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Users;";

            SqlCommand Command = new SqlCommand(query, Connection);


            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();
                
                if(reader.HasRows) {

                    Users.Load(reader);
                }

                reader.Close();
            }
            catch(Exception Ex) {
                Users = null;

            }
            finally {
                Connection.Close();
            }

            return Users;
        }

        static public clsUserDTO Find(int UserID) {

            clsUserDTO User = null;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);



            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    User = new clsUserDTO(
                        userID: (int)reader["UserID"],
                        personID: (int)reader["PersonID"],
                        username: (string)reader["UserName"],
                        password: (string)reader["Password"],
                        isActive: (bool)reader["IsActive"]                       
                    );
                }

                reader.Close();
            }
            catch (Exception Ex) {
                User = null;

            }
            finally {
                Connection.Close();
            }

            return User;

        }

        static public int AddUser(clsUserDTO User) {

            int UserID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Users(PersonID, UserName, Password, IsActive)
                             VALUES(@PersonID, @Username, @Password, @IsActive);
                             SELECT SCOPE_IDENTITY(); ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", User.PersonID);
            Command.Parameters.AddWithValue("@UserName", User.Username);
            Command.Parameters.AddWithValue("@Password", User.Password);
            Command.Parameters.AddWithValue("@IsActive", User.IsActive);


            try {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedPerson)) {

                    UserID = insertedPerson;
                }
                else {
                    UserID = -1;
                }

            }
            catch (Exception Ex) {
                UserID = -1;

            }
            finally {
                Connection.Close();
            }

            return UserID;

        }

        static public bool UpdateUser(clsUserDTO User) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE Users
                             SET PersonID = @PersonID, UserName = @Username, Password = @Password, IsActive = @IsActive
                             WHERE UserID = @UserID;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", User.PersonID);
            Command.Parameters.AddWithValue("@UserName", User.Username);
            Command.Parameters.AddWithValue("@Password", User.Password);
            Command.Parameters.AddWithValue("@IsActive", User.IsActive);
            Command.Parameters.AddWithValue("@UserID", User.UserID);


            try {
                Connection.Open();

                rowAffacted = Command.ExecuteNonQuery();

            }
            catch (Exception Ex) {
                rowAffacted = 0;

            }
            finally {
                Connection.Close();
            }

            return (rowAffacted > 0);

        }

        static public bool DeleteUser(int UserID) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"DELETE FROM Users
                             WHERE UserID = @UserID;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);


            try {
                Connection.Open();

                rowAffacted = Command.ExecuteNonQuery();

            }
            catch (Exception Ex) {
                rowAffacted = 0;

            }
            finally {
                Connection.Close();
            }

            return (rowAffacted > 0);

        }
    }

}
