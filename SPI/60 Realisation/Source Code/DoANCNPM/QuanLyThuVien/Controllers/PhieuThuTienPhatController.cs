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
    public class PhieuThuTienPhatController : Controller
    {
        private readonly DataContext _context;

        public PhieuThuTienPhatController(DataContext context)
        {
            _context = context;
        }

        // GET: PhieuThuTienPhat
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PhieuThuTienPhat.Include(p => p.DocGia).Include(p => p.PhieuTra);
            return View(await dataContext.ToListAsync());
        }

        // GET: PhieuThuTienPhat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuThuTienPhat = await _context.PhieuThuTienPhat
                .Include(p => p.DocGia)
                .Include(p => p.PhieuTra)
                .FirstOrDefaultAsync(m => m.PhieuThuTienPhatID == id);
            if (phieuThuTienPhat == null)
            {
                return NotFound();
            }

            return View(phieuThuTienPhat);
        }

        // GET: PhieuThuTienPhat/Create
        public IActionResult Create()
        {
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi");
            ViewData["PhieuTraID"] = new SelectList(_context.PhieuTra, "PhieuTraID", "PhieuTraID");
            return View();
        }

        // POST: PhieuThuTienPhat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhieuThuTienPhatID,TienCoc,TienPhat,TienConLai,DocGiaID,PhieuTraID")] PhieuThuTienPhat phieuThuTienPhat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuThuTienPhat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi", phieuThuTienPhat.DocGiaID);
            ViewData["PhieuTraID"] = new SelectList(_context.PhieuTra, "PhieuTraID", "PhieuTraID", phieuThuTienPhat.PhieuTraID);
            return View(phieuThuTienPhat);
        }

        // GET: PhieuThuTienPhat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuThuTienPhat = await _context.PhieuThuTienPhat.FindAsync(id);
            if (phieuThuTienPhat == null)
            {
                return NotFound();
            }
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi", phieuThuTienPhat.DocGiaID);
            ViewData["PhieuTraID"] = new SelectList(_context.PhieuTra, "PhieuTraID", "PhieuTraID", phieuThuTienPhat.PhieuTraID);
            return View(phieuThuTienPhat);
        }

        // POST: PhieuThuTienPhat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhieuThuTienPhatID,TienCoc,TienPhat,TienConLai,DocGiaID,PhieuTraID")] PhieuThuTienPhat phieuThuTienPhat)
        {
            if (id != phieuThuTienPhat.PhieuThuTienPhatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuThuTienPhat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuThuTienPhatExists(phieuThuTienPhat.PhieuThuTienPhatID))
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
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi", phieuThuTienPhat.DocGiaID);
            ViewData["PhieuTraID"] = new SelectList(_context.PhieuTra, "PhieuTraID", "PhieuTraID", phieuThuTienPhat.PhieuTraID);
            return View(phieuThuTienPhat);
        }

        // GET: PhieuThuTienPhat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuThuTienPhat = await _context.PhieuThuTienPhat
                .Include(p => p.DocGia)
                .Include(p => p.PhieuTra)
                .FirstOrDefaultAsync(m => m.PhieuThuTienPhatID == id);
            if (phieuThuTienPhat == null)
            {
                return NotFound();
            }

            return View(phieuThuTienPhat);
        }

        // POST: PhieuThuTienPhat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuThuTienPhat = await _context.PhieuThuTienPhat.FindAsync(id);
            _context.PhieuThuTienPhat.Remove(phieuThuTienPhat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuThuTienPhatExists(int id)
        {
            return _context.PhieuThuTienPhat.Any(e => e.PhieuThuTienPhatID == id);
        }
    }
}
