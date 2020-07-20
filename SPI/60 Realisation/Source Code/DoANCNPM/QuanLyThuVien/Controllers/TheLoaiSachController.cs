using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class TheLoaiSachController : Controller
    {
        private readonly DataContext _context;

        public TheLoaiSachController(DataContext context)
        {
            _context = context;
        }

        // GET: TheLoaiSach
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheLoaiSach.ToListAsync());
        }

        // GET: TheLoaiSach/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiSach = await _context.TheLoaiSach
                .FirstOrDefaultAsync(m => m.TheLoaiSachID == id);
            if (theLoaiSach == null)
            {
                return NotFound();
            }

            return View(theLoaiSach);
        }

        // GET: TheLoaiSach/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheLoaiSach/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TheLoaiSach theLoaiSach)
        {
            int count = _context.TheLoaiSach.Count();
            if (ModelState.IsValid)
            {
                if (count >= 3)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    _context.Add(theLoaiSach);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }       
            return View(theLoaiSach);
        }

        // GET: TheLoaiSach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiSach = await _context.TheLoaiSach.FindAsync(id);
            if (theLoaiSach == null)
            {
                return NotFound();
            }
            return View(theLoaiSach);
        }

        // POST: TheLoaiSach/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TheLoaiSachID,TenTLSach")] TheLoaiSach theLoaiSach)
        {
            if (id != theLoaiSach.TheLoaiSachID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theLoaiSach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheLoaiSachExists(theLoaiSach.TheLoaiSachID))
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
            return View(theLoaiSach);
        }

        // GET: TheLoaiSach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theLoaiSach = await _context.TheLoaiSach
                .FirstOrDefaultAsync(m => m.TheLoaiSachID == id);
            if (theLoaiSach == null)
            {
                return NotFound();
            }

            return View(theLoaiSach);
        }

        // POST: TheLoaiSach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theLoaiSach = await _context.TheLoaiSach.FindAsync(id);
            _context.TheLoaiSach.Remove(theLoaiSach);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheLoaiSachExists(int id)
        {
            return _context.TheLoaiSach.Any(e => e.TheLoaiSachID == id);
        }
    }
}
