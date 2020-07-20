use CNPMNC_TH
go 


-- TRIGGER kiểm tra số loại độc giả = 2
CREATE TRIGGER TG_KiemTraSLLoaiDG
	ON LOAIDG
	AFTER INSERT,UPDATE
AS
BEGIN	
		declare @count int
		set @count = (select count(*) from LOAIDG  )
		if(@count > 2)
			begin
				raiserror('số loại loại độc giả đã đủ',16,1)
				rollback tran
				return
			end
END
Go

---- TRIGGER kiểm tra tuổi đọc giả

--CREATE TRIGGER TG_KiemTraTuoiDG
--	ON DOCGIA
--	AFTER INSERT,UPDATE
--AS
--BEGIN	
--		declare  @age int
--		set @age = (select year(GETDATE()) - YEAR(Ngaysinh) as namsinh from DOCGIA)
--		if(@age > 55)
--			begin
--				raiserror('tuổi đọc giả phải trong khoảng 18-55',16,1)
--				rollback tran
--				return
--			end
--		else 
--				if(@age < 18)
--				raiserror('tuổi đọc giả phải trong khoảng 18-55',16,1)
--		rollback tran
--		return		
--END
--Go

-- ngày hết hạn = ngày đăng ký + 6 month
CREATE TRIGGER TG_KiemTraNgayHetHan
	ON DOCGIA
	AFTER INSERT,UPDATE
AS
BEGIN	
		declare @ngayhethan date
			set @ngayhethan =(select NgayHetHan from inserted)
			declare @ngaydangky date
		set @ngaydangky =(select Ngaylapthe from inserted)

		set @ngayhethan= DATEADD(MONTH,6,@ngaydangky)
		update DOCGIA set NgayHetHan=@ngayhethan
END
Go


-- thể loại sách =3

CREATE TRIGGER TG_KiemTraSLTheLoaiSach
	ON THELOAISACH
	AFTER INSERT,UPDATE
AS
BEGIN	
		declare @count int
		set @count = (select count(*) from THELOAISACH  )
		if(@count > 3)
			begin
				raiserror('số lượng thể loại sách đã đủ',16,1)
				rollback tran
				return
			end
END
Go

-- năm đến hạn thanh lý = năm xuất bản + 8 năm
CREATE TRIGGER TG_KiemTraNamThanhLy
	ON Sach
	AFTER INSERT,UPDATE
AS
BEGIN	
		declare @namthanhly date
		set @namthanhly = (select NamDKHTL from inserted  )
		declare @namxuatban date
		set @namxuatban = (select NamXB from inserted  )

		set @namthanhly=dateadd(year,8,@namxuatban)
		update SACH set NamDKHTL=@namthanhly  
END
Go

-- cập nhật sách sau khi mượn
CREATE TRIGGER TG_DemSLSachSauKhiMuon
	ON CT_PHIEUMUON
	AFTER INSERT,UPDATE
AS
		
		BEGIN	
		declare @MaPhieuMuon int
			set @MaPhieuMuon =(select MaPhieuMuon from inserted)
		declare @SLsachmuon int
		set @SLsachmuon =(select COUNT(*) from inserted where MaPhieuMuon=@MaPhieuMuon)
		declare @MaDocGia int
			set @MaDocGia =(select MaDG from PHIEUMUON where PHIEUMUON.MaPhieuMuon=@MaPhieuMuon )
		update DOCGIA set SLSDaMuon = SLSDaMuon+ @SLsachmuon
		END
		Go


		select COUNT(*) from CT_PHIEUMUON 
-- cập nhật sách sau khi Trả
CREATE TRIGGER TG_DemSLSachSauKhiTra
	ON CT_PHIEUTRA
	AFTER INSERT,UPDATE
AS
		
		BEGIN	
		declare @MaPhieutra int
			set @MaPhieutra =(select MaPhieuTra from inserted)
		declare @SLsachtra int
		set @SLsachtra =(select COUNT(*) from inserted)
		declare @MaDocGia int
			set @MaDocGia =(select MaDG from PHIEUMUON where PHIEUMUON.MaPhieuMuon=@MaPhieutra )
		update DOCGIA set SLSDaMuon = SLSDaMuon - @SLsachtra where MaDG=@MaDocGia
		END
Go

--