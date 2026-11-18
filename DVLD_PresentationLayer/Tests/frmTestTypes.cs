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

namespace DVLD_PresentationLayer.Tests {

    public partial class frmTestTypes : Form {

        private DataTable _TestTypes;

        private int _RecordsCount {
            get {
                return dgvTestTypes.Rows.Count;
            }
        }


        public frmTestTypes() {
            InitializeComponent();
        }

        private void frmTestTypes_Load(object sender, EventArgs e) {
            _LoadTestTypesIntoDGV();
            _SetDGVSettings();
        }



        private void _LoadTestTypesIntoDGV() {
            _TestTypes = clsTestType.ListTestTypes();
            dgvTestTypes.DataSource = _TestTypes;
        }

        private void _SetDGVSettings() {

            if (_RecordsCount > 0) {
                dgvTestTypes.Columns[0].Width = 75;
                dgvTestTypes.Columns[0].HeaderText = "ID";

                dgvTestTypes.Columns[1].Width = 170;
                dgvTestTypes.Columns[1].HeaderText = "Title";

                dgvTestTypes.Columns[2].Width = 350;
                dgvTestTypes.Columns[2].HeaderText = "Description";

                dgvTestTypes.Columns[3].Width = 120;
                dgvTestTypes.Columns[3].HeaderText = "Fees";


                // Font 
                dgvTestTypes.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
                dgvTestTypes.RowsDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);

            }

        }

        public void TestTypeUpdated() {
            _LoadTestTypesIntoDGV();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void dgvTestTypes_DataSourceChanged(object sender, EventArgs e) {
            lblRecords.Text = $"#Records : {_RecordsCount}";
        }


        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e) {
            int id = (int)dgvTestTypes.CurrentRow.Cells["TestTypeID"].Value;

            frmEditTestType editTestType = new frmEditTestType((clsTestType.enTestType)id);
            editTestType.InfoUpdated += TestTypeUpdated;
            editTestType.ShowDialog();
        }
    }
}
