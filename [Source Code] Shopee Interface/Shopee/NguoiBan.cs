using System;
using System.IO;
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
    public partial class NguoiBan : Form
    {
        public static string manguoiban;
        public BindingSource SanPham = new BindingSource();
        public BindingSource DonHang = new BindingSource();
        public NguoiBan(string makhachhang)
        {
            manguoiban = makhachhang;
            InitializeComponent();
            Load();
        }
        void Load()
        {
            dataGridView1.DataSource = SanPham;
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data = new DataSet();
                string query = "select * from SanPham where MaNguoiBan='"+manguoiban+"'";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                SanPham.DataSource = data.Tables[0];
                SoLuong.Text = data.Tables[0].Rows.Count.ToString();
                connection.Close();
            }
            textBox19.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaSanPham"));
            textBox18.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "TenSanPham"));
            textBox17.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "ChatLieu"));
            textBox16.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "XuatXu"));
            textBox15.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SuMoTa"));
            textBox14.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "SoLuong"));
            textBox13.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "Gia"));
            textBox12.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "KhoiLuong"));
            textBox10.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "KichThuoc"));
            comboBox4.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaDanhMucCap3"));
            comboBox5.DataBindings.Add(new Binding("Text", dataGridView1.DataSource, "MaThuongHieu"));
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data = new DataSet();
                string query = "select * from DanhMucCap3";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                comboBox1.DataSource = data.Tables[0];
                comboBox1.DisplayMember = "MaDanhMucCap3";
                comboBox1.ValueMember = "MaDanhMucCap3";
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data = new DataSet();
                string query = "select * from ThuongHieu";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                comboBox2.DataSource = data.Tables[0];
                comboBox2.DisplayMember = "MaThuongHieu";
                comboBox2.ValueMember = "MaThuongHieu";
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data = new DataSet();
                string query = "select distinct(TinhTrang) from DonHang";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                comboBox3.DataSource = data.Tables[0];
                comboBox3.DisplayMember = "TinhTrang";
                connection.Close();
            }
            dataGridView2.DataSource = DonHang;
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataTable table = new DataTable();
                DataSet data = new DataSet();
                string query = "exec ThongKeDonHangHoanThanh_Moithang '"+manguoiban+"' , N'Đã giao',12,2019";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                connection.Close();
                DonHang.DataSource = data.Tables[0];
                table = data.Tables[1];
                foreach (DataRow row in table.Rows)
                {
                    textBox9.Text = row["Soluong"].ToString();                    
                }    
            }
            textBox20.DataBindings.Add(new Binding("Text", dataGridView2.DataSource, "MaDonHang"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                string masp = "";
                do
                {
                    masp = Path.GetRandomFileName();
                    masp = masp.Replace(".", "");
                }
                while (masp.Length > 11);
                connection.Open();
                try
                {
                    string query2 = "exec ThemSanPham @masp,@ten,@chatlieu,@xuatxu,@mota,9,@danhmuc,@manguoiban,@thuonghieu,@soluong,@gia,@khoiluong,@kichthuoc";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@masp", masp);
                    command2.Parameters.AddWithValue("@ten", textBox1.Text);
                    command2.Parameters.AddWithValue("@chatlieu", textBox2.Text);
                    command2.Parameters.AddWithValue("@xuatxu", textBox3.Text);
                    command2.Parameters.AddWithValue("@mota", textBox4.Text);
                    command2.Parameters.AddWithValue("@manguoiban", manguoiban);
                    command2.Parameters.AddWithValue("@danhmuc", comboBox1.Text);
                    command2.Parameters.AddWithValue("@thuonghieu", comboBox2.Text);
                    command2.Parameters.AddWithValue("@soluong", int.Parse(textBox5.Text));
                    command2.Parameters.AddWithValue("@gia", float.Parse(textBox6.Text));
                    command2.Parameters.AddWithValue("@khoiluong", float.Parse(textBox7.Text));
                    command2.Parameters.AddWithValue("@kichthuoc", float.Parse(textBox8.Text));
                    int result = command2.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("không Thành công", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                        DataSet data = new DataSet();
                        string query = "select * from SanPham where MaNguoiBan='" + manguoiban + "'";
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                        SanPham.DataSource = data.Tables[0];
                        SoLuong.Text = data.Tables[0].Rows.Count.ToString();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("vui lòng nhập đúng định dạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(thang.Text==""|| textBox11.Text=="")
                MessageBox.Show("vui lòng nhập đúng định dạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                    {
                        DataTable table = new DataTable();
                        DataSet data = new DataSet();
                        string query = "exec ThongKeDonHangHoanThanh_Moithang '" + manguoiban + "' , N'" + comboBox3.Text + "'," + thang.Text + "," + textBox11.Text;
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                        connection.Close();
                        DonHang.DataSource = data.Tables[0];
                        table = data.Tables[1];
                        foreach (DataRow row in table.Rows)
                        {
                            textBox9.Text = row["Soluong"].ToString();
                        }
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("vui lòng nhập đúng định dạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }    

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox19.Text == "")
            {
                MessageBox.Show("bạn chưa chọn sản phẩm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                    {
                        connection.Open();
                        string query2 = "delete from SanPham where MaSanPham = @masanpham";
                        SqlCommand command2 = new SqlCommand(query2, connection);
                        command2.Parameters.AddWithValue("@masanpham", textBox19.Text);
                        int result = command2.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("không Thành công", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                            DataSet data = new DataSet();
                            string query = "select * from SanPham where MaNguoiBan='" + manguoiban + "'";
                            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                            adapter.Fill(data);
                            SanPham.DataSource = data.Tables[0];
                            SoLuong.Text = data.Tables[0].Rows.Count.ToString();
                        }
                        connection.Close();
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Sản phẩm đã được đặt mua không thể xóa", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                string query = "select MaSanPham,SoLuong,DanhGia from CT_DonHang where MaDonHang='" + textBox20.Text + "'";
                connection.Open();
                DataSet data = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                dataGridView3.DataSource = data.Tables[0];
                connection.Close();
            }
        }
    }
}
