using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccessLayer {
    static public class clsTestData {

        static private clsTestDTO _MapDataReaderToTestDTO(SqlDataReader reader) {

            return new clsTestDTO(
                testID: (int)reader["TestID"],
                testAppointmentID: (int)reader["TestAppointmentID"],
                notes: (string)reader["Notes"],
                createdByUserID: (int)reader["CreatedByUserID"],
                testResult: (bool)reader["TestResult"]
            );
        }



        static public clsTestDTO Find(int TestID) {

            clsTestDTO Test = null;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Tests WHERE TestID = @TestID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {
                    Test = _MapDataReaderToTestDTO(reader);

                }
                else {
                    Test = null;
                }

                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                Test = null;

            }
            finally {
                Connection.Close();
            }

            return Test;

        }

        static public int AddNewTest(clsTestDTO TestInfo) {
            int ID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO Tests(TestAppointmentID, TestResult, Notes, CreatedByUserID)
                             VALUES(@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                             SELECT SCOPE_IDENTITY(); ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestInfo.TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestInfo.TestResult);
            Command.Parameters.AddWithValue("@CreatedByUserID", TestInfo.CreatedByUserID);

            if (string.IsNullOrEmpty(TestInfo.Notes)) {
                Command.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
            else {
                Command.Parameters.AddWithValue("@Notes", TestInfo.Notes);
            }


            try {

                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedTest)) {

                    ID = insertedTest;
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

        static public bool UpdateTest(clsTestDTO TestInfo) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE Tests 
                             SET TestAppointmentID = @TestAppointmentID, TestResult = @TestResult, Notes = @Notes, CreatedByUserID = @CreatedByUserID
                             WHERE TestID = @TestID ; ";
                 

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestInfo.TestAppointmentID);
            Command.Parameters.AddWithValue("@TestResult", TestInfo.TestResult);
            Command.Parameters.AddWithValue("@Notes", TestInfo.Notes);
            Command.Parameters.AddWithValue("@CreatedByUserID", TestInfo.CreatedByUserID);


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

        static public bool Delete(int TestID) {
            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"DELETE FROM Tests 
                             WHERE TestID = @TestID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestID", TestID);


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

        static public byte TrialsCount(int LocalDrivingLicenseApplicationID, int TestTypeID) {
            byte Trials = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * 
                            FROM Tests
                            INNER JOIN TestAppointments
	                            ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                            AND TestTypeID = @TestTypeID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if(result != null) {
                    Trials = (byte)result;
                }
    
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                Trials = 0;

            }
            finally {
                Connection.Close();
            }

            return Trials;
        }
    }
}
