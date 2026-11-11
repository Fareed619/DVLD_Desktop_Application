using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessLayer
{
    static public class clsCountryData {


        static public DataTable GetAllCountries() {

            DataTable Countries = new DataTable();


            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Countries;";

            SqlCommand Command = new SqlCommand(query, Connection);

            try {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {
                    Countries.Load(reader);
                }
                else
                {
                    Countries = null;
                }

                reader.Close();

            }catch(Exception Ex) {
                Countries = null;
            }
            finally {
                Connection.Close();
            }

            return Countries;


        }


        static public bool FindCountry(int CountryID, ref string CountryName) {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsSettings.ConnectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID;";

            SqlCommand Command = new SqlCommand(query, Connection);

            Command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read()) {

                    CountryName = (string)reader["CountryName"];
                    isFound = true;
                }
                else
                {
                    isFound = false;

                }

                reader.Close();

            }
            catch (Exception Ex)
            {
                isFound = false;
            }
            finally
            {
                Connection.Close();
            }

            return isFound;


        }
    }
}
