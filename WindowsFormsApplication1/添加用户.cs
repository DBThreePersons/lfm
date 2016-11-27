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
    public partial class 添加用户 : Form
    {
        //public Form7 par;
        public static string str = "Server=192.168.2.114;User ID=test;Password=test;Database=networkcentre;CharSet=gbk";
        public MySqlConnection con = new MySqlConnection(str);
        //声明一个事件
        public EventHandler UpdateListView1 { get; set; }
        public 添加用户()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
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
                MessageBox.Show("对不起，数据库连接错误！\n" + E.Message, "提示");
            }
            finally
            {
                con.Close();
            }
        }
        ~添加用户()
        {
            if(con.State == ConnectionState.Connecting)
            {
                con.Close();
            }
        }
        //public Form8(Form7 parent)
        //{
        //    InitializeComponent();
        //    this.par = parent;
        //}

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username;
            string pw;
            string authority;
            string describe = "";
            username = textBox1.Text;
            pw = textBox2.Text;
            authority = comboBox1.Text;
            describe = textBox3.Text;
            if(username.Length == 0 || authority.Length ==0 || pw.Length == 0)
            {
                MessageBox.Show("前三个输入不能为空，请添加内容", "提示");
                return;
            }
            string show_message = "用户名：" + username + " \n权限：";
            int temp = -1;

            //MessageBox.Show(username,"Information");

            string strcmd = "select * from user where username='" + username + "'";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            //MessageBox.Show(ds.Tables[0].Rows[0][1].ToString(), "Information");
            if (ds.Tables[0].Rows.Count == 0)
            {
                this.Close();
                if (authority == "系统管理")
                {
                    show_message += "系统管理员";
                    temp = 0;
                }
                else if (authority == "仓库管理")
                {
                    show_message += "仓库管理员";
                    temp = 1;
                }
                else
                {
                    show_message += "访问查看";
                    temp = 2;
                }

                if(con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                strcmd = string.Format("insert into user(username,authority,password,userdescribe) values('{0}',{1},'{2}','{3}')", username, temp, pw, describe);
                cmd = new MySqlCommand(strcmd, con);
                int result = cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    MessageBox.Show(show_message, "添加用户成功！");
                    strcmd = "select * from user";
                    cmd = new MySqlCommand(strcmd, con);
                    ada = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    ada.Fill(ds);

                    strcmd = "select * from user";
                    cmd = new MySqlCommand(strcmd, con);
                    ada = new MySqlDataAdapter(cmd);
                    ds = new DataSet();
                    ada.Fill(ds);

                    //刷新dataGridView1
                    //par.dataGridView1.DataSource = ds.Tables[0];

                    //引发事件
                    this.UpdateListView1(this, EventArgs.Empty);
                }
                else
                    MessageBox.Show(show_message, "添加用户失败！");
            }
            else
            {
                string used_username = "用户名\"" + username + "\"已经被使用， 请重新输入用户名";
                MessageBox.Show(used_username, "提示");
            }
        }

        private void 添加用户_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
