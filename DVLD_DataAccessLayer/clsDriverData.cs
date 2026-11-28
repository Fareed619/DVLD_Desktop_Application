using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {
    static public class clsDriverData {

        //CRUD Operations

        static public DataTable ListDrivers() {

            DataTable Drivers = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Drivers;";

            SqlCommand Command = new SqlCommand(query, Connection);


            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {

                    Drivers.Load(reader);
                }

                reader.Close();
            }
            catch (Exception Ex) {
                Drivers = null;

            }
            finally {
                Connection.Close();
            }

            return Drivers;
        }

        static public bool Find(int DriverID, ref int PersonID, ref int CreatedByUserID,ref DateTime CreatedDate) {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);



            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];

                    isFound = true;

                }
                else {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception Ex) {
                isFound = false;

            }
            finally {
                Connection.Close();
            }

            return isFound;

        }

        static public int AddUser(int PersonID,  int CreatedByUserID,  DateTime CreatedDate) {

            int UserID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Drivers(PersonID, CreatedByUserID, CreatedDate)
                             VALUES(@PersonID, @CreatedByUserID, @CreatedDate);
                             SELECT SCOPE_IDENTITY(); ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);


            try {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedDriver)) {

                    UserID = insertedDriver;
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

        static public bool UpdateUser(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE Drivers
                             SET PersonID = @PersonID, CreatedByUserID = @CreatedByUserID, CreatedDate = @CreatedDate
                             WHERE DriverID = @DriverID;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            Command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            Command.Parameters.AddWithValue("@DriverID", DriverID);


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

        static public bool DeleteUser(int DriverID) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"DELETE FROM Drivers
                             WHERE DriverID = @DriverID;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);


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

        static public bool DoesDriverExist(int DriverID) {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT 1 FROM Drivers WHERE DriverID = @DriverID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@DriverID", DriverID);

            try {
                Connection.Open();

                object res = Command.ExecuteScalar();

                if (res == null) {
                    isFound = false;
                }
                else {
                    isFound = true;
                }


            }
            catch (Exception Ex) {
                isFound = false;

            }
            finally {
                Connection.Close();
            }

            return isFound;
        }

     
        
    }
}
