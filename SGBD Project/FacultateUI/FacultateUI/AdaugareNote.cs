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
    public partial class AdaugareNote : Form
    {
        public AdaugareNote()
        {
            InitializeComponent();
        }

        private void AdaugareNote_Load(object sender, EventArgs e)
        {
            Global.connection = new SqlConnection(Global.strConectare);
            Global.dataSet = new DataSet();

            string strSelect = "select * from tNote";
            Global.dataAdapter = new SqlDataAdapter(strSelect, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tNote");

            string strSelect1 = "select * from tCursuri";
            Global.dataAdapter= new SqlDataAdapter(strSelect1, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tCursuri");
            string strSelect2 = "select * from tStudenti";
            Global.dataAdapter = new SqlDataAdapter(strSelect2, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tStudenti");
        }
        private bool ExistaNumeCurs()
        {
            foreach (DataRow dataRow in Global.dataSet.Tables["tCursuri"].Rows)
            {
                if (dataRow["NumeCurs"].Equals(txtNumeCurs.Text))
                    return true;
            }
            return false;
        }
        private bool ExistaStudent()
        {
           
                foreach (DataRow dataRow in Global.dataSet.Tables["tStudenti"].Rows)
                {
                    if (dataRow["CodStudent"].Equals(txtCodStudent.Text))
                        return true;
                }
                return false;
            }


        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtNumeCurs.Text)||!ExistaNumeCurs())
                MessageBox.Show("Nume Curs neintrodus sau inexistent");
            else if (String.IsNullOrEmpty(txtCodStudent.Text)||!ExistaStudent())
                MessageBox.Show(" Cod student neintrodus sau inexistent ");
            else if (String.IsNullOrEmpty(txtNota.Text))
                MessageBox.Show("Introduceti nota studentului");
            else
            {
                DataTable table = Global.dataSet.Tables["tNote"];

                DataRow row = table.NewRow();
                row["NumeCurs"] = txtNumeCurs.Text;
                row["CodStudent"] = txtCodStudent.Text;
                row["Nota"] = txtNota.Text;

                table.Rows.Add(row);

                string strInsert = "insert into tNote values ('" +
                    txtNumeCurs.Text + "','" + txtCodStudent.Text + "','" + txtNota.Text + "')";

                SqlCommand cmd = new SqlCommand(strInsert, Global.connection);

                //folosire SqlDataAdapter
                Global.dataAdapter.InsertCommand = cmd;
                Global.dataAdapter.Update(table);
                Global.dataSet.AcceptChanges();


                //Global.con.Open();
                //cmd.ExecuteNonQuery();
                //Global.con.Close();


                txtNumeCurs.Text = "";
                txtCodStudent.Text = "";
                txtNota.Text = "";
                MessageBox.Show("Nota adaugata cu succes!");

            }
        }
    }
}
