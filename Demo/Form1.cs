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


namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            // Return the hexadecimal string.
            return "0x" + sBuilder.ToString();
        }


        private void lg_btn_Click(object sender, EventArgs e)
        {
            DBConnection dbc = new DBConnection();
            SqlConnection cnn = dbc.DB_Connection();
            try
            {
                cnn.Open();
                MD5 md5Hash = MD5.Create();
                string login_check = string.Format("SELECT COUNT(*) FROM tbl_USER WHERE TenDN = '{0}' AND MatKhau = (SELECT CONVERT(VARBINARY(MAX), N'{1}',1))", id_tb.Text, (GetMd5Hash(md5Hash, pw_tb.Text)));
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
