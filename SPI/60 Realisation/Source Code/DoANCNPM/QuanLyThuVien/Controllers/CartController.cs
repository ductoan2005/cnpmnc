using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using QuanLyThuVien.SessionHelper;

namespace QuanLyThuVien.Controllers
{
    public class CartController : Controller
    {
        DataContext db;
        public CartController(DataContext db)
        {
            this.db = db;
        }
        private int isExist(int id)
        {
            List<BookToCart> cart = SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Sach.SachID == id)
                {
                    return i;
                }
            }
            return -1;
        }
        public IActionResult Index()
        {
            var cart = SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.total = cart.Sum(a => a.Sach.Gia * a.SoLuong);
            return View();
        }
        public IActionResult Lent(int id)
        {

            if (SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart") == null)
            {
                List<BookToCart> cart = new List<BookToCart>();
                cart.Add(new BookToCart { Sach = db.Sach.FirstOrDefault(p => p.SachID == id), SoLuong = 1 });
                SessionHelper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<BookToCart> cart = SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart");
                int index = isExist(id);

                if (index != -1)
                {
                    cart[index].SoLuong++;
                }
                else
                {
                    cart.Add(new BookToCart { Sach = db.Sach.FirstOrDefault(p => p.SachID == id), SoLuong = 1 });
                }
                SessionHelper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult PhieuMuon(List<DocGia> model)
        {
            List<BookToCart> cart = SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart");
            if (cart.Sum(a => a.SoLuong) > 5)
            {
                ViewBag.Message = "Không thể thuê hơn 5 cuốn sách";
            }
            else
            {           
                for(int j=0;j<model.Count;j++)
                {
                    if(model[j].IsSelected)
                    {
                        PhieuMuon pm = new PhieuMuon();
                        pm.TienCoc = cart.Sum(a => a.Sach.Gia * a.SoLuong);
                        pm.DocGiaID = model[j].DocGiaID;
                        pm.NgayMuon = DateTime.Now;
                        pm.TinhTrangPhieuMuonID = 1;
                        db.PhieuMuon.Add(pm);
                        db.SaveChanges();
                        for (int i = 0; i < cart.Count; i++)
                        {
                            CT_PhieuMuon ct = new CT_PhieuMuon()
                            {
                                SachID = cart[i].Sach.SachID,
                                PhieuMuonID = pm.PhieuMuonID,
                            };
                            db.CT_PhieuMuon.Add(ct);
                            var sach = db.Sach.FirstOrDefault(p => p.SachID == cart[i].Sach.SachID);
                            sach.TinhTrangSachID = 2;
                            db.SaveChanges();
                        }
                        ViewBag.Message = "Cảm ơn bạn đã thuê sách,Thông tin mượn của bạn đã được lưu, xin mời in phiếu mượn sách";
                        cart.Clear();
                        SessionHelper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        return View();
                    }
                    else
                    {
                        return View("ChooseUser");
                    }
                }
            }
            return View("PhieuMuon");
        }
        [HttpGet]
        public IActionResult Remove(int id)
        {
            List<BookToCart> cart = SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult ChooseUser()
        {
            var user = new List<DocGia>();
            foreach (var docgia in db.DocGia.ToList())
            {
                user.Add(docgia);
            }

            return View(user);
        }
        //[HttpPost]
        //public IActionResult ChooseUser(int id, List<DocGia> model)
        //{
        //    Cart carts = new Cart();
        //    for (int i = 0; i < model.Count; i++)
        //    {
        //        var users = db.DocGia.Find(model[i].DocGiaID);

        //        if (model[i].IsSelected)
        //        {
        //            List<BookToCart> cart = SessionHelper.SessionHelper.GetObjectFromJson<List<BookToCart>>(HttpContext.Session, "cart");
        //            int index = isExist(id);
        //            if(cart.Sum(a=>a.SoLuong)<=5)
        //            {
                        
        //            }
        //            if (cart[index].SoLuong <= 5)
        //            {
        //                cart[index].DocGiaID = model[i].DocGiaID;
        //                users.SLSachDaMuon = cart[index].SoLuong;
        //                db.SaveChanges();
        //            }
        //            else return RedirectToAction("PhieuMuon");
        //            SessionHelper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        //        }
        //    }
        //    return RedirectToAction("PhieuMuon");
        //}
    }
}