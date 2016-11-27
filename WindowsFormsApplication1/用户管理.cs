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
    public partial class 用户管理 : Form
    {
        public static string str = "Server=192.168.2.114;User ID=test;Password=test;Database=networkcentre;CharSet=gbk";
        public MySqlConnection con = new MySqlConnection(str);
        public 用户管理()
        {
            InitializeComponent();
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (System.Exception E)
            {
                MessageBox.Show("对不起，数据库连接错误！" + E.Message, "提示");
            }
            finally
            {
                con.Close();
            }
            //dataGridView1.Columns.Add("username","用户名");
            //dataGridView1.Columns.Add("authority", "权限");
            //dataGridView1.Columns.Add("describe", "描述");
            
        }
        ~用户管理()
        {
            if(con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        //声明一个委托
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            UpdateDataGridView();

        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            // e.Cancel=true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            添加用户 fm8 = new 添加用户();
            //this.AddOwnedForm(fm8);
            //fm8.OwnedForms(this);
            //fm8.Parent = this;
            //订阅事件（将事件处理程序添加到事件的调用列表中）
            fm8.UpdateListView1 += new EventHandler(BabyWindow_UpdateListView);
            fm8.Show();
        }

        private void BabyWindow_UpdateListView(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string delete_user = dataGridView1.Rows[index].Cells["用户名"].Value.ToString();
            DialogResult dr = MessageBox.Show("确认删除用户：" + delete_user + " ?", "确认信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string strcmd = "delete from user where username ='" + delete_user + "'";
                MySqlCommand cmd = new MySqlCommand(strcmd, con);
                cmd = new MySqlCommand(strcmd, con);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show("成功删除用户：" + delete_user, "提示信息");
                    UpdateDataGridView();
                }
            }
        }
        private void UpdateDataGridView()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string strcmd = "select username, authority, userdescribe from user";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            DataTable dt=ds.Tables[0];
            DataTable dt_temp = new DataTable();
            dt_temp.Columns.Add("用户名", typeof(string));
            dt_temp.Columns.Add("权限", typeof(string));
            dt_temp.Columns.Add("描述", typeof(string));
            for (int i = 0; i < dt.Rows.Count; i ++)
            {
                DataRow row = dt_temp.NewRow();
                row[0] = dt.Rows[i][0];
                row[2] = dt.Rows[i][2];
                if((int)dt.Rows[i][1]==0)
                {
                    row[1] = "系统管理";
                }
                else if ((int)dt.Rows[i][1] == 1)
                {
                    row[1] = "仓库管理";
                }
                else
                {
                    row[1] = "访问查看";
                }
                dt_temp.Rows.Add(row);

            }

            dataGridView1.DataSource = dt_temp;
            dataGridView1.Rows[0].ReadOnly = true;
            con.Close();
        }

        private void ShowDataGirdView()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string strcmd = "select username, authority, userdescribe from user";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            //dataGridView1.DataSource = ds.Tables[0];
            DataGridViewRow row = new DataGridViewRow();
            int index = 0;
            //dataGridView1.
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                index = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = dr["username"].ToString();
                if (dr["authoruity"].ToString() == "0")
                    dataGridView1.Rows[index].Cells[1].Value = "系统管理";
                else if (dr["authoruity"].ToString() == "1")
                    dataGridView1.Rows[index].Cells[1].Value = "仓库管理";
                else
                    dataGridView1.Rows[index].Cells[1].Value = "访问查看";
                dataGridView1.Rows[index].Cells[2].Value = dr["userdesctibe"].ToString();
            }
            dataGridView1.Columns[0].HeaderCell.Value = "用户名";
            dataGridView1.Columns[1].HeaderCell.Value = "权限";
            dataGridView1.Columns[2].HeaderCell.Value = "描述";

            con.Close();
        }
    }
}
