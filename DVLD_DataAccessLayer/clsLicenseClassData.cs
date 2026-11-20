using DVLD_SharedDTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer {

    static public class clsLicenseClassData {

        static private clsLicenseClassDTO MapReaderToLicenseClassDTO (SqlDataReader reader) {

            return new clsLicenseClassDTO(

                licenseClassID: (int)reader["LicenseClassID"],
                className: (string)reader["ClassName"],
                classDescription: (string)reader["ClassDescription"],
                minAgeAllowed: (byte)reader["MinAgeAllowed"],
                defaultValidityLength: (byte)reader["DefaultValidityLength"],
                classFees: Convert.ToSingle(reader["ClassFess"])

            );
        }



        static public clsLicenseClassDTO Find(int LicenseClassID) {

            clsLicenseClassDTO LicenseClassDTO;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM LicenseClasses 
                            WHERE LicenseClassID = @LicenseClassID; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try {

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    LicenseClassDTO = MapReaderToLicenseClassDTO(reader);
                }
                else {
                    LicenseClassDTO = null;
                }

            }
            catch (Exception Ex) {
                LicenseClassDTO = null;


            }
            finally {
                Connection.Close();
            }

            return LicenseClassDTO;

        }

        static public clsLicenseClassDTO Find(string ClassName) {

            clsLicenseClassDTO LicenseClassDTO;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = @"SELECT * FROM LicenseClasses 
                            WHERE ClassName = @ClassName; ";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try {

                Connection.Open();
                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    LicenseClassDTO = MapReaderToLicenseClassDTO(reader);
                }
                else {
                    LicenseClassDTO = null;
                }

            }
            catch (Exception Ex) {
                LicenseClassDTO = null;


            }
            finally {
                Connection.Close();
            }

            return LicenseClassDTO;

        }
       

    }
}
