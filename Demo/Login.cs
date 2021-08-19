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
using System.Data;
using System.Security.Cryptography;
using Demo.Encryption;

namespace Demo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void lg_btn_Click(object sender, EventArgs e)
        {
            DBConnection dbc = new DBConnection();
            SqlConnection cnn = dbc.DB_Connection();
            MD5 md5Hash = MD5.Create();
            MD5En md5e = new MD5En();
            string md5str = md5e.GetMd5Hash(md5Hash, pw_tb.Text.ToString());

            try
            {
                cnn.Open();
                string login_check = string.Format("SELECT COUNT(*) FROM tbl_USER WHERE TenDN = '{0}' AND MatKhau = (SELECT CONVERT(VARBINARY(MAX), N'{1}',1))", id_tb.Text, (md5e.GetMd5Hash(md5Hash, pw_tb.Text)));
                SqlDataAdapter sda = new SqlDataAdapter(login_check, cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    MessageBox.Show("Login success!");
                    Form menu = new MenuForm();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
