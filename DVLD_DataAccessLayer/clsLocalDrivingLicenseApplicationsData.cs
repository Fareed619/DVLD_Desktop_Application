using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DVLD_DataAccessLayer {

    static public class clsLocalDrivingLicenseApplicationsData {

        static private bool _CheckIfColumnNameValid(string Column) {

            string[] AllowedColumns = { "LocalDrivingLicenseApplicationID", "DrivingClass", "NationalNo", "FullName", "ApplicationDate", "Status", "PassedTests" };

            return AllowedColumns.Contains(Column);
        }



        static public DataTable ListLocalDrivingLicenseAppsView() {

            DataTable LocalLicense = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplicationViewFD;";

            SqlCommand Command = new SqlCommand(query, Connection);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {

                    LocalLicense.Load(reader);
                }
                else {
                    LocalLicense = null;
                }



                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                LocalLicense = null;
            }
            finally {
                Connection.Close();
            }

            return LocalLicense;
        }

        static public bool GetLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID) {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID ;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                    isFound = true;
                }
                else {
                    isFound = false;
                }


                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                isFound = false;

            }
            finally {
                Connection.Close();
            }

            return isFound;
        }

        static public bool GetLocalDrivingLicenseApplicationByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID) {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID ;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];

                    isFound = true;
                }
                else {
                    isFound = false;
                }


                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                isFound = false;

            }
            finally {
                Connection.Close();
            }

            return isFound;
        }

        static public int AddNewLocalDrivingLicenseApplication(int ApplicationID,int LicenseClassID) {

            int localDrivingLicenseApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO LocalDrivingLicenseApplications(ApplicationID, LicenseClassID)
                             VALUES(@ApplicationID, @LicenseClassID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
           

            try {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedLocalLicense)) {

                    localDrivingLicenseApplicationID = insertedLocalLicense;
                }
                else {
                    localDrivingLicenseApplicationID = -1;
                }


            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                localDrivingLicenseApplicationID = -1;

            }
            finally {
                Connection.Close();
            }

            return localDrivingLicenseApplicationID;

        }

        static public bool UpdateLocalDrivingLicenseApplication( int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE LocalDrivingLicenseApplications
                             SET ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID
                             WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try {
                Connection.Open();

                rowAffacted = Command.ExecuteNonQuery();


            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                rowAffacted = 0;

            }
            finally {
                Connection.Close();
            }


            return (rowAffacted > 0);
        }

        static public bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID) {

            
            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"DELETE FROM LocalDrivingLicenseApplications
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);


            try {
                Connection.Open();

                rowAffacted = Command.ExecuteNonQuery();


            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                rowAffacted = 0;

            }
            finally {
                Connection.Close();
            }


            return (rowAffacted > 0);
            

        }

        static public DataTable FilterBy(string ColumnName, string Value) {

            DataTable LocalLicense = new DataTable();

            if (!_CheckIfColumnNameValid(ColumnName)) {
                throw new ArgumentException("Invalid column name");
            }

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = $@"SELECT * FROM LocalDrivingLicenseApplicationViewFD
                             WHERE {ColumnName} LIKE @Value ;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

          

            Command.Parameters.AddWithValue("@ColumnName", ColumnName);
            Command.Parameters.AddWithValue("@Value", "%" + Value + "%");

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {
                    LocalLicense.Load(reader);
                }
                else {
                    LocalLicense = null;
                }



                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                LocalLicense = null;
            }
            finally {
                Connection.Close();
            }

            return LocalLicense;
        }

        static public DataTable FilterBy(string ColumnName, int Value) {

            DataTable LocalLicense = new DataTable();

            if (!_CheckIfColumnNameValid(ColumnName)) {
                throw new ArgumentException("Invalid column name");
            }

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = $@"SELECT * FROM LocalDrivingLicenseApplicationViewFD
                             WHERE {ColumnName} = @Value; ";

            SqlCommand Command = new SqlCommand(query, Connection);

           

            Command.Parameters.AddWithValue("@ColumnName", ColumnName);
            Command.Parameters.AddWithValue("@Value", Value);



            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {
                    LocalLicense.Load(reader);
                }
                else {
                    LocalLicense = null;
                }



                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                LocalLicense = null;
            }
            finally {
                Connection.Close();
            }

            return LocalLicense;
        }


    }
}
