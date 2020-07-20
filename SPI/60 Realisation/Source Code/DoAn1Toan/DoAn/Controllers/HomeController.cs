using DoAn.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DoAn.Controllers
{
    public class HomeController : Controller
    {
        CSDLContext context = new CSDLContext();
        public ActionResult Index(int? id)
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
        public ActionResult Detail(int id)
        {
            var laptop = context.Laptop.FirstOrDefault(m => m.LaptopID == id);
            var dsdanhgia = context.Comment.Where(a => a.LaptopID == id).Include(l => l.Account).ToList();
            var SLComment = context.Comment.Where(a => a.LaptopID == id).Count();
            ViewBag.dsdanhgia = dsdanhgia;
            ViewBag.SLComment = SLComment;
           
            if (laptop == null)
            {
                return View("index");
            }
            return View(laptop);
        }
        [HttpPost]
        public ActionResult CreateCmt(int laptopID, string text)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            if (Session["UserID"] == null)
            {
                return RedirectToAction("login");
            }
            var comment = new Comment();
                comment.BinhLuan = text;            
                comment.Ngay = DateTime.Now;
                comment.AcountID = userId;
                comment.LaptopID = laptopID;      
                context.Comment.Add(comment);
                context.SaveChanges();
                // return View("index","homepage");
                return RedirectToAction("Detail", new RouteValueDictionary(new { Controller = "Home", Action = "Detail", id = laptopID })); 
        }
        public ActionResult SetTheme(string data)
        {
            HttpCookie cookie = new HttpCookie("theme", data);
            cookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(cookie);
            return RedirectToAction("index","home");
        }
    }
}