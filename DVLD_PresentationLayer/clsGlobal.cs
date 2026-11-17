using DVLD_BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_PresentationLayer {

    static public class clsGlobal {

        private static clsLogin _CurrentUser;

        public static clsLogin CurrentUser {

            get {
                return _CurrentUser;
            }

            set {
                _CurrentUser = value;
            }
        }
    }
}
