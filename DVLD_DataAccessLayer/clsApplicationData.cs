using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
