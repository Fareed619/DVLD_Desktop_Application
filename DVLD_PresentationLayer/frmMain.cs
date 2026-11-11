using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_PresentationLayer
{
    public partial class frmMain : Form {

        public frmMain() {

            InitializeComponent();
        }

       private void MainForm_Load(object sender, EventArgs e) {

          
        }

        private void applicationstoolStripMenuItem_Click(object sender, EventArgs e){
            frmApplications frm = new frmApplications();
            frm.ShowDialog();

        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e) {
            frmPeople frm = new frmPeople();
            frm.ShowDialog();

        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e) {
            frmDrivers frm = new frmDrivers();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e) {
            frmUsers frm = new frmUsers();
            frm.ShowDialog();
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
            frmSettings frm = new frmSettings();
            frm.ShowDialog();
        }
    }
}
