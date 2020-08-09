using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
namespace FacultateUI
{
    public partial class VizualizareCursuri : Form
    {
        public VizualizareCursuri()
        {
            InitializeComponent();
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private bool ExistaCodCurs()
        {

            foreach (DataRow dataRow in Global.dataSet.Tables["tCursuri"].Rows)
            {
                if (dataRow["CodCurs"].Equals(textBox1.Text))
                    return true;
            }
            return false;
        }

        private bool ExistaNumeCurs()
        {
            foreach (DataRow dataRow in Global.dataSet.Tables["tCursuri"].Rows)
            {
                if (dataRow["NumeCurs"].Equals(textBox1.Text))
                    return true;
            }
            return false;
        }
        private void VizualizareCursuri_Load(object sender, EventArgs e)
        {
            Global.connection = new SqlConnection(Global.strConectare);
            Global.dataSet = new DataSet();

            string strSelect = "select * from tCursuri";
            Global.dataAdapter = new SqlDataAdapter(strSelect, Global.connection);
            Global.dataAdapter.Fill(Global.dataSet, "tCursuri");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "CodCurs")
            {


                dataGridView1.DataSource = Global.dataSet;
                dataGridView1.DataMember = "tCursuri";
                DataView dv = new DataView(Global.dataSet.Tables["tCursuri"]);
                dv.Sort = "CodCurs";
                dataGridView1.DataSource = dv;
                MessageBox.Show("Sortare realizata cu succes!");
            }
            else if (comboBox1.Text == "NumeCurs")
            {
                dataGridView1.DataSource = Global.dataSet;
                dataGridView1.DataMember = "tCursuri";
                DataView dv = new DataView(Global.dataSet.Tables["tCursuri"]);
                dv.Sort = "NumeCurs";
                dataGridView1.DataSource = dv;
                MessageBox.Show("Sortare realizata cu succes!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "CodCurs")
            {
                if (ExistaCodCurs())
                {
                    dataGridView1.DataSource = Global.dataSet;
                    dataGridView1.DataMember = "tCursuri";
                    DataView dv = new DataView(Global.dataSet.Tables["tCursuri"], "CodCurs= '" + textBox1.Text + "'", null, DataViewRowState.CurrentRows);
                    dataGridView1.DataSource = dv;
                    MessageBox.Show("Curs gasit cu succes");
                }
                else
                    MessageBox.Show("Codul cursului nu exista");
            }
            else if (comboBox2.Text == "NumeCurs")
            {
                if (ExistaNumeCurs())
                {
                    dataGridView1.DataSource = Global.dataSet;
                    dataGridView1.DataMember = "tCursuri";
                    DataView dv = new DataView(Global.dataSet.Tables["tCursuri"], "NumeCurs= '" + textBox1.Text + "'", null, DataViewRowState.CurrentRows);
                    dataGridView1.DataSource = dv;

                    MessageBox.Show("Curs gasit cu succes");
                }
                else
                    MessageBox.Show("Numele cursului nu exista!");
            }
        }
    }

    }
