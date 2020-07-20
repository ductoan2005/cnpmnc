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
    public class CT_PhieuMuonController : Controller
    {
        private readonly DataContext _context;

        public CT_PhieuMuonController(DataContext context)
        {
            _context = context;
        }

        // GET: CT_PhieuMuon
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.CT_PhieuMuon.Include(c => c.Cart).Include(c => c.PhieuMuon).Include(c => c.Sach);
            return View(await dataContext.ToListAsync());
        }



        public IActionResult DetailsPhieuMuon(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phieuMuon = _context.CT_PhieuMuon.FirstOrDefault(m=>m.PhieuMuonID==id);
            if (phieuMuon == null)
            {
                return NotFound();
            }

            return View(phieuMuon);
        }

        // GET: CT_PhieuMuon/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cT_PhieuMuon = await _context.CT_PhieuMuon
        //        .Include(c => c.Cart)
        //        .Include(c => c.PhieuMuon)
        //        .Include(c => c.Sach)
        //        .FirstOrDefaultAsync(m => m.CT_PhieuMuonID == id);
        //    if (cT_PhieuMuon == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cT_PhieuMuon);
        //}

        // GET: CT_PhieuMuon/Create
        public IActionResult Create()
        {
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID");
            ViewData["PhieuMuonID"] = new SelectList(_context.PhieuMuon, "PhieuMuonID", "PhieuMuonID");
            ViewData["SachID"] = new SelectList(_context.Sach, "SachID", "NhaXB");
            return View();
        }

        // POST: CT_PhieuMuon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CT_PhieuMuonID,SachID,CartID,PhieuMuonID")] CT_PhieuMuon cT_PhieuMuon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cT_PhieuMuon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", cT_PhieuMuon.CartID);
            ViewData["PhieuMuonID"] = new SelectList(_context.PhieuMuon, "PhieuMuonID", "PhieuMuonID", cT_PhieuMuon.PhieuMuonID);
            ViewData["SachID"] = new SelectList(_context.Sach, "SachID", "NhaXB", cT_PhieuMuon.SachID);
            return View(cT_PhieuMuon);
        }

        // GET: CT_PhieuMuon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cT_PhieuMuon = await _context.CT_PhieuMuon.FindAsync(id);
            if (cT_PhieuMuon == null)
            {
                return NotFound();
            }
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", cT_PhieuMuon.CartID);
            ViewData["PhieuMuonID"] = new SelectList(_context.PhieuMuon, "PhieuMuonID", "PhieuMuonID", cT_PhieuMuon.PhieuMuonID);
            ViewData["SachID"] = new SelectList(_context.Sach, "SachID", "NhaXB", cT_PhieuMuon.SachID);
            return View(cT_PhieuMuon);
        }

        // POST: CT_PhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CT_PhieuMuonID,SachID,CartID,PhieuMuonID")] CT_PhieuMuon cT_PhieuMuon)
        {
            if (id != cT_PhieuMuon.CT_PhieuMuonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cT_PhieuMuon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CT_PhieuMuonExists(cT_PhieuMuon.CT_PhieuMuonID))
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
            ViewData["CartID"] = new SelectList(_context.Cart, "CartID", "CartID", cT_PhieuMuon.CartID);
            ViewData["PhieuMuonID"] = new SelectList(_context.PhieuMuon, "PhieuMuonID", "PhieuMuonID", cT_PhieuMuon.PhieuMuonID);
            ViewData["SachID"] = new SelectList(_context.Sach, "SachID", "NhaXB", cT_PhieuMuon.SachID);
            return View(cT_PhieuMuon);
        }

        // GET: CT_PhieuMuon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cT_PhieuMuon = await _context.CT_PhieuMuon
                .Include(c => c.Cart)
                .Include(c => c.PhieuMuon)
                .Include(c => c.Sach)
                .FirstOrDefaultAsync(m => m.CT_PhieuMuonID == id);
            if (cT_PhieuMuon == null)
            {
                return NotFound();
            }

            return View(cT_PhieuMuon);
        }

        // POST: CT_PhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cT_PhieuMuon = await _context.CT_PhieuMuon.FindAsync(id);
            _context.CT_PhieuMuon.Remove(cT_PhieuMuon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CT_PhieuMuonExists(int id)
        {
            return _context.CT_PhieuMuon.Any(e => e.CT_PhieuMuonID == id);
        }
    }
}
