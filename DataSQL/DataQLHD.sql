/*--------------------------------------------
           Tạo DATABASE và TABLE
--Trước khi chạy script này, phải thực hiện:
-- (1) Đổi tên Database
-- (2) Kiểm tra các dữ liệu đưa vào trước
----------------------------------------------*/
USE master
GO

--Tạo Database
CREATE DATABASE [QLHD]
GO

-- Sử dụng database
USE [QLHD]
GO

-- Setup thông số môi trường
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

--Tạo Tables

CREATE TABLE dbo.tHopDong(
	ID			int PRIMARY KEY IDENTITY(1,1),
	SOHD		nvarchar(MAX) NOT NULL,					-- Số hợp đồng	
	ID_GT		INT NOT NULL,							-- Id gói thầu
	ID_NT		int NOT NULL,							-- Id nhà thầu
	ID_CP		int NOT NULL,							-- Id chi phí (Tư vấn, Xây lắp, khác ...)
	ID_HT		int NOT NULL,							-- Id hình thức hợp đồng (Trọn gói, Điều chỉnh ...)
	ID_HD_GOC	int NULL,  								-- Id hợp đồng ban đầu (áp dụng cho PLHĐ)
	ID_HD_CHINH	int NULL,  								-- Id hợp đồng thầu chính (áp dụng cho hợp đồng A-B, B-B')	
	NOIDUNG		nvarchar(MAX) NULL,						-- Tóm tắt nội dung cơ bản của hợp đồng
	GIAHD		float NOT NULL DEFAULT 0.0,				-- Giá trị hợp đồng		
	NGAYHD		datetime NOT NULL DEFAULT GETDATE(),	-- Ngày ký hợp đồng
	NGAYKC		datetime NOT NULL DEFAULT GETDATE(),	-- Ngày khởi công
	NGAYKT		datetime NOT NULL DEFAULT GETDATE(),	-- Ngày kết thúc
	NGAYBLTU	datetime NULL,							-- Ngày hết hạn bảo lãnh tạm ứng
	NGAYBLHD	datetime NULL,							-- Ngày hết hạn bảo lãnh thực hiện hợp đồng	
	TLTAMUNG	float NULL DEFAULT 0.2,					-- Tỷ lệ tạm ứng theo hợp đồng
	TLBLHD		float NULL DEFAULT 0.03,				-- Tỷ lệ bảo lãnh thực hiện hợp đồng
	TLQT		float NULL DEFAULT 0.05,				-- Tỷ lệ giữ lại chờ quyết toán
	FILEPDF		nvarchar(MAX) NULL,						-- Tên file PDF
	FILEOFFICE	nvarchar(MAX) NULL,						-- Tên file OFFICE	
	bTHANHLY	bit NOT NULL DEFAULT 0,					-- Xác định hợp đồng đã thanh lý hay chưa?
	bQUYETTOAN	bit NOT NULL DEFAULT 0,					-- Xác định hợp đồng đã quyết toán hay chưa?
	GHICHU		nvarchar(MAX) NULL						-- Ghi chú
)
GO

CREATE TABLE dbo.tNhaThau(
	ID			int PRIMARY KEY IDENTITY(1,1),
	KH			nvarchar(20) NULL,			-- Ký hiệu (tên nhà thầu viết tắt)
	TENNT		nvarchar(MAX) NOT NULL,		-- Tên Nhà thầu
	MST			nvarchar(20) NOT NULL,		-- Mã số thuế
	DIACHI		nvarchar(MAX) NOT NULL,		-- Địa chỉ đăng ký kinh doanh
	LIENHE		nvarchar(MAX) NOT NULL,		-- Liên hệ (Điện thoại; Fax; Email ...)
	GHICHU		nvarchar(MAX) NULL			-- Ghi chú
)
GO

CREATE TABLE dbo.tGoiThau(
	ID			int PRIMARY KEY IDENTITY(1,1),
	KH			nvarchar(20) NULL,			-- Ký hiệu gói thầu
	TENGOITHAU	nvarchar(MAX) NOT NULL		-- Tên gói thầu
)
GO

CREATE TABLE dbo.tHinhThuc(
	ID			int PRIMARY KEY IDENTITY(1,1),
	KH			nvarchar(20) NULL,			-- Tên hình thức hợp đồng (viết tắt)
	TENHTHD		nvarchar(MAX) NOT NULL		-- Tên hình thức hợp đồng
)
GO

CREATE TABLE dbo.tChiPhi(
	ID			int PRIMARY KEY IDENTITY(1,1),
	KH			nvarchar(20) NULL,			-- Ký hiệu chi phí (viết tắt)
	TENCP		nvarchar(MAX) NOT NULL		-- Tên chi phí (Tư vấn, Xây lắp, khác ...) 	
)
GO

CREATE TABLE dbo.tThanhToan(
	ID			int PRIMARY KEY IDENTITY(1,1),	
	SOPTT		nvarchar(20) NOT NULL,				-- Số phiếu thanh toán
	NGAYPTT		datetime NOT NULL DEFAULT GETDATE(),-- Ngày phiếu thanh toán
	GTHT		float NOT NULL DEFAULT 0.0,			-- Giá trị hoàn thành
	GTPS		float NOT NULL DEFAULT 0.0, 		-- Giá trị phát sinh
	GTKTTU		float NOT NULL DEFAULT 0.0,			-- Giá trị khấu trừ tạm ứng
	GTKTQT		float NOT NULL DEFAULT 0.0,			-- Giá trị giữ lại chờ quyết toán
	GTKTKH		float NOT NULL DEFAULT 0.0,			-- Giá trị giữ lại khác
	GTTT		float NOT NULL DEFAULT 0.0,			-- Giá trị thanh toán trong kỳ
	GTGN		float NOT NULL DEFAULT 0.0,			-- Giá trị giải ngân trong kỳ
	ID_HD		int NOT NULL,						-- Id hợp đồng
	ID_NV		int NOT NULL,						-- Id nguồn vốn (CSH, Vay NĐT, vay NH)
	FILEPDF		nvarchar(MAX) NULL,					-- Tên file PDF
	FILEOFFICE	nvarchar(MAX) NULL,					-- Tên file OFFICE	
	GHICHU		nvarchar(MAX) NULL					-- Ghi chú
 )
GO

CREATE TABLE dbo.tNguonVon(
	ID			int PRIMARY KEY IDENTITY(1,1),
	KH			nvarchar(20) NULL,			-- Ký hiệu (tên nguồn vốn viết tắt)
	TENNV		nvarchar(MAX) NOT NULL		-- Tên nguồn vốn (CSH, Vay NĐT, vay NH)	
)
GO

CREATE TABLE dbo.tKeHoach(
	ID			int PRIMARY KEY IDENTITY(1,1),	
	TENKH		nvarchar(MAX) NOT NULL,		-- Tên kế hoạch giải ngân
	GIATRI		float NOT NULL DEFAULT 0.0,	-- Tổng giá trị kế hoạch giải ngân
	GHICHU		nvarchar(MAX) NULL			-- Ghi chú
)
GO

CREATE TABLE dbo.tNhanVien
(
	ID			int PRIMARY KEY IDENTITY(1,1),
	USERNAME	NVARCHAR(20) NOT NULL,				-- Tên đăng nhập
	HOTEN		NVARCHAR(MAX) NOT NULL,				-- Tên nhân viên
	QUYEN		NVARCHAR(2) NOT NULL DEFAULT 'KH',	-- Quyền của nhân viên (LD, KH, KT, DA, NT, AD)
	MATKHAU		NVARCHAR(MAX) NOT NULL DEFAULT '2003011414115776479911161271372042013444',
	bHIEULUC	BIT DEFAULT 1						-- Hiệu lực của user
)
GO

CREATE TABLE dbo.tDangNhap
(
	USERNAME	NVARCHAR(20) NOT NULL,				-- Username nhân viên đăng nhập
	NGAYGIOVAO	DATETIME NOT NULL DEFAULT GETDATE(),-- Ngày giờ vào
	NGAYGIORA	DATETIME NULL						-- Ngày giờ ra
)
GO

CREATE TABLE dbo.tThongSo
(
	ID			int PRIMARY KEY IDENTITY(1,1),
	KH			NVARCHAR(20) NOT NULL,				-- Ký hiệu của thông số
	NOIDUNG		NVARCHAR(MAX) NULL,					-- Nội dung (mô tả) thông số 
	STR_DATA	NVARCHAR(MAX) NOT NULL,				-- Chuỗi thông số kết nối vào SQLSERVER
	STR_PATH	NVARCHAR(MAX) NOT NULL,				-- Chuỗi chứa đường dẫn file đính kèm
	bHIEULUC	BIT DEFAULT 0						-- Hiệu lực của thông số
)
GO

/* ĐƯA DỮ LIỆU BAN ĐẦU */

-- Insert tNhanVien
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'haint', N'Ngô Thanh Hải', N'AD', N'2003011414115776479911161271372042013444', 1)
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'giangnt', N'Nguyễn Thị Giang', N'KH', N'2003011414115776479911161271372042013444', 1)
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'thuynt', N'Nguyễn Thị Thùy', N'KH', N'2003011414115776479911161271372042013444', 1)
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'hieuhv', N'Huỳnh Văn Hiếu', N'LD', N'2003011414115776479911161271372042013444', 1)
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'anht', N'Trần Anh', N'DA', N'2003011414115776479911161271372042013444', 1)
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'phuongtt', N'Trần Thị Phượng', N'NT', N'2003011414115776479911161271372042013444', 1)
INSERT INTO dbo.tNhanVien (USERNAME, HOTEN, QUYEN, MATKHAU, bHIEULUC)
VALUES (N'huongdt', N'Đàm Thị Hường', N'KT', N'2003011414115776479911161271372042013444', 1)
GO

-- Insert tChiPhi
INSERT INTO dbo.tChiPhi (KH, TENCP) VALUES (N'GPMB', N'Chi phí giải phóng mặt bằng (GPMB)')
INSERT INTO dbo.tChiPhi (KH, TENCP) VALUES (N'TVDT', N'Chi phí tư vấn đầu tư xây dựng (TVĐT)')
INSERT INTO dbo.tChiPhi (KH, TENCP) VALUES (N'XDTB', N'Chi phí xây dựng và thiết bị (XDTB)')
INSERT INTO dbo.tChiPhi (KH, TENCP) VALUES (N'QLDA', N'Chi phí quản lý dự án (QLDA)')
INSERT INTO dbo.tChiPhi (KH, TENCP) VALUES (N'KHAC', N'Chi phí khác (không thuộc GPMB, TVĐT, XDTB, QLDA')
GO

-- Insert tHinhThuc
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'TRONGOI', N'Hợp đồng trọn gói')
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'CODINH', N'Hợp đồng đơn giá cố định')
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'DIEUCHINH', N'Hợp đồng đơn giá điều chỉnh')
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'THOIGIAN', N'Hợp đồng theo thời gian')
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'CONGPHI', N'Hợp đồng theo chi phí cộng phí')
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'XDKHAC', N'Hợp đồng xây dựng khác')
INSERT INTO dbo.tHinhThuc (KH, TENHTHD) VALUES (N'HONHOP', N'Hợp đồng theo giá kết hợp')
GO

-- Insert tNguonVon
INSERT INTO dbo.tNguonVon (KH, TENNV) VALUES (N'NSNN', N'Vốn Ngân sách Nhà nước')
INSERT INTO dbo.tNguonVon (KH, TENNV) VALUES (N'BOT-CSH', N'Vốn chủ sở hữu do Nhà đầu tư góp')
INSERT INTO dbo.tNguonVon (KH, TENNV) VALUES (N'BOT-VAY1', N'Vốn vay Ngân hàng (TPBANK)')
INSERT INTO dbo.tNguonVon (KH, TENNV) VALUES (N'BOT-VAY2', N'Vốn vay Nhà đầu tư (CC1)')
GO

-- Insert tThongSo
INSERT INTO dbo.tThongSo (KH, STR_DATA, STR_PATH, bHIEULUC, NOIDUNG)
VALUES (N'QLHD_HP', N'Data Source=192.168.1.222,1433;Initial Catalog=QLHD_HP;Persist Security Info=True;User ID=BOT.HP;Password=hp12345', N'\\192.168.1.222\QLHD\FILE_ATT', 1, N'Dự án PPP ven biển Hải Phòng - Thời gian từ 2018-2023')
GO

