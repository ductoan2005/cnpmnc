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
    public class PhieuMuonController : Controller
    {
        private readonly DataContext _context;

        public PhieuMuonController(DataContext context)
        {
            _context = context;
        }

        // GET: PhieuMuon
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PhieuMuon.Include(p => p.DocGia).Include(p => p.TinhTrangPhieuMuon);
            return View(await dataContext.ToListAsync());
        }

        // GET: PhieuMuon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuon
                .Include(p => p.DocGia)
                .Include(p => p.TinhTrangPhieuMuon)
                .FirstOrDefaultAsync(m => m.PhieuMuonID == id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // GET: PhieuMuon/Create
        public IActionResult Create()
        {
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi");
            ViewData["TinhTrangPhieuMuonID"] = new SelectList(_context.TinhTrangPhieuMuon, "TinhTrangPhieuMuonID", "TinhTrangPhieuMuonID");
            return View();
        }

        // POST: PhieuMuon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhieuMuonID,NgayMuon,TinhTrangPhieuMuonID,TienCoc,DocGiaID")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuMuon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi", phieuMuon.DocGiaID);
            ViewData["TinhTrangPhieuMuonID"] = new SelectList(_context.TinhTrangPhieuMuon, "TinhTrangPhieuMuonID", "TinhTrangPhieuMuonID", phieuMuon.TinhTrangPhieuMuonID);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuon.FindAsync(id);
            if (phieuMuon == null)
            {
                return NotFound();
            }
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi", phieuMuon.DocGiaID);
            ViewData["TinhTrangPhieuMuonID"] = new SelectList(_context.TinhTrangPhieuMuon, "TinhTrangPhieuMuonID", "TinhTrangPhieuMuonID", phieuMuon.TinhTrangPhieuMuonID);
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhieuMuonID,NgayMuon,TinhTrangPhieuMuonID,TienCoc,DocGiaID")] PhieuMuon phieuMuon)
        {
            if (id != phieuMuon.PhieuMuonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuMuon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuMuonExists(phieuMuon.PhieuMuonID))
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
            ViewData["DocGiaID"] = new SelectList(_context.DocGia, "DocGiaID", "DiaChi", phieuMuon.DocGiaID);
            ViewData["TinhTrangPhieuMuonID"] = new SelectList(_context.TinhTrangPhieuMuon, "TinhTrangPhieuMuonID", "TinhTrangPhieuMuonID", phieuMuon.TinhTrangPhieuMuonID);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = await _context.PhieuMuon
                .Include(p => p.DocGia)
                .Include(p => p.TinhTrangPhieuMuon)
                .FirstOrDefaultAsync(m => m.PhieuMuonID == id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // POST: PhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phieuMuon = await _context.PhieuMuon.FindAsync(id);
            _context.PhieuMuon.Remove(phieuMuon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuMuonExists(int id)
        {
            return _context.PhieuMuon.Any(e => e.PhieuMuonID == id);
        }
    }
}
