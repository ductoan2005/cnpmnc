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
    public class DocGiaController : Controller
    {
        private readonly DataContext _context;

        public DocGiaController(DataContext context)
        {
            _context = context;
        }

        // GET: DocGia
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.DocGia.Include(d => d.LoaiDocGia);
            return View(await dataContext.ToListAsync());
        }

        // GET: DocGia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGia
                .Include(d => d.LoaiDocGia)
                .FirstOrDefaultAsync(m => m.DocGiaID == id);
            if (docGia == null)
            {
                return NotFound();
            }

            return View(docGia);
        }

        // GET: DocGia/Create
        public IActionResult Create()
        {
            ViewData["LoaiDocGiaID"] = new SelectList(_context.LoaiDocGia, "LoaiDocGiaID", "TenDocGia");
            return View();
        }

        // POST: DocGia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DocGiaID,TenDocGia,Email,NgaySinh,DiaChi,NgayLapThe,SLSachDaMuon,NgayHetHan,LoaiDocGiaID")] DocGia docGia)
        {
            if (ModelState.IsValid)
            {
                docGia.NgayHetHan = docGia.NgayLapThe.AddMonths(8);
                docGia.SLSachDaMuon = 0;
                _context.Add(docGia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoaiDocGiaID"] = new SelectList(_context.LoaiDocGia, "LoaiDocGiaID", "TenDocGia", docGia.LoaiDocGiaID);
            return View(docGia);
        }

        // GET: DocGia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGia.FindAsync(id);
            if (docGia == null)
            {
                return NotFound();
            }
            ViewData["LoaiDocGiaID"] = new SelectList(_context.LoaiDocGia, "LoaiDocGiaID", "TenDocGia", docGia.LoaiDocGiaID);
            return View(docGia);
        }

        // POST: DocGia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DocGiaID,TenDocGia,Email,NgaySinh,DiaChi,NgayLapThe,SLSachDaMuon,NgayHetHan,LoaiDocGiaID")] DocGia docGia)
        {
            if (id != docGia.DocGiaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(docGia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocGiaExists(docGia.DocGiaID))
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
            ViewData["LoaiDocGiaID"] = new SelectList(_context.LoaiDocGia, "LoaiDocGiaID", "TenDocGia", docGia.LoaiDocGiaID);
            return View(docGia);
        }

        // GET: DocGia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var docGia = await _context.DocGia
                .Include(d => d.LoaiDocGia)
                .FirstOrDefaultAsync(m => m.DocGiaID == id);
            if (docGia == null)
            {
                return NotFound();
            }

            return View(docGia);
        }

        // POST: DocGia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var docGia = await _context.DocGia.FindAsync(id);
            _context.DocGia.Remove(docGia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocGiaExists(int id)
        {
            return _context.DocGia.Any(e => e.DocGiaID == id);
        }
        public IActionResult LentBook()
        {

            return View();
        }
    }
}
