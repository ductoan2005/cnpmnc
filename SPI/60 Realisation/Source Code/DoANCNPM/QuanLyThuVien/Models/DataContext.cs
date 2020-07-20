using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using QuanLyThuVien.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<LoaiDocGia> LoaiDocGia { get; set; }
        public DbSet<DocGia> DocGia { get; set; }
        public DbSet<TheLoaiSach> TheLoaiSach { get; set; }
        public DbSet<Sach> Sach { get; set; }
        public DbSet<PhieuMuon> PhieuMuon { get; set; }
        public DbSet<CT_PhieuMuon> CT_PhieuMuon { get; set; }
        public DbSet<PhieuTra> PhieuTra { get; set; }
        public DbSet<CT_PhieuTra> CT_PhieuTra { get; set; }
        public DbSet<PhieuThuTienPhat> PhieuThuTienPhat { get; set; }
        public DbSet<TinhTrangSach> TinhTrangSach { get; set; }
        public DbSet<TinhTrangPhieuMuon> TinhTrangPhieuMuon { get; set; }
        public DbSet<Cart> Cart { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TinhTrangPhieuMuon>().HasData(
                new TinhTrangPhieuMuon
                {
                    TinhTrangPhieuMuonID = 1,
                    TinhTrangPM = "Chưa Trả"
                },
                new TinhTrangPhieuMuon
                {
                    TinhTrangPhieuMuonID = 2,
                    TinhTrangPM = "Đã Trả"
                }
                );
            builder.Entity<TinhTrangSach>().HasData(
                new TinhTrangSach
                {
                    TinhTrangSachID = 1,
                    TenTTSach = "Trống",
                },
                new TinhTrangSach
                {
                    TinhTrangSachID = 2,
                    TenTTSach = "Cho Thuê",
                },
                new TinhTrangSach
                {
                    TinhTrangSachID = 3,
                    TenTTSach = "Mất",
                }
                );
        }
    }
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public int SoLuong { get; set; }
        public Sach Sach { get; set; }
        public Nullable<int> SachID { get; set; }
        public DocGia DocGia { get; set; }
        public Nullable<int> DocGiaID { get; set; }
        public ICollection<CT_PhieuMuon> CT_PhieuMuons { get; set; }
    }
    public class LoaiDocGia
    {
        [Key]
        public int LoaiDocGiaID { get; set; }
        [Required]
        public string TenDocGia { get; set; }
        public virtual ICollection<DocGia> DocGias { get; set; }
    }
    public class DocGia
    {
        [Key]
        public int DocGiaID { get; set; }
        [Required]
        public string TenDocGia { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime NgaySinh { get; set; }
        [Required]
        public string DiaChi { get; set; }
        public DateTime NgayLapThe { get; set; }
        public int SLSachDaMuon { get; set; }
        public DateTime NgayHetHan { get; set; }
        public bool IsSelected { get; set; }
        public double ConLai { get; set; }
        public virtual LoaiDocGia LoaiDocGia { get; set; }
        public Nullable<int> LoaiDocGiaID { get; set; }
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<PhieuTra> PhieuTras { get; set; }
        public virtual ICollection<PhieuThuTienPhat> PhieuThuTienPhats { get; set; }

    }
    public class TheLoaiSach
    {
        [Key]
        public int TheLoaiSachID { get; set; }
        [Required]
        public string TenTLSach { get; set; }
        public virtual ICollection<Sach> Sachs { get; set; }
    }
    public class TinhTrangSach
    {
        [Key]
        public int TinhTrangSachID { get; set; }
        public string TenTTSach { get; set; }
        public ICollection<Sach> Sachs { get; set; }
    }
    public class Sach
    {
        [Key]     
        public int SachID { get; set; }
        [Required]
        public string TenSach { get; set; }
        public string HinhAnh { get; set; }
        [Required]
        public string NhaXB { get; set; }
        [Required]
        public DateTime NgayNhap { get; set; }
        public DateTime NamHetHan { get; set; }
        
        public virtual TinhTrangSach TinhTrangSach { get; set; }
        public Nullable<int> TinhTrangSachID { get; set; }
        public int Gia { get; set; }
        [Required]
        public string TacGia { get; set; }
        public DateTime NgayXB { get; set; }
        
        public virtual TheLoaiSach TheLoaiSach { get; set; }
        public Nullable<int> TheLoaiSachID { get; set; }

        public virtual ICollection<CT_PhieuMuon> CT_PhieuMuons { get; set; }
        public virtual ICollection<CT_PhieuTra> CT_PhieuTras { get; set; }
    }
    public class PhieuMuon
    {
        [Key]
        public int PhieuMuonID { get; set; }
        public DateTime NgayMuon { get; set; }
        
        public virtual TinhTrangPhieuMuon TinhTrangPhieuMuon { get; set; }
        public Nullable<int> TinhTrangPhieuMuonID { get; set; }

        public virtual DocGia DocGia { get; set; }
        public double TienCoc { get; set; }
        public Nullable<int> DocGiaID { get; set; }
        public virtual ICollection<CT_PhieuMuon> CT_PhieuMuons { get; set; }
    }
    public class TinhTrangPhieuMuon
    {
        [Key]
        public int TinhTrangPhieuMuonID { get; set; }
        public string TinhTrangPM { get; set; }
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
    }
    public class CT_PhieuMuon
    {
        [Key]
        public int CT_PhieuMuonID { get; set; }
        
        public virtual Sach Sach { get; set; }
        public Nullable<int> SachID { get; set; }
        public virtual Cart Cart { get; set; }
        public Nullable<int> CartID { get; set; }
        public virtual PhieuMuon PhieuMuon { get; set; }
        public Nullable<int> PhieuMuonID { get; set; }
        public bool IsSelected { get; set; }
    }
    public class PhieuTra
    {
        [Key]
        public int PhieuTraID { get; set; }
        public DateTime NgayTra { get; set; }
        public int TienPhat { get; set; }        
        public virtual DocGia DocGia { get; set; }
        public Nullable<int> DocGiaID { get; set; }

        public virtual ICollection<CT_PhieuTra> CT_PhieuTras { get; set; }
        public virtual ICollection<PhieuThuTienPhat> PhieuThuTienPhats { get; set; }

    }
    public class CT_PhieuTra
    {
        [Key]
        public int CT_PhieuTraID { get; set; }
        public int SoNgayMuon { get; set; }
       
        public virtual Sach Sach { get; set; }
        public Nullable<int> SachID { get; set; }
        
        public virtual PhieuTra PhieuTra { get; set; }
        public Nullable<int> PhieuTraID { get; set; }
    }
    public class PhieuThuTienPhat
    {
        [Key]
        public int PhieuThuTienPhatID { get; set; }
        public double TienCoc { get; set; }
        public double TienPhat { get; set; }
        public double TienConLai { get; set; }
        public virtual DocGia DocGia { get; set; }
        public Nullable<int> DocGiaID { get; set; }

        public Nullable<int> PhieuTraID { get; set; }
        public virtual PhieuTra PhieuTra { get; set; }

    }

}
