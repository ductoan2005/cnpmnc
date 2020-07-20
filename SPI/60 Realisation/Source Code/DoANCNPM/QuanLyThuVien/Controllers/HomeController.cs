using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace QuanLyThuVien.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly DataContext context;

        public HomeController(DataContext context)
        {
            this.context = context;
           
        }
        public IActionResult Index(int? id)
        {
            if(HttpContext.Session.GetString("Admin")==null)
            {
                return RedirectToAction("Login");
            }
            int k = 0;
            if (id != null)
            {
                k = id.GetValueOrDefault() * 5;
            }
            List<Sach> lt = context.Sach.OrderBy(s => s.SachID).Skip(k).Take(5).Include(l => l.TheLoaiSach).ToList();
            ViewBag.sach = lt;
            ViewBag.count = context.Sach.Count() / 5;
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(DocGia docgia)
        {
            var signin = context.DocGia.FirstOrDefault(x => x.Email == docgia.Email && x.TenDocGia==docgia.TenDocGia);
            var loaidocgia = context.LoaiDocGia.Find(signin.LoaiDocGiaID);
            if(loaidocgia.TenDocGia == "Thủ Thư")
            {
                HttpContext.Session.SetString("Admin", signin.Email);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
    }
}