using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public partial class 仓库查询 : Form
    {
        //string str = "Server=192.168.2.114;User ID=test;Password=test;Database=world;CharSet=gbk";
        MySqlConnection mysqlcon = new MySqlConnection("Server=127.0.0.1;User ID=test;Password=12345678;Database=wlzx;CharSet=gbk");

        public 仓库查询()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.textBox1.Enabled = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sbid = textBox1.Text;
           
            try
            {
                mysqlcon.Open();
                string strcmd = "select * from wlzx_ckzb where ID ="+sbid;
                MySqlCommand cmd = new MySqlCommand(strcmd, mysqlcon);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds, "table1");
                dataGridView1.DataSource = ds.Tables["table1"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mysqlcon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                mysqlcon.Open();
                string strcmd = "select * from wlzx_ckzb";
                MySqlCommand cmd = new MySqlCommand(strcmd, mysqlcon);
                MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ada.Fill(ds, "table1");
                dataGridView1.DataSource = ds.Tables["table1"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            mysqlcon.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
