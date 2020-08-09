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
    public partial class StergereConturi : Form
    {
        public StergereConturi()
        {
            InitializeComponent();
        }
        private void StergereConturi_Load(object sender, EventArgs e)
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
 
        private void button1_Click(object sender, EventArgs e)
        {
            if(ExistaEmail())
            {
                SqlCommand cmd = new SqlCommand("ps_DeleteCont", Global.connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter p1 = new SqlParameter();
                p1.Value = txtEmail.Text;
                p1.ParameterName = "@Email";
                cmd.Parameters.Add(p1);
                Global.connection.Open();
                cmd.ExecuteNonQuery();
                Global.connection.Close();
                MessageBox.Show("Stergere realizata cu succes!");



            }
            else
                MessageBox.Show("Acest cont nu este inregistrat");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Global.dataSet;
            dataGridView1.DataMember = "tConturi";
            MessageBox.Show("Data grid populat cu succes!");
        }
    }
}
