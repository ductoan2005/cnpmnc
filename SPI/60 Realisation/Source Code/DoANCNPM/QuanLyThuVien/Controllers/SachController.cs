using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class SachController : Controller
    {
        private readonly DataContext _context;

        public SachController(DataContext context)
        {
            _context = context;
        }

        // GET: Sach
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var ds = _context.Sach.Include(l => l.TheLoaiSach).Include(l=>l.TinhTrangSach);
            List<Sach> sach = await ds.ToListAsync();
            ViewBag.ds = sach;
            return View();
        }

        // GET: Sach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach.Include(l=>l.TinhTrangSach).Include(l=>l.TheLoaiSach)
                .FirstOrDefaultAsync(m => m.SachID == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // GET: Sach/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var ds = _context.TheLoaiSach.ToList();
            var tt = _context.TinhTrangSach.ToList();
            ViewBag.tt = tt;
            ViewBag.ds = ds;
            return View();
        }

        // POST: Sach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sach sach,IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                if (photo == null)
                {
                    sach.HinhAnh = "iphone-11-pro-max-green-600x600.jpg";
                }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);

                    await photo.CopyToAsync(stream);
                    sach.HinhAnh = photo.FileName;
                }
                sach.TinhTrangSachID = 1;
                sach.NamHetHan = sach.NgayXB.AddYears(8);
                _context.Add(sach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sach);
        }

        // GET: Sach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login","Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            var ds = _context.TheLoaiSach.ToList();
            ViewBag.ds = ds;
            var sach = await _context.Sach.FindAsync(id);
            if (sach == null)
            {
                return NotFound();
            }
            return View(sach);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sach sach, IFormFile photo)
        {
            if (id != sach.SachID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (photo == null)
                    {
                        sach.HinhAnh = "iphone-11-pro-max-green-600x600.jpg";
                    }
                    else
                    {
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image", photo.FileName);
                        var stream = new FileStream(path, FileMode.Create);

                        await photo.CopyToAsync(stream);
                        sach.HinhAnh = photo.FileName;
                    }
                    _context.Update(sach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SachExists(sach.SachID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sach);
        }

        // GET: Sach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("Admin") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }

            var sach = await _context.Sach
                .FirstOrDefaultAsync(m => m.SachID == id);
            if (sach == null)
            {
                return NotFound();
            }

            return View(sach);
        }

        // POST: Sach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sach = await _context.Sach.FindAsync(id);
            _context.Sach.Remove(sach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SachExists(int id)
        {
            return _context.Sach.Any(e => e.SachID == id);
        }
    }
}
