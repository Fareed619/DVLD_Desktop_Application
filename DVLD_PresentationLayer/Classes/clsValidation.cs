using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD_PresentationLayer.Classes {

    static public class clsValidation {

        static public bool ValidateEmail(string Email) {


            Regex EmailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return EmailRegex.IsMatch(Email);
        }


    }
}
