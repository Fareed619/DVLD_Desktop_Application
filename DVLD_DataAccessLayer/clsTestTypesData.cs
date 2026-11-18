using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {

    static public class clsTestTypesData {



        static private clsTestTypeDTO MapReaderToTestTypeDTO(SqlDataReader reader) {


            return new clsTestTypeDTO(

                (int)reader["TestTypeID"],
                (string)reader["TestTypeTitle"],
                (string)reader["TestTypeDescription"],
                Convert.ToSingle(reader["TestTypeFees"])
            );


        }



        static public DataTable ListTestTypes() {

            DataTable TestTypes = new DataTable();

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM TestTypes; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.HasRows) {
                    TestTypes.Load(reader);
                }
                else {
                    TestTypes = null;
                }

                reader.Close();
            }
            catch (Exception Ex) {

                TestTypes = null;
            }
            finally {
                Connection.Close();
            }

            return TestTypes;
        }

        static public clsTestTypeDTO Find(int TestTypeID) {

            clsTestTypeDTO TestTypeDTO;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM TestTypes 
                            WHERE TestTypeID = @TestTypeID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try {

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    TestTypeDTO = MapReaderToTestTypeDTO(reader);
                }
                else {
                    TestTypeDTO = null;
                }

            }
            catch (Exception Ex) {
                TestTypeDTO = null;


            }
            finally {
                Connection.Close();
            }

            return TestTypeDTO;
        }

        static public int Insert(clsTestTypeDTO TestTypeDTO) {

            int ID = -1;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"INSERT INTO TestTypes(TestTypeTitle, TestTypeDescription, TestTypeFees)
                             VALUES(@Title, @Description, @NewFees);
                             SELECT SCOPE_IDENTITY(); ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@Title", TestTypeDTO.TestTypeTitle);
            Command.Parameters.AddWithValue("@Description", TestTypeDTO.TestTypeDescription);
            Command.Parameters.AddWithValue("@Fees", TestTypeDTO.TestTypeFees);

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

        static public bool Update(clsTestTypeDTO TestTypeDTO) {

            int rowAffacted = 0;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"UPDATE TestTypes 
                             SET TestTypeFees = @NewFees, TestTypeTitle = @Title, TestTypeDescription = @Description
                             WHERE TestTypeID = @ID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@NewFees", TestTypeDTO.TestTypeFees);
            Command.Parameters.AddWithValue("@ID", TestTypeDTO.TestTypeID);
            Command.Parameters.AddWithValue("@Title", TestTypeDTO.TestTypeTitle);
            Command.Parameters.AddWithValue("@Description", TestTypeDTO.TestTypeDescription);

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
