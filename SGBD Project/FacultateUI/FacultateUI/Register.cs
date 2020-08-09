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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        private void Register_Load(object sender, EventArgs e)
        {
            Global.connection = new SqlConnection(Global.strConectare);
            Global.dataSet = new DataSet();

            string strSelect = "select * from tConturi";
            Global.dataAdapter = new SqlDataAdapter(strSelect, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tConturi");

        }
        private bool ExistEmail()
        {
            foreach (DataRow dataRow in Global.dataSet.Tables["tConturi"].Rows)
            {
                if (dataRow["Email"].Equals(txtEmail.Text))
                    return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool admin = false;
            if(String.IsNullOrEmpty(txtEmail.Text)||ExistEmail())
                MessageBox.Show("Email neintrodus sau existent");
        else if(String.IsNullOrEmpty(txtParola.Text))
                MessageBox.Show("Parola neintrodusa");
        else
            {if (txtEmail.Text == "admin" && txtParola.Text == "admin")
                    admin = true;
                DataTable table = Global.dataSet.Tables["tConturi"];

                DataRow row = table.NewRow();
                row["Email"] = txtEmail.Text;
                row["Parola"] = txtParola.Text;
                row["EsteAdmin"] = admin;

                table.Rows.Add(row);

                string strInsert = "insert into tConturi values ('" +
                    txtEmail.Text + "','" +txtParola.Text + "','" + admin + "')";

                SqlCommand cmd = new SqlCommand(strInsert, Global.connection);

                //folosire SqlDataAdapter
                Global.dataAdapter.InsertCommand = cmd;
                Global.dataAdapter.Update(table);
                Global.dataSet.AcceptChanges();


                //Global.con.Open();
                //cmd.ExecuteNonQuery();
                //Global.con.Close();


                txtEmail.Text = "";
               txtParola.Text = "";
                MessageBox.Show("Intregistrare realizata cu succes");
                LogIn log = new LogIn();
                log.ShowDialog();



            }

        }

  
    }
}
