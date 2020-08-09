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
    public partial class AdaugareProfesori : Form
    {
        public AdaugareProfesori()
        {
            InitializeComponent();
        }
        private void AdaugareProfesori_Load(object sender, EventArgs e)
        {

            Global.connection = new SqlConnection(Global.strConectare);
            Global.dataSet = new DataSet();

            string strSelect = "select * from tProfesori";
            Global.dataAdapter = new SqlDataAdapter(strSelect, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tProfesori");
        }
        private bool ExistaNumeCurs()
        {
            foreach (DataRow dataRow in Global.dataSet.Tables["tProfesori"].Rows)
            {
                if (dataRow["NumeCurs"].Equals(txtNumeCurs.Text))
                    return true;
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNume.Text))
                MessageBox.Show("Introduceti numele profesorului");
            else if (string.IsNullOrEmpty(txtVechime.Text))
                MessageBox.Show("Introduceti Vechimea profesorului");
            else if (String.IsNullOrEmpty(txtNumeCurs.Text) || ExistaNumeCurs())
                MessageBox.Show("Nume curs neintrodus sau existent");
            else
            {
                DataTable table = Global.dataSet.Tables["tProfesori"];

                DataRow row = table.NewRow();
                row["Nume"] = txtNume.Text;
                row["Vechime"] = txtVechime.Text;
                row["NumeCurs"] = txtNumeCurs.Text;

                table.Rows.Add(row);

                string strInsert = "insert into tProfesori values ('" +
                    txtNume.Text + "','" + txtVechime.Text + "','" + txtNumeCurs.Text + "')";

                SqlCommand cmd = new SqlCommand(strInsert, Global.connection);

                //folosire SqlDataAdapter
                Global.dataAdapter.InsertCommand = cmd;
                Global.dataAdapter.Update(table);
                Global.dataSet.AcceptChanges();


                //Global.con.Open();
                //cmd.ExecuteNonQuery();
                //Global.con.Close();


                txtNume.Text = "";
                txtVechime.Text = "";
                txtNumeCurs.Text = "";
                MessageBox.Show("Profesor adaugat cu succes!");

            }
        }


        }
}
