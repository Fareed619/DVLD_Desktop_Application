using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DVLD_PresentationLayer.Classes {
    public class clsUtils {

        static public string GenerateNewGuid() {
            return Guid.NewGuid().ToString();
        }

        static public void StoreLoginInfoInTxt(string Username, string Password) {

            string path = @"C:\Users\User\Desktop\FareedDVLD\LoginInfo.txt";
            string data = $"{Username}%{Password}";

            File.WriteAllText(path, data);
        }

        static public void EmptyLoginInfoTxt() {

            string path = @"C:\Users\User\Desktop\FareedDVLD\LoginInfo.txt";

            File.WriteAllText(path, "");
        }

        static public bool GetLoginInfoFromTxt(ref string Username, ref string Password) {
            string path = @"C:\Users\User\Desktop\FareedDVLD\LoginInfo.txt";


            if (!File.Exists(path)) return false;

            using (StreamReader reader = new StreamReader(path)) {
                string line;

                if ((line = reader.ReadLine()) != null) {
                    Username = line.Split('%')[0];
                    Password = line.Split('%')[1];

                    return true;
                }
                else {
                    return false;
                }
            }
        }
    }
}
