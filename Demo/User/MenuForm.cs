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
    }
}
