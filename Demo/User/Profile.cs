using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Demo.User
{
    public partial class Profile : Form
    {
        string id_user = Login.id_user;
        public Profile()
        {
            InitializeComponent();
        }
        public void EncodeUTF8(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            str = Encoding.UTF8.GetString(bytes);
        }
        public SqlConnection cnn = GetConnection.getConnection();
        public void loadData()
        {
            try
            {
                cnn.Open();
                string tmp = cnn.ConnectionString;
                //Query để load thông tin cá nhân user
                string loadDataQuery = string.Format("SELECT u.MaNV, u.TenNV, u.Email, u.SDT, u.ChucVu, k.TenKhoa, u.PubKey, u.MaKhoa FROM dbo.tbl_USER AS u LEFT JOIN dbo.tbl_KHOA AS k ON k.MaKhoa = u.MaKhoa WHERE u.MaNV = '{0}'", id_user);
                SqlDataAdapter sda = new SqlDataAdapter(loadDataQuery, cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //Query để load danh sách khoa
                string loadListQuery = "SELECT TenKhoa FROM tbl_KHOA";
                SqlCommand sc = new SqlCommand(loadListQuery, cnn);
                SqlDataReader sdr = sc.ExecuteReader();
                while (sdr.Read())
                {
                    khoa_cb.Items.Add(sdr[0]);
                }
                cnn.Close();
                cv_cb.DataSource = Enum.GetValues(typeof(CVenum));
                magv_tb.Text = dt.Rows[0][0].ToString();
                ten_tb.Text = dt.Rows[0][1].ToString();
                email_tb.Text = dt.Rows[0][2].ToString();
                sdt_tb.Text = dt.Rows[0][3].ToString();
                cv_cb.Text = Enum.GetName(typeof(CVenum), dt.Rows[0][4]);
                khoa_cb.Text = dt.Rows[0][5].ToString();
                pubkey_tb.Text = dt.Rows[0][6].ToString();
                makhoa_tb.Text = dt.Rows[0][7].ToString();
               
                //kiểm tra xem là giáo vụ hay giáo viên để cho phép nhập pubkey tùy chọn

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (cv_cb.SelectedItem == null || khoa_cb.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ chức vụ và khoa.");
            }
            else
            {
                try
                {
                    cnn.Open();
                    //Query để lưu lại thông tin user
                    string updateQuery = string.Format("UPDATE dbo.tbl_USER SET TenNV=N'{1}',Email='{2}',SDT='{3}',ChucVu={4},MaKhoa = '{5}', PubKey= N'{6}' WHERE MaNV = '{0}'", magv_tb.Text, ten_tb.Text, email_tb.Text, sdt_tb.Text, (int)(CVenum)cv_cb.SelectedItem, makhoa_tb.Text, pubkey_tb.Text);
                    SqlCommand sc = new SqlCommand(updateQuery, cnn);
                    sc.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu!");
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        public void pubkeyUpdate()
        {
            string pubk;
            if ((CVenum)cv_cb.SelectedItem == CVenum.GiaoVu)
            {
                inspubkey_tb.ReadOnly = true;
                //encode utf8 tên khoa
                string p = khoa_cb.Text;
                EncodeUTF8(p);
                pubk = makhoa_tb.Text + p;
                pubkey_tb.Text = pubk;
            }
            else
            {
                inspubkey_tb.ReadOnly = false;
                //encode utf8 mã nv
                string p = magv_tb.Text;
                EncodeUTF8(p);
                pubk = inspubkey_tb.Text + p;
                pubkey_tb.Text = pubk;
            }
        }
        private void cv_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            pubkeyUpdate();
        }

        private void khoa_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (cnn.State.ToString() == "Open") { cnn.Close(); }
                cnn.Open();
                //Query lấy mã khoa
                string getDataQuery = string.Format("SELECT MaKhoa FROM tbl_KHOA WHERE TenKhoa = N'{0}'",khoa_cb.Text);
                SqlDataAdapter sda = new SqlDataAdapter(getDataQuery, cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                makhoa_tb.Text = dt.Rows[0][0].ToString();
                pubkeyUpdate();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void inspubkey_tb_TextChanged(object sender, EventArgs e)
        {
            pubkeyUpdate();
        }
    }
}
