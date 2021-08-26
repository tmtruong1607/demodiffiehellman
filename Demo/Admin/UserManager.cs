using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Demo.Encryption;
using Demo.DBConnection;

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
            chucvu_cb.Enabled = false;
        }
        public void writeAllow()
        {
            id_tb.ReadOnly = false;
            pw_tb.ReadOnly = false;
            chucvu_cb.Enabled = true;
        }
        public void emptyTextBox()
        {
            id_tb.Text = null;
            pw_tb.Text = null;
            chucvu_cb.Text = null;
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
            string loadDataQuery = "SELECT u.MaNV AS N'Mã GV', u.TenDN AS N'Tên đăng nhập' , u.TenNV AS N'Họ và tên', u.ChucVu AS N'Chức vụ', k.TenKhoa AS N'Tên khoa' FROM dbo.tbl_USER AS u LEFT JOIN dbo.tbl_KHOA AS k ON k.MaKhoa = u.MaKhoa";
            SqlDataAdapter sda = new SqlDataAdapter(loadDataQuery, cnn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            users_dgv.DataSource = dt;
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
            save_btn.Show();
            cancel_btn.Show();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            MD5 md5Hash = MD5.Create();
            MD5Encryption md5e = new MD5Encryption();
            try
            {
                cnn.Open();
                string addQuery = string.Format("INSERT INTO tbl_USER(TenDN, MatKhau) VALUES('{0}',CONVERT(VARBINARY(MAX),'{1}',1))",id_tb.Text, md5e.GetMd5Hash(md5Hash,pw_tb.Text));
                SqlCommand sc = new SqlCommand(addQuery, cnn);
                sc.ExecuteNonQuery();
                MessageBox.Show("Đã thêm.");
                cnn.Close();
                add_btn.Enabled = true;
                save_btn.Hide();
                cancel_btn.Hide();
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
    }
}
 