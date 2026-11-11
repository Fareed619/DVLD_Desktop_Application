using DVLD_BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.Forms
{
    public partial class frmPersonDetails : Form {

        public event Action PersonUpdateCompleted;

        private int _PersonID = -1;
        private string _NationalNo = string.Empty;

        public frmPersonDetails(int PersonID) {
            InitializeComponent();

            ctrlPersonInformation1.WorkCompleted += PersonUpdated;
            _PersonID = PersonID;
        }

        public frmPersonDetails(string NationalNo) {
            InitializeComponent();

            ctrlPersonInformation1.WorkCompleted += PersonUpdated;
            _NationalNo = NationalNo;
        }

        private void frmPersonDetails_Load(object sender, EventArgs e) {

            if(_PersonID == -1) {
                ctrlPersonInformation1.LoadPersonInfo(_NationalNo);

            }
            else {
                ctrlPersonInformation1.LoadPersonInfo(_PersonID);
            }

        }
        
        private void PersonUpdated() {
            PersonUpdateCompleted?.Invoke();
        }

        private void btnClose_Click(object sender, EventArgs e) {

            this.Close();

        }
    }
}
