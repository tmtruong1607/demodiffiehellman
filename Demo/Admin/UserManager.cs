using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Demo.Encryption;
using Demo.DBConnection;
using Demo.User;
using System.Text.RegularExpressions;

namespace Demo
{
    public partial class UserManager : Form
    {
        public SqlConnection cnn = GetConnection.getConnection();
        public UserManager()
        {
            InitializeComponent();
        }
        public void readOnly()
        {
            id_tb.ReadOnly = true;
            pw_tb.ReadOnly = true;
        }
        public void writeAllow()
        {
            id_tb.ReadOnly = false;
            pw_tb.ReadOnly = false;
        }
        public void emptyTextBox()
        {
            id_tb.Text = null;
            pw_tb.Text = null;
        }
        //public SqlConnection openConnection()
        //{
        //    SqlConnection cnn = new SqlConnection();
        //    cnn.ConnectionString = ConfigurationManager.ConnectionStrings["DemoDBConnectionString"].ConnectionString;
        //    return cnn;
        //}
        public void loadData()
        {
            cnn.Open();
            string tmp = cnn.ConnectionString;
            string loadDataQuery = "SELECT u.MaNV AS N'Mã GV', u.TenDN AS N'Tên đăng nhập' , u.TenNV AS N'Họ và tên', convert(varchar(1),u.ChucVu) AS N'Chức vụ', k.TenKhoa AS N'Tên khoa' FROM dbo.tbl_USER AS u LEFT JOIN dbo.tbl_KHOA AS k ON k.MaKhoa = u.MaKhoa";
            SqlDataAdapter sda = new SqlDataAdapter(loadDataQuery, cnn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            users_dgv.DataSource = dt;
            foreach  (DataRow dr in dt.Rows)
            {
                if (dr[3].ToString() == "")
                {
                    continue;
                }
                else
                {
                    int i = int.Parse(dr[3].ToString());
                    if (i==0)
                    {
                        dr[3] = "Administrator";
                    }
                    else
                    {
                        var etmp = (CVenum)(i);
                        dr[3] = etmp.GetEnumDescription();
                    }
                    
                }
            }
            cnn.Close();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {
            readOnly();
            loadData();
        }

        private void users_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            manv_tb.Text = users_dgv.Rows[r].Cells[0].Value.ToString();
            id_tb.Text = users_dgv.Rows[r].Cells[1].Value.ToString();
            pw_tb.Text = "*******";
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            emptyTextBox();
            writeAllow();
            add_btn.Enabled = false;
            del_btn.Enabled = false;
            save_btn.Enabled = true;
            cancel_btn.Enabled = true;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            bool validatePasswordResult = validatePassword(pw_tb.Text);
            if (validatePasswordResult == true)
            {
                MD5 md5Hash = MD5.Create();
                MD5Encryption md5e = new MD5Encryption();
                try
                {
                    cnn.Open();
                    string addQuery = string.Format("INSERT INTO tbl_USER(TenDN, MatKhau) VALUES('{0}',CONVERT(VARBINARY(MAX),'{1}',1))", id_tb.Text, md5e.GetMd5Hash(md5Hash, pw_tb.Text));
                    SqlCommand sc = new SqlCommand(addQuery, cnn);
                    sc.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm.");
                    cnn.Close();
                    add_btn.Enabled = true;
                    del_btn.Enabled = true;
                    save_btn.Enabled = false;
                    cancel_btn.Enabled = false;
                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu phải bao gồm ký tự thường,in hoa, chữ số, ký tự đặc biệt và độ dài lớn hơn 12.");
            }
            
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            emptyTextBox();
            readOnly();
            add_btn.Enabled = true;
            save_btn.Hide();
            cancel_btn.Hide();
        }

        private void del_btn_Click(object sender, EventArgs e)
        {
            if (manv_tb.Text == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để xóa!");
            }
            else
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên: " + manv_tb.Text, "Xóa", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        cnn.Open();
                        string delQuery = string.Format("DELETE FROM dbo.tbl_USER WHERE MaNV = {0}", manv_tb.Text);
                        SqlCommand sc = new SqlCommand(delQuery, cnn);
                        sc.ExecuteNonQuery();
                        cnn.Close();
                        MessageBox.Show("Đã xóa.");
                        loadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else if (dr == DialogResult.No)
                {
                    loadData();
                }

            }

        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form login = new Login();
            login.Show();
            this.Close();
        }
        static bool validatePassword(string password)
        {
            var input = password;
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{12,}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
            if (!hasLowerChar.IsMatch(input))
            {
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
 