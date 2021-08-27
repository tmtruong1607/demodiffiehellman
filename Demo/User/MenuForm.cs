using System;
using System.Drawing;
using System.Windows.Forms;

namespace Demo.User
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
        }
        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            this.Close();
        }

        private void MarkManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }
            Form marmag_child = new MarkManager();
            marmag_child.MdiParent = this;
            marmag_child.Text = "Quản lý điểm";
            marmag_child.FormBorderStyle = FormBorderStyle.None;
            marmag_child.Dock = DockStyle.Fill;
            marmag_child.Show();
        }

        private void ProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }
            Form prof_child = new Profile();
            prof_child.Text = "Thông tin cá nhân";
            prof_child.FormBorderStyle = FormBorderStyle.None;
            prof_child.Dock = DockStyle.Fill;
            prof_child.MdiParent = this;
            prof_child.Show();
        }

        private void MarkManager2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null) { ActiveMdiChild.Close(); }
            Form marmag_child = new MarkManager();
            marmag_child.Text = "Quản lý điểm";
            marmag_child.FormBorderStyle = FormBorderStyle.None;
            marmag_child.Dock = DockStyle.Fill;
            marmag_child.MdiParent = this;
            marmag_child.Show();
        }
    }
}
