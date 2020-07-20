

CREATE DATABASE CNPMNC_TH
GO

USE CNPMNC_TH
GO

CREATE TABLE LOAIDG(
	MaLoaiDG int identity(1,1) Primary key ,
	TenLoaiDG nvarchar(50) not null
)

CREATE TABLE DOCGIA(
	MaDG int identity(1,1) primary key not null,
	MaLoaiDG int,
	TenDocGia nvarchar(50) not null,
	Email nvarchar(50) not null,
	Ngaysinh date,
	Diachi nvarchar(50) not null,
	Ngaylapthe date,
	SLSDaMuon int,
	NgayHetHan datetime,
	CONSTRAINT FK_DOCGIA_LOAIDG	FOREIGN KEY(MaloaiDG) REFERENCES LOAIDG(MaLoaiDG)
)


CREATE TABLE THELOAISACH(
	MaTLSach int identity(1,1) primary key,
	TenTLSach nvarchar(50) not null
)

CREATE TABLE SACH(
	MaSach int identity(1,1) primary key,
	TenSach nvarchar(50) not null,
	MaTLSach int not null,
	NhaXB nvarchar(50) not null,
	NgayNhap date not null,
	TinhTrang nvarchar(50) not null,
	Gia decimal,
	NamDKHTL date,
	TacGia nvarchar(50) not null,
	NamXB date,
	CONSTRAINT FK_Sach_THELOAISACH	FOREIGN KEY(MaTLSach) REFERENCES THELOAISACH(MaTLSach)
)


create table PHIEUMUON(
	MaPhieuMuon int identity(1,1) primary key,
	NgayMuon date,
	MaDG int,
	CONSTRAINT FK_PHIEUMUON_DOCGIA FOREIGN KEY(MaDG) REFERENCES DOCGIA(MaDG)
)

create table CT_PHIEUMUON(
	MaCT_PhieuMuon int identity(1,1) primary key,
	MaSach int not null,
	MaPhieuMuon int not null,
	CONSTRAINT FK_CT_PHIEUMUON_PHIEUMUON FOREIGN KEY(MaSach) REFERENCES SACH(MaSach),
	CONSTRAINT FK_CT_PHIEUMUON_DOCGIA FOREIGN KEY(MaPhieuMuon) REFERENCES PHIEUMUON(MaPhieuMuon)
)

create table PHIEUTRA(
	MaPhieuTra int identity(1,1) primary key,
	NgayTra date,
	TienPhat decimal,
	 MaDG int,

	CONSTRAINT FK_PHIEUTRA_DOCGIA FOREIGN KEY(MaDG) REFERENCES DOCGIA(MaDG)
)

create table CT_PHIEUTRA(
	MaCT_PhieuTra int identity(1,1) primary key,
	NgayMuon datetime,
	SoNgayMuon datetime,
	TienPhat decimal(6,2),
	MaSach int not null,
	MaPhieuTra int not null,
	CONSTRAINT FK_CT_PHIEUTRA_SACH FOREIGN KEY(MaSach) REFERENCES SACH(MaSach),
	CONSTRAINT FK_CT_PHIEUTRA_SACH_PHIEUTRA FOREIGN KEY(MaPhieuTra) REFERENCES PHIEUTRA(MaPhieuTra)

)

create table PHIEUTHUTIENPHAT(
	MaPhieuThuTP int identity(1,1) primary key,
	TongNo int,
	SoTienThu decimal,
	Conlai decimal,
	MaDG int,
	CONSTRAINT FK_PHIEUTHUTIENPHAT_DOCGIA	FOREIGN KEY(MaDG) REFERENCES DOCGIA(MaDG)
)





