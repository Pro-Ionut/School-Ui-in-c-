using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FacultateUI
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            Global.connection = new SqlConnection(Global.strConectare);
            Global.dataSet = new DataSet();

            string strSelect = "select * from tConturi";
            Global.dataAdapter = new SqlDataAdapter(strSelect, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tConturi");

        }
        private bool ExistaEmail()
        {
            foreach (DataRow dataRow in Global.dataSet.Tables["tConturi"].Rows)
            {
                if (dataRow["Email"].Equals(txtEmail.Text))
                    return true;
            }
            return false;
        }
        private bool ExistaParola()
        {
            foreach (DataRow dataRow in Global.dataSet.Tables["tConturi"].Rows)
            {
                if (dataRow["Parola"].Equals(txtParola.Text))
                    return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        { if(String.IsNullOrEmpty(txtEmail.Text)||!ExistaEmail())
                MessageBox.Show("Email neintrodus sau inexistent");
        else if(String.IsNullOrEmpty(txtParola.Text)||!ExistaParola())
                MessageBox.Show("Parola neintrodusa sau inexistenta");
            else
            {

                bool admin = false;
                if (txtEmail.Text == "admin" && txtParola.Text == "admin")
                    admin = true;
                if(admin==true)
                {
                    MessageBox.Show("Salut admin!");
                    DashboardAdmin dashboardAdmin = new DashboardAdmin();
                    dashboardAdmin.ShowDialog();
                  
                }
                else
                {
                    MessageBox.Show("Salut" + txtEmail.Text);
                    DashboardUser user = new DashboardUser();
                    user.ShowDialog();
                    
                }
            }

        }
    }
}
