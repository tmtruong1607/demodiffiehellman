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
        string id_user = Login.id_user;
        public MarkManager()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        public byte[] loadAndGenerateSharedKey()
        {
            byte[] sharedKey = null;
            try
            {
                cnn.Open();
                string getKeysQuery_GiaoVien = string.Format("SELECT (CONVERT(VARCHAR(MAX),u.PrivKey)), (CONVERT(VARCHAR(MAX),u.PubKey)), u.MaKhoa FROM dbo.tbl_USER AS u JOIN dbo.tbl_KHOA AS k ON k.MaKhoa = u.MaKhoa WHERE u.MaNV = '{0}'", id_user);
                SqlDataAdapter sda = new SqlDataAdapter(getKeysQuery_GiaoVien, cnn);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                string getKeysQuery_GiaoVu = string.Format("SELECT DISTINCT (CONVERT(VARCHAR(MAX),u.PrivKey)), (CONVERT(VARCHAR(MAX),u.PubKey)), u.MaKhoa FROM dbo.tbl_USER AS u WHERE u.MaKhoa = '{0}' AND u.ChucVu = 2 AND u.PubKey IS NOT NULL", dt1.Rows[0][2]);
                DataTable dt2 = new DataTable();
                SqlDataAdapter sda2 = new SqlDataAdapter(getKeysQuery_GiaoVu, cnn);
                sda2.Fill(dt2);
                
                sharedKey = Curve25519.GetSharedSecret(Convert.FromBase64String(dt1.Rows[0][0].ToString()), Convert.FromBase64String(dt2.Rows[0][1].ToString()));
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return sharedKey;
        }
        public SqlConnection cnn = GetConnection.getConnection();
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
                MessageBox.Show(ex.ToString());
            }
        }
        //Lấy bảng điểm theo lớp học phần
        private String Encrypt256(string mark, byte[] sharedKey)
        {
            // AesCryptoServiceProvider
            byte[] Key = sharedKey;
            Array.Resize(ref Key, 32);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            //aes.GenerateKey();
            aes.Key = Key;
            aes.IV = Encoding.UTF8.GetBytes(@"!QAZ2WSX#EDC4RFV");
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            //string markString = mark.ToString();
            // Convert string to byte array
            byte[] src = System.Text.ASCIIEncoding.ASCII.GetBytes(mark);
            //MessageBox.Show(Encoding.UTF8.GetString(src));

            // encryption
            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                string result = Convert.ToBase64String(dest);
                //test_tb.Text = PrintByteArray(dest);
                return result;
                // Convert byte array to Base64 strings
                //return Convert.ToBase64String(dest);
            }
        }

        private string Decrypt256(string markEncrypted, byte[] sharedKey)
        {
            byte[] Key = sharedKey;
            Array.Resize(ref Key, 32);
            // AesCryptoServiceProvider
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = Encoding.UTF8.GetBytes(@"!QAZ2WSX#EDC4RFV");
            aes.Key = Key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            // Convert Base64 strings to byte array
            byte[] src = Convert.FromBase64String(markEncrypted);

            // decryption
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return  Encoding.UTF8.GetString(dest);
                //float markDecrypted = float.Parse(tmp);
                //return markDecrypted;
            }
        }
        public void loadDataTranscript(byte[] sharedKey, string malopString)
        {
            try
            {
                cnn.Open();
                string loadDataQuery = String.Format("SELECT bd.MaSV AS N'Mã sinh viên', sv.TenSV AS N'Họ và tên', (SELECT CONVERT(VARCHAR(MAX),bd.Diem)) AS N'Điểm' FROM dbo.tbl_BANGDIEM AS bd JOIN dbo.tbl_SINHVIEN AS sv ON sv.MaSV = bd.MaSV WHERE bd.MaLop = '{0}'",malopString);
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
                        dr[2] = Decrypt256(dr[2].ToString(), sharedKey);
                    }
                }
                transcript_dgv.DataSource = dt;
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void transcript_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MarkManager_Load(object sender, EventArgs e)
        {
            //id_user = Login.id_user;
            byte[] sharedKey = loadAndGenerateSharedKey();
            loadDataListClass();
        }

        private void malop_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] tmp = null;
            loadDataTranscript(tmp, malop_cb.Text);
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] sharedKey = loadAndGenerateSharedKey();
                cnn.Open();
                foreach (DataGridViewRow dgvr in transcript_dgv.Rows)
                {
                    try
                    {
                        string updateMarkQuery = string.Format("UPDATE dbo.tbl_BANGDIEM SET Diem = (SELECT CONVERT(VARBINARY(MAX),'{0}')) WHERE MaSV = '{1}' AND MaLop = '{2}'", Encrypt256(dgvr.Cells[2].Value.ToString(), sharedKey), dgvr.Cells[0].Value, malop_cb.Text);
                        SqlCommand sc = new SqlCommand(updateMarkQuery, cnn);
                        sc.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show($"Có lỗi trong quá trình nhập điểm đối với sinh viên: {dgvr.Cells[1].Value} - {dgvr.Cells[0].Value}");
                    }

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
