using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {

    static public class clsApplicationTypesData {

        static public DataTable ListApplicationTypes() {

            DataTable ApplicationTypesList = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM ApplicationTypes; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {
                    ApplicationTypesList.Load(reader);
                }
                else {
                    ApplicationTypesList = null;
                }

                reader.Close();
            }
            catch (Exception Ex) {

                ApplicationTypesList = null;
            }
            finally {
                Connection.Close();
            }

            return ApplicationTypesList;
        }

        static public bool UpdateApplicationType(int ApplicationTypeID, float NewFees, string Title) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE ApplicationTypes 
                             SET ApplicationFees = @NewFees, ApplicationTypeTitle = @Title
                             WHERE ApplicationTypeID = @ApplicationTypeID;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NewFees", NewFees);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@Title", Title);

            try {

                Connection.Open();
                rowAffacted = Command.ExecuteNonQuery();
            }
            catch (Exception Ex) {

            }
            finally {
                Connection.Close();
            }

            return (rowAffacted > 0);
        }

        static public int InsertApplicationType(float NewFees, string Title) {

            int ID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO ApplicationTypes(ApplicationTypeTitle, ApplicationFees)
                             VALUES(@Title, @NewFees);
                             SELECT SCOPE_IDENTITY(); ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NewFees", NewFees);
            Command.Parameters.AddWithValue("@Title", Title);

            try {

                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedApplicationType)) {

                    ID = insertedApplicationType;
                }
                else {
                    ID = -1;
                }
            }
            catch (Exception Ex) {

            }
            finally {
                Connection.Close();
            }

            return ID;
        }

        static public bool Find(int ApplicationTypeID, ref string Title, ref float Fees) {

            bool isFound = false;


            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM ApplicationTypes 
                            WHERE ApplicationTypeID = @ApplicationTypeID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try {

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    Title = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
                    
                    isFound = true;
                }
                else {
                    isFound = false;
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

        static public bool Find(string ApplicationTypeTitle, ref int ID, ref float Fees) {

            bool isFound = false;


            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM ApplicationTypes 
                            WHERE ApplicationTypeTitle = @ApplicationTypeTitle; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);

            try {

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    ID = (int)reader["ApplicationTypeID"];
                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
                    
                    isFound = true;
                }
                else {
                    isFound = false;
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
