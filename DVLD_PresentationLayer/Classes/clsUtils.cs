using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_PresentationLayer.Classes {
    public class clsUtils {

        static public string GenerateNewGuid() {
            return Guid.NewGuid().ToString();
        }
    }
}
