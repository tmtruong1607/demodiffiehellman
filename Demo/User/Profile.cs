using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Demo.DBConnection;
using Demo.Encryption;

namespace Demo.User
{
    public partial class Profile : Form
    {
        string id_user = Login.id_user; //Lấy id user sau khi đăng nhập
        public SqlConnection cnn = GetConnection.getConnection();
        public static byte[] publicKey;
        public static byte[] privateKey;
        bool check = false; //đặt biến kiểm tra xem user đã có thông tin (khoa, chức vụ, cặp key) chưa

        public Profile()
        {
            InitializeComponent();
        }
        protected override void OnResize(EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        public void EncodeUTF8(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            str = Encoding.UTF8.GetString(bytes);
        }
        byte[] generatePrivKey(string privk) //tạo private key
        {
            byte[] tmp = Encoding.UTF8.GetBytes(privk);
            return Curve25519.ClampPrivateKey(tmp);
        }

        public void privateKeyUpdate()
        {
            //Kiểm tra xem chức vụ là gì để xây dựng pubkey tương ứng.
            if ((CVenum)cv_cb.SelectedValue == CVenum.GiaoVu)
            {
                optionkey_tb.Hide();
                optionkey_lb.Hide();
                string privateKeyString = makhoa_tb.Text + khoa_cb.Text;
                privateKey = generatePrivKey(privateKeyString);
                publicKey = Curve25519.GetPublicKey(privateKey);
            }
            else
            {
                optionkey_tb.Show();
                optionkey_lb.Show();
                string privateKeyString = optionkey_tb.Text + magv_tb.Text;
                privateKey = generatePrivKey(privateKeyString);
                publicKey = Curve25519.GetPublicKey(privateKey);
            }
        }
        public void loadData()
        {
            try
            {
                cnn.Open();
                string tmp = cnn.ConnectionString;
                //Query để load thông tin cá nhân user
                string loadDataQuery = string.Format("SELECT u.MaNV, u.TenNV, u.Email, u.SDT, u.ChucVu, k.TenKhoa, u.MaKhoa, CONVERT(VARCHAR(MAX),u.OptionKey) FROM dbo.tbl_USER AS u LEFT JOIN dbo.tbl_KHOA AS k ON k.MaKhoa = u.MaKhoa WHERE u.MaNV = '{0}'", id_user);
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
                cv_cb.DisplayMember = "Description";
                cv_cb.ValueMember = "Value";
                cv_cb.DataSource = Enum.GetValues(typeof(CVenum))
                    .Cast<Enum>()
                    .Select(value => new
                    {
                        (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                        value
                    })
                    .OrderBy(item => item.value)
                    .ToList();
                magv_tb.Text = dt.Rows[0][0].ToString();
                ten_tb.Text = dt.Rows[0][1].ToString();
                email_tb.Text = dt.Rows[0][2].ToString();
                sdt_tb.Text = dt.Rows[0][3].ToString();
                int ind;
                //Lấy giá trị của chức vụ (1,2)
                if (dt.Rows[0][4].ToString() == "") { ind = 1; }
                else
                {
                    ind = (int)dt.Rows[0][4];
                    cv_cb.Enabled = false;
                } 
                cv_cb.SelectedIndex = ind-1; //Gán giá trị của chức vụ vào combobox để làm giá trị mặc định khi load.
                //Kiểm tra xem user đã có chức vụ, khoa chưa, nếu có rồi thì sẽ ẩn, không cho user thay đổi giá trị đó, tránh trường hợp private key bị thay đổi dẫn đến lỗi khi xem điểm đã mã hóa bằng private key cũ. User sẽ chỉ được cập nhật thông tin chức vụ, mã khoa một lần.
                if (dt.Rows[0][5].ToString() != "") { khoa_cb.Enabled = false; }
                khoa_cb.Text = dt.Rows[0][5].ToString();
                if (dt.Rows[0][6].ToString() != "") { makhoa_tb.ReadOnly = true; }
                makhoa_tb.Text = dt.Rows[0][6].ToString();
                optionkey_tb.Text = dt.Rows[0][7].ToString(); //Nếu đã có private key thì sẽ ẩn phần nhập private key, tránh trường hợp thay đổi private key dẫn đến lỗi khi xem điểm đã được mã hóa bằng private key cũ.
                if (optionkey_tb.Text != "")
                {
                    check = true;
                    optionkey_tb.ReadOnly = true;
                }
                else
                {
                    check = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tải thông tin.");
                cnn.Close();
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
                    string updateQuery = "";
                    //Kiểm tra xem user đã có private key chưa (tức là đã khai báo chức vụ, mã khoa), nếu đã có thì chỉ update thông tin liên hệ cá nhân, các thông tin có thể dẫn đến việc thay đổi private key sẽ không được update hay sửa đổi.
                    if (check == false)
                    {
                        updateQuery = string.Format("UPDATE dbo.tbl_USER SET TenNV=N'{1}',Email='{2}',SDT='{3}',ChucVu={4},MaKhoa = '{5}', PubKey= (SELECT CONVERT(VARBINARY(MAX),'{6}')), PrivKey= (SELECT CONVERT(VARBINARY(MAX),'{7}')), OptionKey = (SELECT CONVERT(VARBINARY(MAX),'{8}')) WHERE MaNV = '{0}'", magv_tb.Text, ten_tb.Text, email_tb.Text, sdt_tb.Text, (int)(CVenum)cv_cb.SelectedValue, makhoa_tb.Text, Convert.ToBase64String(publicKey), Convert.ToBase64String(privateKey), optionkey_tb.Text);
                    }
                    else
                    {
                        updateQuery = string.Format("UPDATE dbo.tbl_USER SET TenNV=N'{1}',Email='{2}',SDT='{3}' WHERE MaNV = '{0}'", magv_tb.Text, ten_tb.Text, email_tb.Text, sdt_tb.Text);
                    }
                        
                    SqlCommand sc = new SqlCommand(updateQuery, cnn);
                    sc.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu!");
                    cnn.Close();
                    loadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lưu thất bại, có lỗi!");
                    cnn.Close();
                }
            }
        }
        
        private void cv_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            privateKeyUpdate();
        }

        private void khoa_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();
                //Query lấy mã khoa
                string getDataQuery = string.Format("SELECT MaKhoa FROM tbl_KHOA WHERE TenKhoa = N'{0}'",khoa_cb.Text);
                SqlDataAdapter sda = new SqlDataAdapter(getDataQuery, cnn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                makhoa_tb.Text = dt.Rows[0][0].ToString();
                privateKeyUpdate();
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                cnn.Close();
            }
        }

        private void insprivkey_tb_TextChanged(object sender, EventArgs e)
        {
            privateKeyUpdate();
        }
    }
}
