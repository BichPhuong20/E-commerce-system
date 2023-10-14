--Tình huống 1: người mua có mã 'DH_00242' làm phiếu trả hàng cho sản phẩm có mã sản phẩm là ''SP_16409' bị lỗi
--Hệ thống đề nghị chỉ mục Index 

create proc taoPhieuTraHang @manguoimua nchar(10),@maphieu nchar(10),@lydotra nvarchar(50),@masanpham nchar(10),@madonhang nchar(10)
as
begin
   	if(not exists (select MaNguoiMua from DonHang where @manguoimua=MaNguoiMua))
         	begin
                	print N'Đơn hàng không phải của bạn'
         	end
   	if(exists (select MaPhieuTraHang from PhieuTraHang where @maphieu=MaPhieuTraHang))
         	print N'mã phiếu trả hàng đã tồn tại'
	else if(not exists (select * from DonHang where TinhTrang like N'Đã giao' and MaDonHang=@madonhang))
			print N'Đơn Hàng không hợp lệ'
	else
			begin
			declare @soluong int = (select SoLuong from CT_DonHang where MaDonHang=@madonhang)
			declare @tienhoantra money= @soluong*(select Gia from SanPham  where MaSanPham=@masanpham)
         	insert into PhieuTraHang values (@maphieu,GETDATE(),@lydotra,@tienhoantra,@masanpham,@soluong,@madonhang,null)
			end
end
go
exec taoPhieuTraHang 'DH_00242','PTH_BTS',N'Hàng không tốt','SP_16409','DH_09884'
go
--Tạo index
create nonclustered index index_PTT on DonHang(MaNguoiMua)
exec taoPhieuTraHang 'DH_00242','PTH_BTS',N'Hàng không tốt','SP_16409','DH_09884'


--Tình huống 2: Người mua xóa một sản phẩm khỏi giỏ hàng, thì bên giỏ hàng tự động cập nhật lại số lượng sản phẩm và tổng tiền
 -- Hệ thống không đề nghị chỉ mục Index 
create proc xoaspkhoigiohang @magh nchar(10), @masp nchar(10)
as
begin
   	if not exists (select * from CT_GioHang where MaGioHang=@magh and MaSanPham=@masp)
         	print N'Sản phẩm không tồn tại trong giỏ hàng của bạn'
   	else
         	begin
                	delete from CT_GioHang
                	where MaGioHang=@magh and MaSanPham=@masp
                	print N'Đã xóa sản phẩm khỏi giỏ hàng của bạn'
                	-- cập nhật lại tổng số sản phẩm và tổng tiền của mặt hàng
                	declare @tien money = (select Gia from SanPham where MaSanPham = @masp) * (select SoLuong from CT_GioHang where MaGioHang=@magh and MaSanPham=@masp)
                	update GioHang
                	set SoSanPham=SoSanPham-1, TongTien = TongTien - @tien
                	where MaGioHang=@magh
         	end
end

exec xoaspkhoigiohang 'GH_00006','SP_00003'


 --Tình huống 3: Tìm kiếm theo tên và thêm sản phẩm vào giỏ hàng
  -- Hệ thống không đề nghị chỉ mục Index 
create proc TimKiemSanPham_ThemVaoGioHang @TenSanPham nvarchar(30),@MaGioHang nchar(10),@MaSanPham nchar(10),@SoLuong int
as
begin
--TÌm kiếm sản phẩm theo tên
 if(exists (select* from SanPham where TenSanPham like '%'+'@TenSanPham'+'%'))
	begin
		select* from SanPham where TenSanPham like '%'+'Var'+'%'
	end

 if(exists(select* from GioHang where MaGioHang=@MaGioHang ))
	begin
		--Kiểm tra sản phẩm đã có trong CT_GioHang chưa
		if(exists(select* from CT_GioHang where MaGioHang=@MaGioHang and MaSanPham=@MaSanPham))
			begin 
				--Nếu có rồi thì update số lượng sản phẩm
				update CT_GioHang set SoLuong=SoLuong+@SoLuong where MaGioHang=@MaGioHang and MaSanPham=@MaSanPham
			end
		else 
			begin
				--Kiểm tra sản phẩm đã có trong CT_GioHang chưa
				if(exists(select* from CT_GioHang where MaGioHang=@MaGioHang and MaSanPham != @MaSanPham ))
					begin 
					--Nếu chưa có sản phẩm trong CT_GioHang thì insert
						insert into CT_GioHang values(@MaGioHang,@MaSanPham,@SoLuong)
						update GioHang set SoSanPham=SoSanPham+1 where MaGioHang=@MaGioHang
					end 
			end 
	end
end
go
exec [dbo].[TimKiemSanPham_ThemVaoGioHang] 'Var','GH_00212','SP_02531',1
select* from CT_GioHang

exec [dbo].[TimKiemSanPham_ThemVaoGioHang] 'Var', 'GH_00212','SP_06',5
select* from CT_GioHang
select* from GioHang


--tình huống 4: S nhận xét về sản phẩm và đánh giá số sao, sau khi đánh giá thì S sẽ nhận được 200 xu vào Shopee Xu
 -- Hệ thống không đề nghị chỉ mục Index 
create proc danhgiasanpham @madonhang nchar(10), @masp nchar(10), @comment nvarchar(1000), @sosao tinyint
as
begin
	if exists(select MaSanPham from CT_DonHang c, DonHang d where d.MaDonHang=@madonhang and  d.TinhTrang=N'Đã giao' and SoSao is not null and c.MaDonHang= @madonhang and c.MaSanPham=@masp)
		print N'Bạn đã đánh giá sản phẩm'
	if exists(select MaSanPham from CT_DonHang c, DonHang d where d.MaDonHang=@madonhang and  d.TinhTrang=N'Đã giao' and SoSao is null and c.MaDonHang= @madonhang and c.MaSanPham=@masp)
	begin
	update CT_DonHang
	set DanhGia=@comment, SoSao=@sosao
	where MaDonHang=@madonhang and MaSanPham=@masp
	-- sau khi đánh giá thì được cộng 200 xu
	declare @manguoimua nchar(10) = (select MaNguoiMua from DonHang where MaDonHang=@madonhang)
	update NguoiMua
	set TongXuShopee=TongXuShopee+200
	where MaNguoiMua=@manguoimua
	print N'Cảm ơn bạn đã đánh giá. Bạn đã nhận được 200 xu'
	end
end

select * from CT_DonHang WHERE MaDonHang = 'DH_00002' AND MaSanPham = 'SP_00004'
exec danhgiasanpham 'DH_00002','SP_00004','Phuc tau hai', 4 


--Tình huống 5: P thống kê đơn hàng hoàn thành trong mỗi tháng 
-- Hệ thống đề nghị chỉ mục Index 
 create proc ThongKeDonHangHoanThanh_MoiThang @MaNguoiBan nchar(10), @TinhTrang nvarchar(20) ,@Thang int,@Nam int 
 as
 begin
	--Xem don hang trong moi hang
	select *  from DonHang as DH,CT_DonHang,SanPham as SP where DH.MaDonHang= CT_DonHang.MaDonHang and SP.MaSanPham=CT_DonHang.MaSanPham and 
	SP.MaNguoiBan=@MaNguoiBan and TinhTrang=@TinhTrang and month(ThoiGianHoanThanh)=@Thang and year(ThoiGianHoanThanh)=@Nam
	--Thong ke so luong 
	select count(*) as SoLuong from DonHang as DH,CT_DonHang,SanPham as SP where DH.MaDonHang= CT_DonHang.MaDonHang and SP.MaSanPham=CT_DonHang.MaSanPham and 
	SP.MaNguoiBan=@MaNguoiBan and TinhTrang=@TinhTrang and month(ThoiGianHoanThanh)=@Thang and year(ThoiGianHoanThanh)=@Nam
 end
 go
 exec ThongKeDonHangHoanThanh_Moithang DH_87256 , N'Đã giao',4,2019
 go

 -- Tạo index
 create nonclustered index index_ThongKeDonTheoThang on SanPham(MaNguoiBan)
 create nonclustered index index_CTDONHang on CT_DonHang(MaSanPham) include (MaDonHang,SoLuong,DanhGia,SoSao)

 exec ThongKeDonHangHoanThanh_Moithang DH_87256 , N'Đã giao',4,2019


 --Tình huống 6: người bán tạo sản phẩm cho shop
 -- Hệ thống không đề nghị chỉ mục Index 

create proc ThemSanPham @MaSanPham nchar(10),@TenSanPham nvarchar(30),@ChatLieu nvarchar(20),@XuatXu nvarchar(20),@SuMoTa nvarchar(50),@HangDatTruoc tinyint,
@MaDanhMucCap3 nchar(10),@MaNguoiBan nchar(10),@MaThuongHieu nchar(10),@Soluong int ,@Gia money,@KhoiLuong int ,@KichThuoc int
as
begin
	if(not exists(select* from SanPham where MaSanPham=@MaSanPham))
	begin 
		insert into SanPham values(@MaSanPham,@TenSanPham,@ChatLieu,@XuatXu,@SuMoTa,@HangDatTruoc,@MaDanhMucCap3,@MaNguoiBan,@MaThuongHieu,@Soluong,@Gia,@KhoiLuong,@KichThuoc)
	end

end
go

exec ThemSanPham 'SPA_111','Son','Dulux','VietNam','Son dau cung dep',9,'DMC2_00285','DH_00419','Brand_0012',20,444.00,6,5