using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {
    static public class clsTestAppointmentsData {

        static private int _GetNullValueInt(SqlDataReader reader) {

            int colOrdinal = reader.GetOrdinal("RetakeTestApplicationID");

            return reader.IsDBNull(colOrdinal)
                ? -1
                : reader.GetInt32(colOrdinal);
            
        }

        static private clsTestAppointmentDTO _MapDataReaderToTestAppointmentDTO(SqlDataReader reader) {

            return new clsTestAppointmentDTO(
                testAppointmentID: (int)reader["TestAppointmentID"],
                appointmentDate: (DateTime)reader["AppointmentDate"],
                testTypeID: (int)reader["TestTypeID"],
                localDrivingLicenseApplicationID: (int)reader["LocalDrivingLicenseApplicationID"],
                fees: Convert.ToSingle(reader["PaidFees"]),
                createdByUser: (int)reader["CreatedByUserID"],
                isLocked: (bool)reader["IsLocked"],
                retakeTestApplicationID: _GetNullValueInt(reader)

            );
        }

        static public DataTable ListTestAppointments(int LocalDrivingLicenseApplicationID, int TestTypeID) {

            DataTable TestAppointments = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT  
                            TestAppointmentID, AppointmentDate, PaidFees, IsLocked
                            FROM TestAppointments
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                            AND TestTypeID = @TestTypeID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {
                    TestAppointments.Load(reader);
                }
                else {
                    TestAppointments = null;
                }

                reader.Close();
            }
            catch (Exception Ex) {

                TestAppointments = null;
            }
            finally {
                Connection.Close();
            }

            return TestAppointments;
        }

        static public clsTestAppointmentDTO Find(int TestAppointmentID) {

            clsTestAppointmentDTO TestAppointment = null;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {
                    TestAppointment = _MapDataReaderToTestAppointmentDTO(reader);

                }
                else {
                    TestAppointment = null;
                }

                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                TestAppointment = null;

            }
            finally {
                Connection.Close();
            }

            return TestAppointment;
        }

        static public clsTestAppointmentDTO DoesLocalAppHaveActiveAppointment(int LocalDrivingLicenseApplicationID) {

            clsTestAppointmentDTO TestAppointment = null;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND IsLocked = 0;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {
                    TestAppointment = _MapDataReaderToTestAppointmentDTO(reader);

                }
                else {
                    TestAppointment = null;
                }

                reader.Close();
            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                TestAppointment = null;

            }
            finally {
                Connection.Close();
            }

            return TestAppointment;
        }


        static public bool IsTestCompleted(int LocalDrivingLicenseApplicationID, int TestTypeID) {

            int Count = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT COUNT(*) FROM Tests
                            INNER JOIN TestAppointments
	                            ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                            WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestTypeID = @TestTypeID AND TestResult = 1; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try {
                Connection.Open();

                object res = Command.ExecuteScalar();

                if (res != null && int.TryParse(res.ToString(), out int count)) {
                    Count = count;

                }              

            }
            catch (Exception ex) {
                //throw new Exception("Error in clsManagePeopleData.Find: " + ex.Message);
                Count = 0;

            }
            finally {
                Connection.Close();
            }

            return (Count > 0);
        }


        static public bool SetAppointmentLocked(int TestAppointmentID) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE TestAppointments 
                             SET IsLocked = 1
                             WHERE TestAppointmentID = @TestAppointmentID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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

        static public int AddNew(clsTestAppointmentDTO TestAppointmentInfo) {
             
            int ID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID)
                             VALUES(@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);
                             SELECT SCOPE_IDENTITY(); ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestAppointmentInfo.TestTypeID);
            Command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", TestAppointmentInfo.LocalDrivingLicenseApplicationID);
            Command.Parameters.AddWithValue("@AppointmentDate", TestAppointmentInfo.AppointmentDate);
            Command.Parameters.AddWithValue("@PaidFees", TestAppointmentInfo.PaidFees);
            Command.Parameters.AddWithValue("@CreatedByUserID", TestAppointmentInfo.CreatedByUserID);
            Command.Parameters.AddWithValue("@IsLocked", TestAppointmentInfo.IsLocked);

            if(TestAppointmentInfo.RetakeTestApplicationID == -1) {
                Command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            }
            else {
                Command.Parameters.AddWithValue("@RetakeTestApplicationID", TestAppointmentInfo.RetakeTestApplicationID);
            }




            try {

                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedTestType)) {

                    ID = insertedTestType;
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

        static public bool Update(int TestAppointmentID, DateTime TestAppointmentDate) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE TestAppointments 
                             SET AppointmentDate = @AppointmentDate
                             WHERE TestAppointmentID = @TestAppointmentID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@AppointmentDate", TestAppointmentDate);
            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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

        static public bool Delete(int TestAppointmentID) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"DELETE FROM TestAppointments 
                             WHERE TestAppointmentID = @TestAppointmentID ; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


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
   

    
    }
}
