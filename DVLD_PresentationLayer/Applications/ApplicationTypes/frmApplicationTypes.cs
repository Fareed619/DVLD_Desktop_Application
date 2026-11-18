using DVLD_BusinessLogicLayer;
using DVLD_PresentationLayer.Applications.ApplicationTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer.Applications {

    public partial class frmApplicationTypes : Form {

        private DataTable _ApplicationTypes;

        private int _RecordsCount {
            get {
                return dgvApplicationTypes.Rows.Count;
            }
        }


        public frmApplicationTypes() {

            InitializeComponent();
        }

        private void frmApplicationTypes_Load(object sender, EventArgs e) {

            _LoadDataIntoDGV();
            _SetDGVSettings();
        }




        private void _LoadDataIntoDGV() {
            _ApplicationTypes = clsApplicationType.ListApplicationTypes();
            dgvApplicationTypes.DataSource = _ApplicationTypes;
        }

        private void _SetDGVSettings() {

            if (_RecordsCount > 0) {
                dgvApplicationTypes.Columns[0].Width = 100;
                dgvApplicationTypes.Columns[0].HeaderText = "ID";

                dgvApplicationTypes.Columns[1].Width = 300;
                dgvApplicationTypes.Columns[1].HeaderText = "Title";

                dgvApplicationTypes.Columns[2].Width = 150;
                dgvApplicationTypes.Columns[2].HeaderText = "Fees";


                // Font 
                dgvApplicationTypes.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
                dgvApplicationTypes.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            }

        }

        public void ApplicationTypeUpdated() {
            _LoadDataIntoDGV();
        }



        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void dgvApplicationTypes_DataSourceChanged(object sender, EventArgs e) {
            lblRecords.Text = $"#Records: {_RecordsCount}";
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e) {

            int ApplicationTypeID = (int)dgvApplicationTypes.CurrentRow.Cells["ApplicationTypeID"].Value;

            frmEditApplicationType frm = new frmEditApplicationType(ApplicationTypeID);
            frm.InfoUpdated += ApplicationTypeUpdated;
            frm.ShowDialog();
        }
    }
}
