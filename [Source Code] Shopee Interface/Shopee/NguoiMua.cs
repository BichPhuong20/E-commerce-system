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
    public partial class NguoiMua : Form
    {
        public string manguoimua;
        public string magiohang;
        public BindingSource SanPham = new BindingSource();
        public BindingSource giohang = new BindingSource();
        public BindingSource ThongKe = new BindingSource();
        public BindingSource SPgiohang = new BindingSource();
        public BindingSource TTCaNhan = new BindingSource();
        public BindingSource MaDH = new BindingSource();
        public BindingSource TraHang = new BindingSource();
        public NguoiMua(string makhachhang)
        {
            manguoimua = makhachhang;
            InitializeComponent();
            Load();
            
        }
        void Load()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                string temp = "";
                string query1 = "select MaGioHang from GioHang where GioHang.MaNguoiMua = '" + manguoimua + "'";
                connection.Open();
                SqlCommand command1 = new SqlCommand(query1, connection);
                temp = (string)command1.ExecuteScalar();
                magiohang = temp;
                connection.Close();
            }

            dataGridView1.DataSource = SanPham;
            DataSet data = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                string query = "select * from SanPham";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);
                connection.Close();
            }
            SanPham.DataSource = data.Tables[0];
            textBox5.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "TenSanPham"));
            MaSanPham.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "MaSanPham"));
            textBox9.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "Gia"));
            textBox10.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "XuatXu"));
            textBox11.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "ChatLieu"));
            textBox15.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "SuMoTa"));
            textBox14.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "HangDatTruoc"));
            textBox13.DataBindings.Add(new Binding("text", dataGridView1.DataSource, "SoLuong"));
            dataGridView2.DataSource = giohang;
            DataSet data2 = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                string query = "select ct.MaSanPham as ma,ct.SoLuong sl,sp.TenSanPham ten,sp.Gia as gia  from SanPham sp,CT_GioHang ct where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data2);
                connection.Close();
            }
            giohang.DataSource = data2.Tables[0];
            textBox17.DataBindings.Add(new Binding("text", dataGridView2.DataSource, "ma"));
            textBox21.DataBindings.Add(new Binding("text", dataGridView2.DataSource, "ten"));
            textBox20.DataBindings.Add(new Binding("text", dataGridView2.DataSource, "gia"));
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data3 = new DataSet();
                string query3 = "select sum(ct.SoLuong) tongsanpham, sum (sp.Gia*ct.SoLuong) tongtien  from CT_GioHang ct,SanPham sp where ct.MaGioHang='" + magiohang+"'";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query3, connection);
                adapter.Fill(data3);
                ThongKe.DataSource = data3.Tables[0];
                connection.Close();                
            }
            textBox12.DataBindings.Add(new Binding("text", ThongKe, "tongsanpham"));
            textBox16.DataBindings.Add(new Binding("text", ThongKe, "tongtien"));
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data3 = new DataSet();
                string query = "select * from KhachHang kh,NguoiMua nm  where  kh.MaKhachHang=nm.MaNguoiMua and kh.MaKhachHang = '" + manguoimua+"'";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data3);
                TTCaNhan.DataSource = data3.Tables[0];
                connection.Close();
            }
            textBox2.DataBindings.Add(new Binding("text", TTCaNhan, "TenKhachHang"));
            textBox4.DataBindings.Add(new Binding("text", TTCaNhan, "Email"));
            textBox3.DataBindings.Add(new Binding("text", TTCaNhan, "SDT"));
            textBox8.DataBindings.Add(new Binding("text", TTCaNhan, "GioiTinh"));
            textBox7.DataBindings.Add(new Binding("text", TTCaNhan, "NgaySinh"));
            textBox6.DataBindings.Add(new Binding("text", TTCaNhan, "DiaChi"));
            textBox1.DataBindings.Add(new Binding("text", TTCaNhan, "MaKhachHang"));
            textBox23.DataBindings.Add(new Binding("text", TTCaNhan, "TongXuShopee"));
            dataGridView3.DataSource = MaDH;
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                DataSet data55=new DataSet();
                string query = "select * from DonHang where MaNguoiMua='" + manguoimua + "'";
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data55);
                MaDH.DataSource =data55.Tables[0];
                connection.Close();
            }
            madonhang.DataBindings.Add(new Binding("text", dataGridView3.DataSource, "MaDonHang"));
            dataGridView4.DataSource = TraHang;
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                string query15 = "select MaSanPham,SoLuong from CT_DonHang where MaDonHang='" + madonhang.Text + "'";
                connection.Open();
                DataSet data15 = new DataSet();
                SqlDataAdapter adapter15 = new SqlDataAdapter(query15, connection);
                adapter15.Fill(data15);
                TraHang.DataSource = data15.Tables[0];
                connection.Close();
            }
            textBox22.DataBindings.Add(new Binding("text", dataGridView4.DataSource, "MaSanPham"));

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataSet data = new DataSet();
            float giamin=0, giamax=0;

            if (name.Text == "" && min.Text == "" && max.Text == "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham";
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                    connection.Close();
                }
            else if (name.Text == "" && min.Text != "" && max.Text != "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham where Gia <= " + max.Text + " and Gia >= " + min.Text;
                    connection.Open();
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("vui lòng nhập đúng kiểu dữ liệu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Close();
                }
            else if (name.Text == "" && min.Text != "" && max.Text == "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham where  Gia >= " + min.Text;
                    connection.Open();
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("vui lòng nhập đúng kiểu dữ liệu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Close();
                }
            else if (name.Text == "" && min.Text == "" && max.Text != "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham where Gia <= " + max.Text ;
                    connection.Open();
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("vui lòng nhập đúng kiểu dữ liệu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Close();
                }
            else if (name.Text != "" && min.Text == "" && max.Text == "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham where TenSanPham like '%" + name.Text + "%'";
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                    connection.Close();
                }
            else if (name.Text != "" && min.Text != "" && max.Text == "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham where TenSanPham like '%" + name.Text + "%' and Gia >= "+min.Text;
                    connection.Open();
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("vui lòng nhập đúng kiểu dữ liệu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Close();
                }
            else if (name.Text != "" && min.Text == "" && max.Text != "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from SanPham where TenSanPham like '%" + name.Text + "%' and Gia =< " + max.Text;
                    connection.Open();
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("vui lòng nhập đúng kiểu dữ liệu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Close();
                }
            else if (name.Text != "" && min.Text != "" && max.Text != "")
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * where TenSanPham like '%" + name.Text + "%' and Gia =< " + max.Text + " and Gia > " + min.Text;
                    connection.Open();
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("vui lòng nhập đúng kiểu dữ liệu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    connection.Close();
                }
            SanPham.DataSource = data.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(MaSanPham.Text=="")
            {
                MessageBox.Show("bạn chưa chọn sản phẩm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }  
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    connection.Open();
                    string query2 = "insert into CT_GioHang values (@magiohang,@masanpham,1)";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@magiohang",magiohang );
                    command2.Parameters.AddWithValue("@masanpham", MaSanPham.Text);
                    try
                    {
                        int result = command2.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("không Thành công", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                            DataSet data2 = new DataSet();
                            DataSet data3 = new DataSet();
                            string query = "select ct.MaSanPham as ma,ct.SoLuong sl,sp.TenSanPham ten,sp.Gia as gia  from SanPham sp,CT_GioHang ct where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                            string query3 = "select sum(ct.SoLuong) tongsanpham, sum (sp.Gia*ct.SoLuong) tongtien  from CT_GioHang ct,SanPham sp where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                            SqlDataAdapter adapter2 = new SqlDataAdapter(query, connection);
                            SqlDataAdapter adapter3 = new SqlDataAdapter(query3, connection);
                            adapter2.Fill(data2); ;
                            adapter3.Fill(data3); ;
                            giohang.DataSource = data2.Tables[0];
                            ThongKe.DataSource = data3.Tables[0];
                        }
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("Sản Phẩm đã có Trong Giỏ hàng của bạn", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    connection.Close();
                }
            }    
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox18.Text == "")
            {
                MessageBox.Show("bạn chưa chọn số", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (int.TryParse(textBox18.Text, out int p) == false)
                MessageBox.Show("số chọn phải là số nguyên", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(int.Parse(textBox18.Text)<0)
                MessageBox.Show("số chọn phải lớn hơn 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                    {
                        connection.Open();
                        string query2 = "update CT_GioHang set SoLuong = @soluong where MaGioHang=@magiohang and MaSanPham=@masanpham ";
                        SqlCommand command2 = new SqlCommand(query2, connection);
                        command2.Parameters.AddWithValue("@magiohang", magiohang);
                        command2.Parameters.AddWithValue("@masanpham", textBox17.Text);
                        command2.Parameters.AddWithValue("@soluong", textBox18.Text);
                        int result = command2.ExecuteNonQuery();
                        if (result < 0)
                            MessageBox.Show("không Thành công", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                            DataSet data2 = new DataSet();
                            DataSet data3 = new DataSet();
                            string query = "select ct.MaSanPham as ma,ct.SoLuong sl,sp.TenSanPham ten,sp.Gia as gia  from SanPham sp,CT_GioHang ct where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                            string query3 = "select sum(ct.SoLuong) tongsanpham, sum (sp.Gia*ct.SoLuong) tongtien  from CT_GioHang ct,SanPham sp where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                            SqlDataAdapter adapter2 = new SqlDataAdapter(query, connection);
                            SqlDataAdapter adapter3 = new SqlDataAdapter(query3, connection);
                            adapter2.Fill(data2); ;
                            adapter3.Fill(data3); ;
                            giohang.DataSource = data2.Tables[0];
                            ThongKe.DataSource = data3.Tables[0];
                        }
                        connection.Close();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("vui lòng nhập đúng định dạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                connection.Open();
                string query = "update KhachHang set TenKhachHang=@ten, Email=@email, SDT=@sdt,GioiTinh=@gioitinh, NgaySinh=@ngaysinh ,DiaChi=@diachi where MaKhachHang=@ma";
                SqlCommand command2 = new SqlCommand(query, connection);
                command2.Parameters.AddWithValue("@ten", textBox2.Text);
                command2.Parameters.AddWithValue("@email", textBox4.Text);
                command2.Parameters.AddWithValue("@sdt", textBox3.Text);
                command2.Parameters.AddWithValue("@gioitinh", textBox8.Text);
                command2.Parameters.AddWithValue("@ngaysinh", textBox7.Text);
                command2.Parameters.AddWithValue("@diachi", textBox6.Text);
                command2.Parameters.AddWithValue("@ma", textBox1.Text);
                try
                {
                    int result = command2.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("không Thành công", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                        DataSet data2 = new DataSet();
                        DataSet data3 = new DataSet();
                        string query2 = "select ct.MaSanPham as ma,ct.SoLuong sl,sp.TenSanPham ten,sp.Gia as gia  from SanPham sp,CT_GioHang ct where ct.MaSanPham=sp.MaSanPham";
                        string query3 = "select sum(ct.SoLuong) tongsanpham, sum (sp.Gia*ct.SoLuong) tongtien  from CT_GioHang ct,SanPham sp where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                        SqlDataAdapter adapter2 = new SqlDataAdapter(query2, connection);
                        SqlDataAdapter adapter3 = new SqlDataAdapter(query3, connection);
                        adapter2.Fill(data2); ;
                        adapter3.Fill(data3); ;
                        giohang.DataSource = data2.Tables[0];
                        ThongKe.DataSource = data3.Tables[0];
                        DataSet data4 = new DataSet();
                        string query4 = "select * from KhachHang kh,NguoiMua nm  where  kh.MaKhachHang=nm.MaNguoiMua and kh.MaKhachHang = '" + manguoimua + "'";
                        SqlDataAdapter adapter4 = new SqlDataAdapter(query4, connection);
                        adapter4.Fill(data4);
                        TTCaNhan.DataSource = data4.Tables[0];
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("vui lòng nhập đúng định dạng", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        DataSet data5 = new DataSet();
                        string query5 = "select * from KhachHang kh,NguoiMua nm  where  kh.MaKhachHang=nm.MaNguoiMua and kh.MaKhachHang = '" + manguoimua + "'";
                    SqlDataAdapter adapter5 = new SqlDataAdapter(query5, connection);
                        adapter5.Fill(data5);
                        TTCaNhan.DataSource = data5.Tables[0];
                }
                connection.Close();
            }
        }

        private void xoa_Click(object sender, EventArgs e)
        {
            if (MaSanPham.Text == "")
            {
                MessageBox.Show("bạn chưa chọn sản phẩm", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    connection.Open();
                    string query2 = "delete from CT_GioHang where Magiohang = @magiohang and MaSanPham = @masanpham";
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    command2.Parameters.AddWithValue("@magiohang", magiohang);
                    command2.Parameters.AddWithValue("@masanpham", textBox17.Text);
                    int result = command2.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("không Thành công", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                        DataSet data2 = new DataSet();
                        DataSet data3 = new DataSet();
                        string query = "select ct.MaSanPham as ma,ct.SoLuong sl,sp.TenSanPham ten,sp.Gia as gia  from SanPham sp,CT_GioHang ct where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                        string query3 = "select sum(ct.SoLuong) tongsanpham, sum (sp.Gia*ct.SoLuong) tongtien  from CT_GioHang ct,SanPham sp where ct.MaSanPham=sp.MaSanPham and ct.MaGioHang='" + magiohang + "'";
                        SqlDataAdapter adapter2 = new SqlDataAdapter(query, connection);
                        SqlDataAdapter adapter3 = new SqlDataAdapter(query3, connection);
                        adapter2.Fill(data2); ;
                        adapter3.Fill(data3); ;
                        giohang.DataSource = data2.Tables[0];
                        ThongKe.DataSource = data3.Tables[0];
                    }
                    connection.Close();
                }
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            if(All.Checked==true)
            {
                DataSet data = new DataSet();
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select * from DonHang where MaNguoiMua='"+manguoimua+"'";
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                    connection.Close();
                }
                MaDH.DataSource = data.Tables[0];
            }
            else
            {
                string tinhtrang="";
                if (Choxn.Checked == true)
                {
                    DataSet data = new DataSet();
                    using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                    {
                        string query = "select * from DonHang where MaNguoiMua='" + manguoimua + "' and  TinhTrang =  N'Chờ xác nhận'";
                        connection.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                        adapter.Fill(data);
                        connection.Close();
                    }
                    MaDH.DataSource = data.Tables[0];
                }    
                if (Cholh.Checked == true)
                {
                        DataSet data = new DataSet();
                        using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                        {
                            string query = "select * from DonHang where MaNguoiMua='" + manguoimua + "' and  TinhTrang =  N'Chờ lấy hàng'";
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                            adapter.Fill(data);
                            connection.Close();
                        }
                        MaDH.DataSource = data.Tables[0];
                }    
                if (DangGiao.Checked == true)
                {
                        DataSet data = new DataSet();
                        using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                        {
                            string query = "select * from DonHang where MaNguoiMua='" + manguoimua + "' and  TinhTrang =  N'Đang giao'";
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                            adapter.Fill(data);
                            connection.Close();
                        }
                        MaDH.DataSource = data.Tables[0];
                }    
                if (DaGiao.Checked == true)
                {
                        DataSet data = new DataSet();
                        using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                        {
                            string query = "select * from DonHang where MaNguoiMua='" + manguoimua + "' and  TinhTrang like N'Đã giao'";
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                            adapter.Fill(data);
                            connection.Close();
                        }
                        MaDH.DataSource = data.Tables[0];
                }    
                if (DaHuy.Checked == true)
                {
                        DataSet data = new DataSet();
                        using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                        {
                            string query = "select * from DonHang where MaNguoiMua='" + manguoimua + "' and  TinhTrang like  N'Đã hủy'";
                            connection.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                            adapter.Fill(data);
                            connection.Close();
                        }
                        MaDH.DataSource = data.Tables[0];
                }    
            }

        }

        private void madonhang_TextChanged(object sender, EventArgs e)
        {
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    string query = "select MaSanPham,SoLuong from CT_DonHang where MaDonHang='" + madonhang.Text + "'";
                    connection.Open();
                    DataSet data = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(data);
                    TraHang.DataSource = data.Tables[0];
                    connection.Close();
                }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(textBox19.Text!=null)
                MessageBox.Show("bạn phải viết lý do", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
            {
                connection.Open();
                string maphieunhap = "";
                do
                {
                    maphieunhap = Path.GetRandomFileName();
                    maphieunhap = maphieunhap.Replace(".", "");
                }
                while (maphieunhap.Length > 11);
                string query2 = "exec taoPhieuTraHang '"+manguoimua+"','"+maphieunhap+"',N'"+textBox19.Text+"','"+textBox22.Text+"','"+madonhang.Text+"'";
                SqlCommand command2 = new SqlCommand(query2, connection);              
                    int result = command2.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("đơn hàng chọn phải đã giao", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                    }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox25.Text == "" || textBox24.Text == "")
                MessageBox.Show("bạn chưa điền thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString.connectionstring))
                {
                    connection.Open();
                    string query2 = "exec danhgiasanpham '" + madonhang.Text + "','" + textBox22.Text + "','" + textBox24.Text + "'," + textBox25.Text;
                    SqlCommand command2 = new SqlCommand(query2, connection);
                    int result = command2.ExecuteNonQuery();
                    if (result < 0)
                        MessageBox.Show("đơn hàng chọn phải đã giao hoặc chưa đánh giá", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        MessageBox.Show("Thành công", "Success", MessageBoxButtons.OK);
                    }
                }
            }
        }
    }
}
