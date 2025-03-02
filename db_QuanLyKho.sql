CREATE DATABASE PMQuanLyKho;
USE PMQuanLyKho;

CREATE TABLE don_vi
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	ten_don_vi NVARCHAR(256),
	mo_ta NVARCHAR(512),
	ngay_tao DATETIME DEFAULT GETDATE()
);

CREATE TABLE nha_cung_cap
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	ten_nhacc NVARCHAR(256),
	dia_chi NVARCHAR(512),
	so_dien_thoai NVARCHAR(20),
	email NVARCHAR(200),
	mo_ta NVARCHAR(512),
	ngay_tao DATETIME
);

CREATE TABLE khach_hang
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	ten_khach_hang NVARCHAR(256),
	dia_chi NVARCHAR(512),
	so_dien_thoai NVARCHAR(20),
	email NVARCHAR(200),
	mo_ta NVARCHAR(512),
	ngay_tao DATETIME DEFAULT GETDATE()
);

CREATE TABLE loai
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	ten_loai NVARCHAR(200),
	mo_ta NVARCHAR(512),
	ngay_tao DATETIME DEFAULT GETDATE()
);

CREATE TABLE vat_tu
(
	id NVARCHAR(128) PRIMARY KEY,
	ten_vat_tu NVARCHAR(256),
	id_don_vi INT NOT NULL,
	id_nhacc INT NOT NULL,
	id_loai INT NOT NULL,
	QRCode NVARCHAR(512),
	BarCode NVARCHAR(512),
	mo_ta NVARCHAR(512),
	ngay_tao DATETIME DEFAULT GETDATE()
	FOREIGN KEY (id_don_vi) REFERENCES don_vi(Id),
	FOREIGN KEY (id_nhacc) REFERENCES nha_cung_cap(Id),
	FOREIGN KEY (id_loai) REFERENCES loai(Id)
);

CREATE TABLE quyen_nguoi_dung
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	ten_quyen NVARCHAR(256),
	mo_ta NVARCHAR(512),
	ngay_tao DATETIME DEFAULT GETDATE()
);

CREATE TABLE nguoi_dung
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	ten_nguoi_dung NVARCHAR(MAX),
	ten_dang_nhap NVARCHAR(100),
	mat_khau NVARCHAR(256),
	ngay_tao DATETIME DEFAULT GETDATE(),
	id_quyen INT NOT NULL,
	FOREIGN KEY (id_quyen) REFERENCES quyen_nguoi_dung(id)
);

CREATE TABLE nhap_kho
(
	id NVARCHAR(128) PRIMARY KEY,
	id_vat_tu NVARCHAR(128) NOT NULL,
	id_loai int NOT NULL,
	id_nguoi_dung INT NOT NULL,
	so_luong INT,
	ngay_nhap DATETIME DEFAULT GETDATE(),
	gia_nhap FLOAT,
	gia_ban FLOAT,
	mo_ta NVARCHAR(512),
	FOREIGN KEY (id_vat_tu) REFERENCES vat_tu(id),
	FOREIGN KEY (id_loai) REFERENCES loai(id),
	FOREIGN KEY (id_nguoi_dung) REFERENCES nguoi_dung(id)
);

CREATE TABLE xuat_Kho
(
	id NVARCHAR(128) PRIMARY KEY,
	id_khach_hang INT NOT NULL,
	id_hoadon NVARCHAR(256) NOT NULL,
	id_nguoi_dung int NOT NULL,
	Count INT,
	ngay_xuat DATETIME DEFAULT GETDATE(),
	id_nhap_kho NVARCHAR(128),
	mo_ta NVARCHAR(512),
	FOREIGN KEY (id_khach_hang) REFERENCES khach_hang(id),
	FOREIGN KEY (id_nguoi_dung) REFERENCES nguoi_dung(id),
	FOREIGN KEY (id_nhap_kho) REFERENCES nhap_kho(id)
);

INSERT INTO quyen_nguoi_dung (ten_quyen, mo_ta)
VALUES 
('Quản trị viên', 'Người có quyền quản lý toàn bộ hệ thống'),
('Người dùng', 'Người sử dụng thông thường trong hệ thống');


INSERT INTO nguoi_dung (ten_nguoi_dung, ten_dang_nhap, mat_khau, id_quyen)
VALUES
(N'Nguyễn Văn A', 'admin', 'db69fc039dcbd2962cb4d28f5891aae1', 1),--pass admin -- Người dùng với quyền quản trị viên
(N'Trần Thị B', 'user', '93f880f9cdeb43b08e59e385a0681515', 2);-- pass user -- Người dùng với quyền thông thường
