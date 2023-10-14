CREATE DATABASE ShopeeTest
go
use ShopeeTest
go
CREATE TABLE KhachHang
(
	MaKhachHang nchar(10),
	TenKhachHang nvarchar(30) NOT NULL,
	Email nchar(30) NOT NULL,
	SDT char(10) NOT NULL,
	GioiTinh nvarchar(3) check (GioiTinh in (N'Nam',N'Nữ')) NOT NULL,
	NgaySinh date NOT NULL,
	DiaChi nvarchar(50) NOT NULL,
	MaTaiKhoan nchar(10) NOT NULL,
	CONSTRAINT PK_KhachHang PRIMARY KEY(MaKhachHang)
)

CREATE TABLE NguoiMua
(
	MaNguoiMua nchar(10),
	TongXuShopee int NOT NULL,
	CONSTRAINT PK_NguoiMua PRIMARY KEY(MaNguoiMua)
)
CREATE TABLE NguoiBan
(
	MaNguoiBan nchar(10),
	TenShop nvarchar(30) NOT NULL,
	SuMoTa nvarchar(50) NOT NULL,
	SoSao float,
	SoNguoiTheoDoi int,
	CONSTRAINT PK_NguoiBan PRIMARY KEY(MaNguoiBan)
)
------------------ThanhToan--
CREATE TABLE TaiKhoanThanhToan
(
	MaTaiKhoanThanhToan nchar(10),
	MaPin char(6) NOT NULL,
	SoDu money NOT NULL,
	CONSTRAINT PK_TaiKhoanThanhToan PRIMARY KEY(MaTaiKhoanThanhToan)
)

CREATE TABLE ViShopee
(
	MaViShopee nchar(10),
	MaKhachHang nchar(10) NOT NULL,
	CONSTRAINT PK_ViShopee PRIMARY KEY(MaViShopee)
)
CREATE TABLE ViAirPay
(
	MaViAirPay nchar(10),
	MaNguoiMua nchar(10) NOT NULL,
	CONSTRAINT PK_ViAirPay PRIMARY KEY(MaViAirPay)
)
CREATE TABLE TaiKhoanNganHang
(
	MaTaiKhoan nchar(10),
	MaKhachHang nchar(10) NOT NULL,
	CONSTRAINT PK_TaiKhoanNganHang PRIMARY KEY(MaTaiKhoan)
)
CREATE TABLE LichSuGiaoDich
(
	MaGiaoDich nchar(10),
	SoTienGiaoDich money NOT NULL,
	ThoiGianGiaoDich datetime NOT NULL,
	LoaiGiaoDich tinyint check (LoaiGiaoDich in (1,2,3,4,5)) NOT NULL,--1: thanh toán, 2: rút tiền, 3: hoàn tiền, 4: nhận tiền hoàn,doanh thu
	MaTaiKhoanThanhToan nchar(10) NOT NULL,
	MaDonHang nchar(10),
	CONSTRAINT PK_LichSuGiaoDich PRIMARY KEY(MaGiaoDich)
)
----------------------------------------------------------

----------------------
-- ---------------SanPham_GioHang_DonHang---------------
CREATE TABLE BangTheoDoi
(
	MaNguoiMua nchar(10),
	MaNguoiBan nchar(10),
	CONSTRAINT PK_BangTheoDoi PRIMARY KEY(MaNguoiMua,MaNguoiBan)
)

CREATE TABLE DanhMucCap1
(
	MaDanhMucCap1 nchar(10),
	TenDanhMucCap1 nvarchar(30) NOT NULL,
	CONSTRAINT PK_DanhMucCap1 PRIMARY KEY(MaDanhMucCap1)
)
CREATE TABLE DanhMucCap2
(
	MaDanhMucCap2 nchar(10),
	TenDanhMucCap2 nvarchar(30) NOT NULL,
	MaDanhMucCap1 nchar(10) NOT NULL,
	CONSTRAINT PK_DanhMucCap2 PRIMARY KEY(MaDanhMucCap2)
)
CREATE TABLE DanhMucCap3
(
	MaDanhMucCap3 nchar(10),
	TenDanhMucCap3 nvarchar(30) NOT NULL,
	MaDanhMucCap2 nchar(10) NOT NULL,
	CONSTRAINT PK_DanhMucCap3 PRIMARY KEY(MaDanhMucCap3)
)
CREATE TABLE ThuongHieu
(
	MaThuongHieu nchar(10),
	TenThuongHieu nvarchar(30) NOT NULL,
	CONSTRAINT PK_ThuongHieu PRIMARY KEY(MaThuongHieu)
)
CREATE TABLE SanPham 
(
	MaSanPham nchar(10),
	TenSanPham nvarchar(30) NOT NULL,
	ChatLieu nvarchar(20) NOT NULL,
	XuatXu nvarchar(20) NOT NULL,
	SuMoTa nvarchar(50) NOT NULL,
	HangDatTruoc tinyint check(HangDatTruoc in (0, 7,8,9,10,11,12,13,14,15)) NOT NULL,
	MaDanhMucCap3 nchar(10) NOT NULL,
	MaNguoiBan nchar(10) NOT NULL,
	MaThuongHieu nchar(10),
	SoLuong int NOT NULL,
	Gia money NOT NULL,
	KhoiLuong int NOT NULL,
	KichThuoc int NOT NULL,
	CONSTRAINT PK_SanPham PRIMARY KEY(MaSanPham)
)




CREATE TABLE GioHang
(
	MaGioHang nchar(10),
	SoSanPham int,
	TongTien money,
	MaNguoiMua nchar(10) NOT NULL,
	CONSTRAINT PK_GioHang PRIMARY KEY(MaGioHang)
)

CREATE TABLE CT_GioHang
(
	MaGioHang nchar(10),
	MaSanPham nchar(10),
	SoLuong int NOT NULL,
	CONSTRAINT PK_CTGioHang PRIMARY KEY(MaGioHang,MaSanPham)
)

CREATE TABLE PhieuTraHang
(
	MaPhieuTraHang nchar(10),
	NgayLap datetime NOT NULL ,
	LyDoTraHang nvarchar(50) NOT NULL,
	TienHoanTra money NOT NULL,
	MaSanPham nchar(10) NOT NULL,
	SoLuong int NOT NULL,
	MaDonHang nchar(10) NOT NULL,
	MaGiaoDich nchar(10)
	CONSTRAINT PK_PhieuTraHang PRIMARY KEY(MaPhieuTraHang)
)

CREATE TABLE DonHang
(
	MaDonHang nchar(10),
	MaNguoiMua nchar(10) NOT NULL,
	MaVanDon nchar(10) ,
	TinhTrang nvarchar(20) check (TinhTrang in (N'Chờ xác nhận',N'Chờ lấy hàng',N'Đang giao',N'Đã giao',N'Đã hủy')),
	PhiVanChuyen money NOT NULL,
	TongTien money NOT NULL,
	TienGiamGia money,
	SoXuDuocDung int,
	SoXuNhanDuoc int,
	MaDVVC nchar(10) NOT NULL,
	MaDiaChi nchar(10) NOT NULL,
	HinhThucThanhToan int check (HinhThucThanhToan in (1,2,3)) NOT NULL,
	MaDonNhap nchar(10),
	MaDonXuat nchar(10),
	ThoiGianDatHang datetime NOT NULL,
	ThoiGianGiaoHangChoDVVC datetime,
	ThoiGianHoanThanh datetime,
	CONSTRAINT PK_DonHang PRIMARY KEY(MaDonHang)
)
CREATE TABLE CT_DonHang
(
	MaDonHang nchar(10),
	MaSanPham nchar(10),
	SoLuong int NOT NULL,
	DanhGia nvarchar(1000), 
	SoSao tinyint check (SoSao in (1,2,3,4,5)), -- số sao đánh giá
	CONSTRAINT PK_CT_DonHang PRIMARY KEY(MaDonHang,MaSanPham)
)
CREATE TABLE DiaChiNhanHang
(
	MaDiaChi nchar(10),
	Ten nvarchar(30) NOT NULL,
	SDT char(10) NOT NULL,
	Tinh_TP nvarchar(15) NOT NULL,
	Quan_Huyen nvarchar(15) NOT NULL,
	Phuong_Xa nvarchar(15) NOT NULL,
	DiaChiCuThe nvarchar(50) NOT NULL,
	MacDinh bit NOT NULL, -- 1: là địa chỉ nhận hàng mặc định, 0 là không phải
	MaNguoiMua nchar(10) NOT NULL,
	CONSTRAINT PK_DiaChiNhanHang PRIMARY KEY(MaDiaChi)
)
------------------------------------------------------------------------
------------- NhapXuatKho - NhanVien
CREATE TABLE PhieuXuat
(
	MaPhieuXuat nchar(10),
	NgayLap datetime NOT NULL,
	SoLuongDonXuat int NOT NULL,
	MaKho nchar(10) NOT NULL,
	MaNhanVien nchar(10) NOT NULL
	CONSTRAINT PK_PhieuXuat PRIMARY KEY(MaPhieuXuat)
)

CREATE TABLE PhieuNhap
(
	MaPhieuNhap nchar(10),
	NgayLap datetime NOT NULL,
	SoLuongDonNhap int NOT NULL,
	MaKho nchar(10) NOT NULL,
	MaNhanVien nchar(10) NOT NULL
	CONSTRAINT PK_PhieuNhap PRIMARY KEY(MaPhieuNhap)
)

CREATE TABLE Kho
(
	MaKho nchar(10),
	TenKho nvarchar(30)NOT NULL,
	DiaChi nvarchar(50) NOT NULL,
	SDT char(10) NOT NULL
	CONSTRAINT PK_Kho PRIMARY KEY(MaKho)
)

CREATE TABLE NhanVien 
(
	MaNhanVien nchar(10),
	TenNhanVien nvarchar(30) NOT NULL,
	SDT char(10) NOT NULL,
	NgaySinh date NOT NULL,
	GioiTinh nvarchar(3) check (GioiTinh IN ('Nam',N'Nữ')) NOT NULL,
	MaKho nchar(10) NOT NULL,
	MaTaiKhoan nchar(10) NOT NULL,
	CONSTRAINT PK_NhanVien PRIMARY KEY(MaNhanVien)
)
-----------------------------------------------------------------------

----------- VOUCHER - DONVIVANCHUYEN


CREATE TABLE Voucher
(
	MaVoucher nchar(10),
	UuDai float,
	DonToiThieu money,
	NgayBatDau datetime NOT NULL,
	NgayKetThuc datetime NOT NULL,
	GiamToiDa money,
	SoLuong int NOT NULL,
	Tien_Xu bit, -- 1 là tiền, 0 là xu
	MaNguoiBan nchar(10) NOT NULL,
	LoaiVoucher nvarchar(30) check (LoaiVoucher in (N'Miễn phí vận chuyển',N'Ngành hàng', N'Shopee', N'Người bán')) NOT NULL,
	CONSTRAINT PK_Voucher PRIMARY KEY(MaVoucher)
)

CREATE TABLE ViVoucher
(
	MaVoucher nchar(10),
	MaNguoiMua nchar(10),
	SoLuong int NOT NULL,
	CONSTRAINT PK_ViVoucher PRIMARY KEY(MaVoucher,MaNguoiMua)
)


CREATE TABLE DonViVanChuyen
(
	MaDVVC nchar(10),
	TenDVVC nvarchar(30) NOT NULL,
	CONSTRAINT PK_DonViVanChuyen PRIMARY KEY(MaDVVC)
)

CREATE TABLE DVVC_Voucher
(
	MaDVVC nchar(10),
	MaVoucher nchar(10),
	CONSTRAINT PK_DVVC_Voucher PRIMARY KEY(MaDVVC,MaVoucher)
)


CREATE TABLE Voucher_DonHang
(
	MaDonHang nchar(10),
	MaVoucher nchar(10),
	SoTienXuGiam money NOT NULL
	CONSTRAINT PK_Voucher_DonHang PRIMARY KEY(MaDonHang,MaVoucher)
)

CREATE TABLE TaiKhoan
(
MaTaiKhoan nchar(10),
TenTaiKhoan  nchar(10) NOT NULL,
MatKhau nchar(20) NOT NULL,
LoaiTaiKhoan tinyint check (LoaiTaiKhoan in (1,2,3)) NOT NULL
CONSTRAINT PK_TaiKhoan PRIMARY KEY(MaTaiKhoan)

)


------------------------------------------------------------------------
-- KHOA NGOAI
----BangTheoDoi
alter table BangTheoDoi
add constraint FK_BangTheoDoi_NguoiMua foreign key(MaNguoiMua) references NguoiMua(MaNguoiMua)

alter table BangTheoDoi
add constraint FK_BangTheoDoi_NguoiBan foreign key(MaNguoiBan) references NguoiBan(MaNguoiBan)


--CT_DonHang
alter table CT_DonHang
add constraint FK_CTDH_DH foreign key(MaDonHang) references DonHang(MaDonHang)

alter table CT_DonHang
add constraint FK_CTDH_MH foreign key(MaSanPham) references SanPham(MaSanPham)

-- CT_GioHang
alter table CT_GioHang
add constraint FK_CTGH_GH foreign key(MaGioHang) references GioHang(MaGioHang)

alter table CT_GioHang
add constraint FK_CTGH_MH foreign key(MaSanPham) references SanPham(MaSanPham)



--DanhMucCap2
alter table DanhMucCap2
add constraint FK_DMC2_DMC1 foreign key(MaDanhMucCap1) references DanhMucCap1(MaDanhMucCap1)

--DanhMucCap3
alter table DanhMucCap3
add constraint FK_DMC3_DMC2 foreign key(MaDanhMucCap2) references DanhMucCap2(MaDanhMucCap2)

--DiaChiNhanHang
alter table DiaChiNhanHang
add constraint FK_DCNH_NguoiMua foreign key(MaNguoiMua) references NguoiMua(MaNguoiMua)

--DonHang
alter table DonHang 
add constraint FK_DH_NM foreign key(MaNguoiMua) references NguoiMua(MaNguoiMua)

alter table DonHang
add constraint FK_DH_DVVC foreign key(MaDVVC) references DonViVanChuyen(MaDVVC)

alter table DonHang
add constraint FK_DH_DC foreign key(MaDiaChi) references DiaChiNhanHang(MaDiaChi)

alter table DonHang
add constraint FK_DH_PN foreign key(MaDonNhap) references PhieuNhap(MaPhieuNhap)

alter table DonHang
add constraint FK_DH_PX foreign key(MaDonXuat) references PhieuXuat(MaPhieuXuat)

--DVVC_Voucher
alter table DVVC_Voucher
add constraint FK_Voucher_DVVC foreign key(MaDVVC) references DonViVanChuyen(MaDVVC)

alter table DVVC_Voucher
add constraint FK_Voucher foreign key(MaVoucher) references Voucher(MaVoucher)

--GioHang
alter table GioHang
add constraint FK_ foreign key(MaNguoiMua) references NguoiMua(MaNguoiMua)

--KhachHang
alter table KhachHang
add constraint FK_KH_TK foreign key(MaTaiKhoan) references TaiKhoan(MaTaiKhoan)

--LichSuGiaoDich
alter table LichSuGiaoDich
add constraint FK_LSGD_TKTT foreign key(MaTaiKhoanThanhToan) references TaiKhoanThanhToan(MaTaiKhoanThanhToan)

alter table LichSuGiaoDich
add constraint FK_LSGD_DonHang foreign key(MaDonHang) references DonHang(MaDonHang)


--NguoiBan
alter table NguoiBan
add constraint FK_NguoiBan_KhachHang foreign key(MaNguoiBan) references KhachHang(MaKhachHang)

--NguoiMua
alter table NguoiMua
add constraint FK_NguoiMua_KhachHang foreign key(MaNguoiMua) references KhachHang(MaKhachHang)

--NhanVien  
alter table NhanVien
add constraint FK_Nhanvien_Kho foreign key(MaKho) references Kho(Makho)

alter table NhanVien
add constraint FK_Nhanvien_TaiKhoan foreign key(MaTaiKhoan) references TaiKhoan(MaTaiKhoan)

--PhieuNhap
alter table PhieuNhap
add constraint FK_PhieuNhap_kho foreign key(Makho) references Kho(MaKho)

alter table PhieuNhap
add constraint FK_PhieuNhap_NhanVien foreign key(MaNhanVien) references NhanVien(MaNhanVien)

--PhieuTraHang
alter table PhieuTraHang
add constraint FK_PhieuTraHang_SanPham foreign key(MaSanPham) references SanPham(MaSanPham)

alter table PhieuTraHang
add constraint FK_PhieuTraHang_DonHang foreign key(MaDonHang) references DonHang(MaDonHang)

alter table PhieuTraHang
add constraint FK_PhieuTraHang_GiaoDich foreign key(MaGiaoDich) references LichSuGiaoDich(MaGiaoDich)

----PhieuXuat
alter table PhieuXuat
add constraint FK_PhieuXuat_kho foreign key(Makho) references Kho(MaKho)

alter table PhieuXuat
add constraint FK_PhieuXuat_NhanVien foreign key(MaNhanVien) references NhanVien(MaNhanVien)

--SanPham
alter table SanPham
add constraint FK_SanPham_NguoiBan foreign key(MaNguoiBan) references NguoiBan(MaNguoiBan)

alter table SanPham
add constraint FK_SanPham_DMC3 foreign key(MaDanhMucCap3) references DanhMucCap3(MaDanhMucCap3)

alter table SanPham
add constraint FK_SanPham_ThuongHieu foreign key(MaThuongHieu) references ThuongHieu(MaThuongHieu)

--TaiKhoanNganHang
alter table TaiKhoanNganHang
add constraint FK_TKNH_TKTT foreign key(MaTaiKhoan) references TaiKhoanThanhToan(MaTaiKhoanThanhToan)

alter table TaiKhoanNganHang
add constraint FK_TKNH_KH foreign key(MaKhachHang) references KhachHang(MaKhachHang)


--ViAirPay
alter table ViAirPay
add constraint FK_ViAirPay_NguoiMua foreign key(MaNguoiMua) references NguoiMua(MaNguoiMua)

alter table ViAirPay
add constraint FK_ViAirPay_TKTT foreign key(MaViAirPay) references TaiKhoanThanhToan(MaTaiKhoanThanhToan)

--ViShopee
alter table ViShopee
add constraint FK_ViShopee_KH foreign key(MaKhachHang) references KhachHang(MaKhachHang)

alter table ViShopee
add constraint FK_ViShopee_TKTT foreign key(MaViShopee) references TaiKhoanThanhToan(MaTaiKhoanThanhToan)


--ViVoucher
alter table ViVoucher
add constraint FK_ViVoucher_Voucher foreign key(MaVoucher) references Voucher(MaVoucher)

alter table ViVoucher
add constraint FK_ViVoucher_NguoiMua foreign key(MaNguoiMua) references NguoiMua(MaNguoiMua)

--Voucher
alter table Voucher
add constraint FK_Voucher_NguoiBan foreign key(MaNguoiBan) references NguoiBan(MaNguoiBan)


--Voucher_DonHang
alter table Voucher_DonHang
add constraint FK_Voucher_DonHang foreign key(MaDonHang) references DonHang(MaDonHang)

alter table Voucher_DonHang
add constraint FK_VoucherSuDung foreign key(MaVoucher) references Voucher(MaVoucher)


---------------------------------------------------------------------------------------------
--TRIGGER--
--khi một đơn hàng được tạo thì phải cập nhật lại số lượng các sản phẩm của đơn hàng đó
create trigger trigger_CTDonHang_SanPham
on CT_DonHang
after insert
as
begin 
    declare @sl int= (select SoLuong from inserted)
	declare @mh nchar(10)= (select MaSanPham from inserted)
	update SanPham
	set SanPham.SoLuong = SanPham.SoLuong - @sl
	where SanPham. MaSanPham= @mh
end
 

go
-- khi số lượng sản phẩm là 0 thì không thể thêm sản phẩm đó vào giỏ hàng
create trigger trigger_SanPham_GioHang
on CT_GioHang 
for insert
as
begin
	if exists (select * from inserted i, SanPham m where i.MaSanPham= m.MaSanPham and m.SoLuong=0)
	begin
		raiserror(N'Lỗi: Sản phẩm này đã hết!!!',16,1)
		rollback
	end
end
go

-- khi số lượng sản phẩm là 0 thì không thể mua hàng
create trigger trigger_muahang
on CT_DonHang
for insert
as
begin
	if exists (select * from inserted i, SanPham s where i.MaSanPham= s.MaSanPham and s.SoLuong=0)
	begin
		raiserror(N'Lỗi: Sản phẩm này đã hết!!!',16,1)
		rollback
	end
end
go
-- khi thêm xóa, sửa một sản phẩm vào giỏ hàng thì cập nhật lại số sản phẩm và tổng số tiền của giỏ hàng
create trigger trigger_giohang
on CT_GioHang
for insert,update, delete
as
begin
	if not exists(select * from inserted) -- delete
		begin
			declare @gh nchar(10)= (select MaGioHang from deleted)
			declare @mh nchar(10)= (select MaSanPham from deleted)
			declare @tien money = (select gia from SanPham where MaSanPham=@mh)* (select SoLuong from deleted)
			update GioHang
			set SoSanPham= SoSanPham-1, TongTien=TongTien- @tien
			where MaGioHang = @gh
		end
	else 
	begin
		if not exists(select * from deleted) -- insert
		begin
			declare @mgh nchar(10)= (select MaGioHang from inserted)
			declare @mmh nchar(10)= (select MaSanPham from inserted)
			declare @sl int = (select SoLuong from inserted)
			declare @t money = (select gia from SanPham where MaSanPham=@mmh)*@sl
			update GioHang
			set SoSanPham= SoSanPham+1, TongTien=TongTien+ @t
			where MaGioHang = @mgh					
		end
		else  -- update
		begin
			if update(SoLuong)
			begin 
				declare @a nchar(10)= (select MaGioHang from inserted)
			declare @b nchar(10)= (select MaSanPham from inserted)
			declare @slc int = (select SoLuong from deleted)
			declare @slm int = (select SoLuong from inserted)
			declare @d money = (select gia from SanPham where MaSanPham=@b)

			update GioHang
				set TongTien=TongTien- @d*@slc + @d*@slm
				where MaGioHang = @a
			-- nếu số lượng sau cập nhật là 0 thì xóa sản phẩm đó khỏi giỏ hàng
			if (@slm=0)
			begin
				update GioHang
				set SoSanPham= SoSanPham-1
				where MaGioHang = @a
				delete from CT_GioHang where MaSanPham=@b
			end
			end
		end
	end
end

go
-- sau khi đặt mua hàng xong thì các sản phẩm trong đơn hàng đó tự động xóa khỏi giỏ hàng
create trigger trigger_muahang_giohang
on CT_DonHang
after insert
as 
begin
	declare @mh nchar(10) = (select MaSanPham from inserted)
	delete from CT_GioHang where MaSanPham =@mh
end

go



-- VIẾT HÀM SỬ DỤNG VOUCHER, CÓ KIỂM TRA HỢP LỆ HAY K
-- sau khi sử dụng voucher thì cập nhật số lượng của voucher đó, nếu số lượng =0 thì xóa voucher đó khỏi ví voucher
create trigger trigger_sudungvoucher
on Voucher_DonHang
after insert
as
begin
	declare @mdh nchar(10)= (select MaDonHang from inserted)
	declare @mvc nchar(10)= (select MaVoucher from inserted)
	update ViVoucher
	set SoLuong=SoLuong-1
	where MaVoucher=@mvc
	if( (select SoLuong from ViVoucher v, DonHang d where v.MaNguoiMua=d.MaNguoiMua and d.MaDonHang= @mdh) =0)
	delete from ViVoucher where MaVoucher=@mvc
end

--khi phát sinh giao dịch doanh thu thì ViShopee của Người bán cập nhật
go
create trigger trigger_doanhthu
on LichSuGiaoDich
after insert
as
begin
	if exists (select * from inserted where LoaiGiaoDich=5)-- doanh thu
	begin
		declare @mdh nchar(10) = (select MaDonHang from inserted)
		declare @sotien money = (select SoTienGiaoDich from inserted)
		declare @manb nchar(10) = (select MaNguoiBan from SanPham s, CT_DonHang c where  s.MaSanPham= c.MaSanPham and c.MaDonHang=@mdh)
		update TaiKhoanThanhToan
		set SoDu = SoDu + @sotien
		where MaTaiKhoanThanhToan = (select MaViShopee from ViShopee where MaKhachHang=@manb)
	end
end

go
-- DonHang
-- thời gian đặt hàng < thời gian giao hàng cho dvvc<thời gian hoàn thành
create trigger trigger_thoigiandonhang
on DonHang
for insert, update
as
begin
	if exists (select * from inserted where ThoiGianDatHang > ThoiGianGiaoHangChoDVVC and ThoiGianGiaoHangChoDVVC> ThoiGianHoanThanh)
	begin 
		raiserror(N'Lỗi: Các mốc thời gian của đơn hàng không hợp lệ!!!',16,1)
		rollback
	end
end

go
--khi một người nhấn theo dõi 1 người bán thì số người theo dõi tăng lên 1
--khi người mua nhấn hủy theo dõi thì số người theo dõi giảm 1
create trigger trigger_theodoi
on BangTheoDoi
after insert, delete
as
begin
     declare @shop nchar(10) = (select MaNguoiBan from inserted)
	 update NguoiBan set SoNguoiTheoDoi=SoNguoiTheoDoi+1 where MaNguoiBan=@shop
	 declare @shop1 nchar(10) = (select MaNguoiBan from deleted)
	  update NguoiBan set SoNguoiTheoDoi=SoNguoiTheoDoi-1 where MaNguoiBan=@shop1
end



