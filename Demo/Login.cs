using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Configuration;
using Demo.Encryption;
using Demo.User;

namespace Demo
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string id_user;
        public static string cv_user;
        private void lg_btn_Click(object sender, EventArgs e)
        {
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["DemoDBConnectionString"].ConnectionString;
            MD5 md5Hash = MD5.Create();
            MD5Encryption md5e = new MD5Encryption();
            try
            {
                cnn.Open();
                string login_check = string.Format("SELECT ChucVu, MaNV, WarningStatus FROM tbl_USER WHERE TenDN = '{0}' AND MatKhau = (SELECT CONVERT(VARBINARY(MAX), N'{1}',1))", id_tb.Text, md5e.GetMd5Hash(md5Hash,pw_tb.Text));
                SqlDataAdapter sda = new SqlDataAdapter(login_check, cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 0 && int.Parse(dt.Rows[0][2].ToString()) >= 5) //Kiểm tra xem tài khoản có quá số lần sai mật khẩu không.
                {
                    MessageBox.Show("Tài khoản của bạn đã bị khóa do nhập sai mật khẩu quá số lần cho phép.");
                }
                else if (dt.Rows.Count != 0 && int.Parse(dt.Rows[0][2].ToString()) < 5)
                {
                    MessageBox.Show("Đăng nhập thành công!");
                    if (dt.Rows[0][0].ToString() == "0")
                    {
                        Form usermanager = new UserManager();
                        usermanager.Show();
                    }
                    else
                    {
                        Form menu = new MenuForm();
                        menu.Show();
                    }
                    cv_user = dt.Rows[0][0].ToString();
                    id_user = dt.Rows[0][1].ToString();
                    this.Hide();
                }
                else
                {
                    string warning = string.Format("UPDATE tbl_USER SET WarningStatus = WarningStatus + 1 WHERE TenDN = '{0}'",id_tb.Text);
                    MessageBox.Show("Sai thông tin đăng nhập!");
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
