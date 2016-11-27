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
using WindowsFormsApplication1.GlobalVariable;

namespace WindowsFormsApplication1
{
    public partial class 登录 : Form
    {
        GetIP get_ip = new GetIP();
        public static string str = "Server=192.168.2.114;User ID=test;Password=test;Database=networkcentre;CharSet=gbk";
        public MySqlConnection con = new MySqlConnection(str);
        LoginInfo login_info = new LoginInfo();
        public 登录()
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
        }
        public 登录(LoginInfo login_info)
        {
            this.login_info = login_info;
            InitializeComponent();
            //con.Open();
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
        }
        ~登录()
        {
            if (con.State == ConnectionState.Connecting)
            {
                con.Close();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void Login()
        {
            string username;
            string pw;
            username = textBox1.Text;
            pw = textBox2.Text;
            if (username.Length == 0 || pw.Length == 0)
            {
                MessageBox.Show("用户名或密码不能为空，请重新输入！", "提示");
                return;
            }
            string strcmd = "select * from user where username='" + username + "'and password='" + pw + "'";
            MySqlCommand cmd = new MySqlCommand(strcmd, con);
            MySqlDataAdapter ada = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ada.Fill(ds);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("用户名或密码错误，请重新输入！", "提示");
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                string authority_of_user = null;
                authority_of_user = ds.Tables[0].Rows[0][2].ToString();
                string show_welcome_string = "欢迎 " + username + " !";
                MessageBox.Show(show_welcome_string, "提示");
                this.login_info.username = username;
                this.login_info.authority = authority_of_user;
                this.Close();
            }
        }
    }
}
