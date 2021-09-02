using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Demo.DBConnection;
using Demo.Encryption;
using System.Security.Cryptography;

namespace Demo.User
{
    public partial class MarkManager : Form
    {
        public SqlConnection cnn = GetConnection.getConnection();
        //public byte[] sharedKey = null;
        string id_user = Login.id_user;
        string cv_user = Login.cv_user;
        public MarkManager()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        private void MarkManager_Load(object sender, EventArgs e)
        {
            //id_user = Login.id_user;
            allowEdit();
            loadDataListClass();
        }
        //Kiểm tra xem là giáo vụ hay giáo viên để cấp quyền sửa điểm
        private void allowEdit()
        {
            if (cv_user == "1")
            {
                transcript_dgv.ReadOnly = false;
                save_btn.Show();
                cancel_btn.Show();
            }
            else
            {
                transcript_dgv.ReadOnly = true;
                save_btn.Hide();
                cancel_btn.Hide();
            }
        }

        //Lấy cặp public/private key của giáo viên và giáo vụ để generate sharedkey
        public byte[] loadAndGenerateSharedKey(string id)
        {
            byte[] sharedKey = null;
            try
            {
                //cnn.Open();
                string getKeysQuery_GiaoVien = string.Format("SELECT (CONVERT(VARCHAR(MAX),u.PrivKey)), (CONVERT(VARCHAR(MAX),u.PubKey)), u.MaKhoa FROM dbo.tbl_USER AS u JOIN dbo.tbl_KHOA AS k ON k.MaKhoa = u.MaKhoa WHERE u.MaNV = '{0}'", id);
                SqlDataAdapter sda = new SqlDataAdapter(getKeysQuery_GiaoVien, cnn);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                string getKeysQuery_GiaoVu = string.Format("SELECT DISTINCT (CONVERT(VARCHAR(MAX),u.PrivKey)), (CONVERT(VARCHAR(MAX),u.PubKey)), u.MaKhoa FROM dbo.tbl_USER AS u WHERE u.MaKhoa = '{0}' AND u.ChucVu = 2 AND u.PubKey IS NOT NULL", dt1.Rows[0][2]);
                DataTable dt2 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(getKeysQuery_GiaoVu, cnn);
                sda2.Fill(dt2);
                sharedKey = Curve25519.GetSharedSecret(Convert.FromBase64String(dt1.Rows[0][0].ToString()), Convert.FromBase64String(dt2.Rows[0][1].ToString()));
                //cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tạo key.");
            }

            return sharedKey;
        }
        //Lấy danh sách lớp học phần cho combobox
        public void loadDataListClass()
        {
            try
            {
                cnn.Open();
                string loadDataListClassQuery = "SELECT MaLop FROM tbl_LOPHP";
                SqlCommand sc = new SqlCommand(loadDataListClassQuery, cnn);
                SqlDataReader sdr = sc.ExecuteReader();
                while (sdr.Read())
                {
                    malop_cb.Items.Add(sdr[0]);
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tải danh sách lớp.");
            }
        }
        //Lấy bảng điểm theo lớp học phần
        public void loadDataTranscript(string malopString)
        {
            byte[] sharedKey = null;
            try
            {
                if (cv_user == "1")
                {
                    sharedKey = loadAndGenerateSharedKey(id_user);
                }
                else if (cv_user == "2")
                {
                    sharedKey = loadAndGenerateSharedKey(magvpt_tb.Text);
                }

                string loadDataQuery = String.Format("SELECT bd.MaSV AS N'Mã sinh viên', sv.TenSV AS N'Họ và tên', bd.Diem AS N'Điểm' FROM dbo.tbl_BANGDIEM AS bd JOIN dbo.tbl_SINHVIEN AS sv ON sv.MaSV = bd.MaSV WHERE bd.MaLop = '{0}'",malopString);
                SqlDataAdapter sda = new SqlDataAdapter(loadDataQuery, cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                int a = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    a += 1;
                    if (dr[2] == null) { continue; }
                    else
                    {
                        dr[2] = AES.Decrypt256(dr[2].ToString(), sharedKey);
                    }
                }
                transcript_dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi tải danh sách điểm.");
            }
        }

        private void transcript_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        //Load bảng điểm và thông tin lớp học mỗi khi chọn lớp học phần dựa theo mã lớp
        private void malop_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //kiểm tra xem giáo viên có được xem/sửa điểm lớp được chọn hay không
            try
            {
                cnn.Open();
                string authenticationCheckQuery = "";
                if (cv_user == "1") //Nếu user là giáo viên
                {
                    authenticationCheckQuery = string.Format("SELECT COUNT(*) FROM dbo.tbl_LOPHP WHERE MaLop = '{0}' AND MaNV = {1}", malop_cb.Text, id_user);
                }
                else //Nếu user là giáo vụ
                {
                    authenticationCheckQuery = string.Format("SELECT COUNT(*) FROM dbo.tbl_LOPHP AS l JOIN dbo.tbl_USER AS u ON u.MaNV = l.MaNV WHERE l.MaLop = '{0}' AND u.MaKhoa = (SELECT MaKhoa FROM dbo.tbl_USER AS u2 WHERE u2.MaNV = {1} )",malop_cb.Text,id_user);
                }
                SqlDataAdapter check_sda = new SqlDataAdapter(authenticationCheckQuery, cnn);
                DataTable check_dt = new DataTable();
                check_sda.Fill(check_dt);
                if (check_dt.Rows[0][0].ToString() != "0")
                {
                    //Lấy thông tin lớp học (tên lớp, giáo viên phụ trách).
                    string loadInformationClass = string.Format("SELECT l.TenLop, l.MaNV, u.TenNV FROM dbo.tbl_LOPHP AS l JOIN dbo.tbl_USER AS u ON u.MaNV = l.MaNV WHERE l.MaLop = '{0}'", malop_cb.Text);
                    SqlDataAdapter sda = new SqlDataAdapter(loadInformationClass, cnn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    tenlop_tb.Text = dt.Rows[0][0].ToString();
                    magvpt_tb.Text = dt.Rows[0][1].ToString();
                    tengvpt_tb.Text = dt.Rows[0][2].ToString();
                    loadDataTranscript(malop_cb.Text);
                }
                else
                { 
                    MessageBox.Show("Bạn không có quyền xem điểm lớp này.");
                }
            }
            catch (Exception ex)
            {
                cnn.Close();
                MessageBox.Show(ex.ToString());
            }
            cnn.Close();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] sharedKey = null;
                if (cv_user == "1")
                {
                    sharedKey = loadAndGenerateSharedKey(id_user);
                }
                else if (cv_user == "2")
                {
                    sharedKey = loadAndGenerateSharedKey(magvpt_tb.Text);
                }
                cnn.Open();
                foreach (DataGridViewRow dgvr in transcript_dgv.Rows)
                {
                    if (String.IsNullOrEmpty(dgvr.Cells[2].Value.ToString()))
                    {
                        dgvr.Cells[2].Value = "";
                    }
                    try
                    {
                        string updateMarkQuery = string.Format("UPDATE dbo.tbl_BANGDIEM SET Diem = '{0}' WHERE MaSV = '{1}' AND MaLop = '{2}'", AES.Encrypt256(dgvr.Cells[2].Value.ToString(), sharedKey), dgvr.Cells[0].Value, malop_cb.Text);
                        SqlCommand sc = new SqlCommand(updateMarkQuery, cnn);
                        sc.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show($"Có lỗi trong quá trình nhập điểm đối với sinh viên: {dgvr.Cells[1].Value} - {dgvr.Cells[0].Value}");
                    }

                }
                cnn.Close();
                MessageBox.Show("Đã lưu.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lưu điểm thất bại, có lỗi.");
                cnn.Close();
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            loadDataTranscript(malop_cb.Text);
        }
    }
}
