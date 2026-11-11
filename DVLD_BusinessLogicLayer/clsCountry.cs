using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLogicLayer
{
    public class clsCountry {
        
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        clsCountry(int ID, string Name) {
           this.CountryID = ID;
           this.CountryName = Name;
        }

        static public DataTable GetAllCountries() {
            return clsCountryData.GetAllCountries();
        }

        static public clsCountry Find(int CountryID) {

            string CountryName = "";

            if (clsCountryData.FindCountry(CountryID, ref CountryName)) {

                return new clsCountry(CountryID, CountryName);
            }
            else {
                return null;
            }
        }
    }
}
