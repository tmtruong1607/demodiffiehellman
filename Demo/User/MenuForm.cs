using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.User
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void profile_btn_Click(object sender, EventArgs e)
        {
                Form prof = new Profile();
                prof.Show();
                this.Hide();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }

        private void ProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form prof_child = new Profile();
            prof_child.Text = "Thông tin cá nhân";
            prof_child.FormBorderStyle = FormBorderStyle.None;
            prof_child.Dock = DockStyle.Fill;
            prof_child.MdiParent = this;
            prof_child.Show();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            this.Close();
        }
    }
}
