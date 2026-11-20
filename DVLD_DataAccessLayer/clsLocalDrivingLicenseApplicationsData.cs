using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {

    static public class clsLocalDrivingLicenseApplicationsData {

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

        static public bool Find(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID) {

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

        static public int Insert(int ApplicationID,int LicenseClassID) {

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

        //static public bool Update() {

        //}





    }
}
