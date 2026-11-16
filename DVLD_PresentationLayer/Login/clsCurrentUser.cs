using DVLD_BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_PresentationLayer.Login {

    static public class clsCurrentUser {

        private static clsLogin _UserLoginInfo;

        public static clsLogin UserLoginInfo {

            get {
                return _UserLoginInfo;
            }

            set {
                _UserLoginInfo = value;
            }
        }
    }
}
