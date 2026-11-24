using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DVLD_PresentationLayer.Applications.Local_License {
    public partial class frmShowLocalDrivingLicenseApplicationInfo : Form {

        private int _LDLAppID = -1;
        public frmShowLocalDrivingLicenseApplicationInfo(int lDLAppID) {
            InitializeComponent();

            _LDLAppID = lDLAppID;
        }

        private void frmShowLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e) {


            ctrlShowApplicationInfo1.LoadApplicationInfoByLocalDrivingLicenseApplicaton(_LDLAppID);
        }
            
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
