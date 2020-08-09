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
    public partial class AdaugareStudenti : Form
    {
        public AdaugareStudenti()
        {
            InitializeComponent();
        }
        private void AdaugareStudenti_Load(object sender, EventArgs e)
        {

            Global.connection = new SqlConnection(Global.strConectare);
            Global.dataSet = new DataSet();

            string strSelect = "select * from tStudenti";
            Global.dataAdapter = new SqlDataAdapter(strSelect, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tStudenti");
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

            if (string.IsNullOrEmpty(txtCodStudent.Text) || ExistaStudent())
                MessageBox.Show("CodStudent neintrodus sau existent");
            else if (String.IsNullOrEmpty(txtNumeStudent.Text))
                MessageBox.Show(" Introduceti numele studentului");
            else if (String.IsNullOrEmpty(txtAnUniversitar.Text))
                MessageBox.Show("Introduceti anul universitar al studentului");
            else
            {
                DataTable table = Global.dataSet.Tables["tStudenti"];

                DataRow row = table.NewRow();
                row["CodStudent"] = txtCodStudent.Text;
                row["NumeStudent"] = txtNumeStudent.Text;
                row["AnUniversitar"] = txtAnUniversitar.Text;

                table.Rows.Add(row);

                string strInsert = "insert into tStudenti values ('" +
                    txtCodStudent.Text + "','" + txtNumeStudent.Text + "','" + txtAnUniversitar.Text + "')";

                SqlCommand cmd = new SqlCommand(strInsert, Global.connection);

                //folosire SqlDataAdapter
                Global.dataAdapter.InsertCommand = cmd;
                Global.dataAdapter.Update(table);
                Global.dataSet.AcceptChanges();


                //Global.con.Open();
                //cmd.ExecuteNonQuery();
                //Global.con.Close();


                txtCodStudent.Text = "";
                txtNumeStudent.Text = "";
                txtAnUniversitar.Text = "";
                MessageBox.Show("Student adaugat cu succes!");
            }


        }
    }
}
