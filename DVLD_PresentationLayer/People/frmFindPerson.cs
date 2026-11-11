using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.People {
    public partial class frmFindPerson : Form {

        public event Action<int> DataBack;

        public frmFindPerson() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();

            DataBack?.Invoke(ctrlPersonCardWithFilter1.PersonID);
        }
    }
}
