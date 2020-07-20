using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyThuVien.Models
{
    public class BookToCart
    {
        [Key]
        public int CartID { get; set; }
        public Sach Sach { get; set; }
        public int SoLuong { get; set; }
        public double TongTien { get; set; }
        public int DocGiaID { get; set; }
        public DocGia DocGia { get; set; }
        public override int GetHashCode()
        {
            return Sach.SachID;
        }
    }
}
