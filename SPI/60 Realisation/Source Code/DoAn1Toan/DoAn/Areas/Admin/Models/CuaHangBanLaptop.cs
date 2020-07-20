using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn.Areas.Admin.Models
{
    public class CuaHangBanLaptop
    {
        public List<Laptop> DSLT()
        {
            List<Laptop> list = new List<Laptop>();
            using (CSDLContext db = new CSDLContext())
            {
                list = db.Laptop.ToList();
            }
            return list;
        }
        public int themsp(string tensp, int giatien, string thongso, int MaTH, string picture)
        {
            try
            {
                using (CSDLContext db = new CSDLContext())
                {
                    db.Laptop.Add(new Laptop
                    {
                        TenLaptop = tensp,
                        Giatien = giatien,
                        ThongSo = thongso,
                        MaTH = MaTH,
                        Image = picture,
                    });
                    db.SaveChanges();
                }
                return 1;
            }
            catch(Exception e)
            {
                return 0;
            } 
        }
        public List<Laptop> timtheoten(string tensp)
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.Laptop.Where(p => p.TenLaptop.Equals(tensp)).ToList();
            }
        }
        public Laptop tim(int id)
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.Laptop.Find(id);
            }
        }
        public List<Laptop> timgandung(string tensp)
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.Laptop.Where(s=>s.TenLaptop.Contains(tensp)).ToList();
            }
        }
        public int xoa(int id)
        {
            try
            {
                using (CSDLContext db = new CSDLContext())
                {
                    Laptop laptop = db.Laptop.Find(id);
                    db.Entry(laptop).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                return 1;
            }
            catch(Exception e)
            { return 0; }
        }
        public int sua(int id, string tensp, int giatien, string thongso, int MaTH)
        {
            try
            {
                using (CSDLContext db = new CSDLContext())
                {
                    Laptop laptop = db.Laptop.Find(id);
                    laptop.TenLaptop = tensp;
                    laptop.Giatien = giatien;
                    laptop.ThongSo = thongso;
                    laptop.MaTH = MaTH;
                    db.Entry(laptop).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return 1;
            }
            catch (Exception e)
            { return 0; }
        }
        public List<Laptop> SXTen()
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.Laptop.OrderBy(s => s.TenLaptop).ToList();
            }
        }
        public List<Laptop> SXGiaTienTang()
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.Laptop.OrderBy(s => s.Giatien).ToList();
            }
        }
        public List<Laptop> SXGiaTienGiam()
        {
            using (CSDLContext db = new CSDLContext())
            {
                return db.Laptop.OrderByDescending(s => s.Giatien).ToList();
            }
        }
    }
}