using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace DoAn.Areas.Admin.Models
{
    public class CSDLContext:DbContext
    {
        public DbSet<Laptop> Laptop { get; set; }
        public DbSet<ThuongHieu> ThuongHieu { get; set; }
        public DbSet<Account> AdminAccount { get; set; }
        public DbSet<TinhTrangLaptop> TinhTrangLaptop { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public CSDLContext()
        {
            SqlConnectionStringBuilder sqlb = new SqlConnectionStringBuilder();
            sqlb.DataSource = "DESKTOP-MQ8QBMD\\SQLEXPRESS";
            sqlb.InitialCatalog = "WNC";
            sqlb.IntegratedSecurity = true;

            Database.Connection.ConnectionString = sqlb.ConnectionString;
        }
    }
    public class Laptop
    {
        [Key]
        public int LaptopID { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "Không được bỏ trống trường này")]
        public string TenLaptop { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống trường này")]
        [Range(1000,1000000)]
        public int Giatien { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống trường này")]
        public string ThongSo { get; set; }
        public string Image { get; set; }
        public Nullable<int> MaTH { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        public Nullable<int> MaTT { get; set; }
        public virtual TinhTrangLaptop TinhTrangLaptop { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
    public class ThuongHieu
    {
        [Key]
        public int MaTH { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống trường này")]
        public string TenThuongHieu { get; set; }
    }
    public class TinhTrangLaptop
    {
        [Key]
        public int MaTT { get; set; }
        public string TenTinhTrang { get; set; }
        public virtual ICollection<Laptop> Laptops { get; set; }
    }
    public class Comment
    {
        [Key]
        public int MaBL { get; set; }
        public DateTime Ngay { get; set; }
        [Required(ErrorMessage ="chưa nhập bình luận")]
        public string BinhLuan { get; set; }
        public Nullable<int> AcountID { get; set; }
        public virtual Account Account { get; set; }
        public virtual Laptop Laptop { get; set; }
        public Nullable<int> LaptopID { get; set; }

    }
    public class Account
    {
        [Key]
        public int AcountID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}