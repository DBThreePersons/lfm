using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1.GlobalVariable;

namespace WindowsFormsApplication1
{
    public partial class 主框架 : Form
    {
        static LoginInfo login_info = new LoginInfo();

        private int childFormNumber = 0;

        public 主框架()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "窗口 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            Form login = new 登录(login_info);
            login.StartPosition = FormStartPosition.CenterParent;
            login.ShowDialog();

            //MessageBox.Show(login_info.username, "可以传递变量");

            Form fm5 = new 查询与修改();
            fm5.MdiParent = this;
            fm5.StartPosition = FormStartPosition.CenterScreen;
            fm5.WindowState = FormWindowState.Maximized;
            fm5.Show();
        }

        private bool checkChildFrmExist(string childFrmName)
        {
            foreach (Form childFrm in this.MdiChildren)
            {
                if (childFrm.Name == childFrmName) //用子窗体的Name进行判断，如果存在则将他激活
                {
                    if (childFrm.WindowState == FormWindowState.Minimized)
                        childFrm.WindowState = FormWindowState.Normal;
                    childFrm.Activate();
                    return true;
                }
            }
            return false;
        }

       

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("Form5") == true)
            {
                return;
            }
            Form fm5 = new 查询与修改();
            fm5.MdiParent = this;
            fm5.WindowState = FormWindowState.Maximized;
            fm5.Show();

        }

        private void fileMenu_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("Form5") == true)
            {
                return;
            }
            Form fm5 = new 查询与修改();
            fm5.MdiParent = this;
            fm5.WindowState = FormWindowState.Maximized;
            fm5.Show();
        }

        private void editMenu_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("Form3") == true)
            {
                return;
            }
            Form fm3 = new 资产录入();
            fm3.MdiParent = this;
            fm3.WindowState = FormWindowState.Maximized;
            fm3.Show();
        }

        private void 仓库查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("Form4") == true)
            {
                return;
            }
            Form fm4 = new 仓库查询();
            fm4.MdiParent = this;
            fm4.WindowState = FormWindowState.Maximized;
            fm4.Show();
        }

        private void 仓库资产对比ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("Form6") == true)
            {
                return;
            }
            Form fm6 = new 仓库资产对比();
            fm6.MdiParent = this;
            fm6.WindowState = FormWindowState.Maximized;
            fm6.Show();
        }

        private void toolsMenu_Click(object sender, EventArgs e)
        {
            if (this.checkChildFrmExist("Form7") == true)
            {
                return;
            }
            Form fm7 = new 用户管理();
            fm7.MdiParent = this;
            fm7.WindowState = FormWindowState.Maximized;
            //fm7.Dock = true;
            fm7.Show();
        }
    }
}
