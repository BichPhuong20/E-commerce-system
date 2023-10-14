using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Shopee
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (typeuser(username.Text, password.Text) == 0)
                MessageBox.Show("tên tài khoản và mật khẩu không chính xác", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (typeuser(username.Text, password.Text) == 1)
            {
                this.Hide();
                Form ad = new NguoiMua(makhachhang(username.Text, password.Text));
                ad.ShowDialog();
                this.Close();
            }
            if (typeuser(username.Text, password.Text) == 2)
            {
                this.Hide();
                Form nhanvien = new NguoiBan(makhachhang(username.Text, password.Text));
                nhanvien.ShowDialog();
                this.Close();
            }
        }
        int typeuser(string username, string password)
        {
            int type=0;
            string query = "select dbo.loginform('" + username + "', '" + password + "')";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                type = (int)command.ExecuteScalar();
                connection.Close();
            }
            return type;
        }
        string makhachhang(string username, string password)
        {
            string ma;
            string query = "select kh.MaKhachHang from TaiKhoan tk join KhachHang kh on tk.MaTaiKhoan = kh.MaKhachHang where" +
                " tk.TenTaiKhoan='" + @username + "' and tk.MatKhau='" + @password + "'";
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                ma = (string)command.ExecuteScalar();
                connection.Close();
            }
            return ma;
        }
    }
}
