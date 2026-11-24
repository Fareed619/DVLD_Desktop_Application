using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer {

    static public class clsApplicationData {

        static private clsApplicationDTO MapReaderApplicationDTO(SqlDataReader reader) {

            return new clsApplicationDTO(

                appID: (int)reader["ApplicationID"],
                appDate: (DateTime)reader["ApplicationDate"],
                lastStatusDate: (DateTime)reader["LastStatusDate"],
                appStatus: (byte)reader["ApplicationStatus"],
                fees: Convert.ToSingle(reader["PaidFees"]),

                appTypeID: (int)reader["ApplicationTypeID"],
                applicantPersonID: (int)reader["ApplicantPersonID"],
                createdByUserID: (int)reader["CreatedByUserID"]

            );



        }



        static public DataTable ListApplications() {

            DataTable Applications = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Applications; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {
                    Applications.Load(reader);
                }
                else {
                    Applications = null;
                }

            }catch(Exception Ex) {

                Applications = null;

            }
            finally {
                Connection.Close();
            }

            return Applications;
        }

        static public clsApplicationDTO Find(int ApplicationID) {

            clsApplicationDTO Application;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Applications  WHERE ApplicationID = @ApplicationID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    Application = MapReaderApplicationDTO(reader);
                }
                else {
                    Application = null;
                }

            }
            catch (Exception Ex) {

                Application = null;

            }
            finally {
                Connection.Close();
            }

            return Application;
        }

        static public int Insert(clsApplicationDTO AppDTO) {

            int ID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Applications(ApplicantPersonID, ApplicationDate, ApplicationTypeID,                ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                             VALUES(@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", AppDTO.ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", AppDTO.ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", AppDTO.ApplicationTypeID);

            Command.Parameters.AddWithValue("@ApplicationStatus", AppDTO.ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", AppDTO.LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", AppDTO.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", AppDTO.CreatedByUserID);


            try {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedApp)) {

                    ID = insertedApp;
                }
                else {
                    ID = -1;
                }


            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                ID = -1;

            }
            finally {
                Connection.Close();
            }


            return ID;
        }
        
        static public bool Update(clsApplicationDTO AppDTO) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE Applications
                             SET ApplicantPersonID = @ApplicantPersonID, ApplicationDate = @ApplicationDate,ApplicationTypeID = @ApplicationTypeID, ApplicationStatus = @ApplicationStatus,
                             LastStatusDate = @LastStatusDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID
                             WHERE ApplicationID = @ApplicationID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", AppDTO.ApplicationID);
            Command.Parameters.AddWithValue("@ApplicantPersonID", AppDTO.ApplicantPersonID);
            Command.Parameters.AddWithValue("@ApplicationDate", AppDTO.ApplicationDate);
            Command.Parameters.AddWithValue("@ApplicationTypeID", AppDTO.ApplicationTypeID);

            Command.Parameters.AddWithValue("@ApplicationStatus", AppDTO.ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", AppDTO.LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", AppDTO.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", AppDTO.CreatedByUserID);



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

        // Delete
        static public bool Delete(int ApplicationID) {
            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"DELETE FROM Applications
                             WHERE ApplicationID = @ApplicationID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


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

        static public bool UpdateStatus(int ApplicationID, byte Status) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE Applications
                             SET  ApplicationStatus = @ApplicationStatus
                             WHERE ApplicationID = @ApplicationID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationStatus", Status);
            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


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

        static public int DoesPersonHaveAlreadyApplication(int PersonID, int LicenseClassID, byte ApplicationStatus) {

            int ApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT Applications.ApplicationID
                            FROM LocalDrivingLicenseApplications
                            INNER JOIN Applications
	                            ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                            WHERE Applications.ApplicantPersonID = @PersonID AND
                            LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID AND
                            Applications.ApplicationStatus = @ApplicationStatus ;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

            try {
                Connection.Open();

                object res = Command.ExecuteScalar();

                if (res != null && int.TryParse(res.ToString(), out int appID)) {
                    ApplicationID = appID;
                }
                else {
                    ApplicationID = -1;
                }

            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                ApplicationID = -1;

            }
            finally {
                Connection.Close();
            }

            return ApplicationID;

        }

        static public bool DoesApplicationExist(int ApplicationID) {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT 1 FROM Applications  WHERE ApplicationID = @ApplicationID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try {
                Connection.Open();

                object res = Command.ExecuteScalar();

                if(res != null) {

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
        
        // Get Active Application ID
        static public int GetActiveApplicationID(int PersonID, int ApplicationTypeID) {
            int appID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT ApplicationID FROM Applications
                             WHERE ApplicantPersonID = @PersonID AND
                             ApplicationTypeID = @ApplicationTypeID AND 
                             ApplicationStatus = 1; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);


            try {
                Connection.Open();

                object res = Command.ExecuteScalar();

                if(res != null && int.TryParse(res.ToString(), out int applicationID)) {
                    appID = applicationID;
                }
                else {
                    appID = -1;
                }

            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                appID = -1;

            }
            finally {
                Connection.Close();
            }


            return appID;
        }


        // Get Active Application ID For License Class
        static public int GetApplicationIDForLicenseClass(int PersonID, int LicenseClassID, int ApplicationTypeID ,byte ApplicationStatus) {

            int ApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT Applications.ApplicationID
                            FROM Applications
                            INNER JOIN LocalDrivingLicenseApplications
	                            ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID

                            WHERE 
                            Applications.ApplicantPersonID = @PersonID AND
                            LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID AND
                            Applications.ApplicationTypeID = @ApplicationTypeID AND
                            Applications.ApplicationStatus = @ApplicationStatus ;  ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            Command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);

            try {
                Connection.Open();

                object res = Command.ExecuteScalar();

                if (res != null && int.TryParse(res.ToString(), out int appID)) {
                    ApplicationID = appID;
                }
                else {
                    ApplicationID = -1;
                }

            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.GetAllPeople: " + ex.Message);
                ApplicationID = -1;

            }
            finally {
                Connection.Close();
            }

            return ApplicationID;
        }

     


    }
}
