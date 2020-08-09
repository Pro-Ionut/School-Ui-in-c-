using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacultateUI
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
            log.ShowDialog();
        }
    }
}
