using DoAn.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAn.Areas.Admin.Controllers
{
    //[Authorize]
    
    public class HomeController : Controller
    {
        
        private CSDLContext context = new CSDLContext();
        // GET: Admin/Home   
        public ActionResult Index(int? id)
        {
            if (Session["AdminID"]!=null || Session["UserID"]!=null)
            {
                int k = 0;
                if (id != null)
                {
                    k = id.GetValueOrDefault() * 5;
                }
                List<Laptop> lt = context.Laptop.OrderBy(s => s.LaptopID).Skip(k).Take(5).ToList();
                ViewBag.ds = lt;
                ViewBag.count = context.Laptop.Count() / 5;
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }          
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if(Session["AdminID"]!= null||Session["UserID"]!=null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                 var user = context.AdminAccount.FirstOrDefault(s => s.Username == username && s.Password == password);
                
                if (user.AcountID > 0)
                {
                    if(user.DisplayName == "User")
                    {
                        Session["DisplayName"] = context.AdminAccount.FirstOrDefault().DisplayName;

                        Session["UserID"] = context.AdminAccount.FirstOrDefault(x=>x.DisplayName == "User").AcountID;
                    }
                    else
                    {
                        Session["DisplayName"] = context.AdminAccount.FirstOrDefault().DisplayName;
                        Session["AdminID"] = context.AdminAccount.FirstOrDefault().AcountID;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Login Failed";
                    return RedirectToAction("Login");
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        
        public ActionResult Add()
        {
            if(Session["AdminID"]==null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else 
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                ViewBag.MaTH = new SelectList(context.ThuongHieu, "MaTH", "TenThuongHieu");
                return View();
            }
        }
        public ActionResult DoiMatKhau(int id)
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                AdminDetail admin = new AdminDetail();
                Account ac = admin.Tim(id);
                ViewBag.ac = ac;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ResultDoiMatKhau(int id, string password)
        {
            AdminDetail admin = new AdminDetail();
            int tmp = admin.DoiMatKhau(id, password);
            if (tmp != 0) ViewBag.Message = "Success";
            else ViewBag.Message = "Failed";
            return View("AdminProfile");
        }
        [HttpPost]
        public ActionResult Add(Laptop laptop, HttpPostedFileBase hinhanh)
        {

            if (ModelState.IsValid)
            {
                if (hinhanh.ContentLength > 0)
                {
                    string _filename = Path.GetFileName(hinhanh.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Areas/Content/img/"), _filename);

                    hinhanh.SaveAs(_path);
                    laptop.Image = hinhanh.FileName;
                }
                context.Laptop.Add(laptop);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.MaTH = new SelectList(context.ThuongHieu, "MaTH", "TenThuongHieu", laptop.MaTH);
            return View(laptop);
        }
        public ActionResult TimGanDung(string tensp)
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                var c = context.Laptop.Where(s => s.TenLaptop.Contains(tensp)).ToList();
                ViewBag.c = c;
                return View();
            }
            
        }
        //Admin Profile
        public ActionResult AdminProfile()
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                AdminDetail admin = new AdminDetail();
                List<Account> detail = admin.DSAdmin();
                ViewBag.detail = detail;
                return View();
            }
        }
        public ActionResult Xoa(int id)
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                CuaHangBanLaptop ch = new CuaHangBanLaptop();
                int tmp = ch.xoa(id);
                if (tmp != 0) ViewBag.Message = "Success";
                else ViewBag.Message = "Failed";
                return View();
            }
        }
        public ActionResult Sua(int id)
        {
            if (Session["AdminID"] == null && Session["UserID"]==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                CuaHangBanLaptop ch = new CuaHangBanLaptop();
                Laptop lt = ch.tim(id);
                ViewBag.lt = lt;
                return View();
            }
        }
        public ActionResult ResultSua(int id, string tensp, int giatien, string thongso, int MaTH)
        {
            CuaHangBanLaptop ch = new CuaHangBanLaptop();
            ch.sua(id, tensp, giatien, thongso,MaTH);
            return RedirectToAction("Index");
        }
        public ActionResult SXTen()
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                CuaHangBanLaptop ch = new CuaHangBanLaptop();
                List<Laptop> lt = ch.SXTen();
                ViewBag.ds = lt;
                return View();
            }
               
        }
        public ActionResult SXGiaTien()
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                CuaHangBanLaptop ch = new CuaHangBanLaptop();
                List<Laptop> lt = ch.SXGiaTienTang();
                ViewBag.ds = lt;
                return View();
            }
                
        }
        public ActionResult SXGiaTienGiam()
        {
            if (Session["AdminID"] == null && Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                if (Session["UserID"] != null)
                {
                    return RedirectToAction("Denied");
                }
                CuaHangBanLaptop ch = new CuaHangBanLaptop();
                List<Laptop> lt = ch.SXGiaTienGiam();
                ViewBag.ds = lt;
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Denied()
        {
            return View();
        }
    }
}