using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using QuanLyThuVien.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

namespace QuanLyThuVien.Controllers
{
    public class PhieuTraController : Controller
    {
        private readonly DataContext _context;

        public PhieuTraController(DataContext context)
        {
            _context = context;
        }

        public IActionResult KiemTraPhieuTra(int? id)
        {
            PhieuMuon phieumuon = _context.PhieuMuon.FirstOrDefault(l => l.PhieuMuonID == id);
            TimeSpan songaymuon = DateTime.Now.Subtract(phieumuon.NgayMuon);
            if (songaymuon.Days == 0)
            {
                int songaymuon1 = songaymuon.Days + 1;
                ViewBag.songaymuon = songaymuon1;

            }
            else
            {
                int songaymuon1 = songaymuon.Days;
                ViewBag.songaymuon = songaymuon1;

            }
            var ctpm = _context.CT_PhieuMuon.Where(l => l.PhieuMuonID == id).Include(l => l.Sach).ToList();
            var dssach = new List<CT_PhieuMuon>();
            foreach (var item in ctpm)
            {
                dssach.Add(item);
            }
            ViewBag.id = id;
            return View(dssach);
        }

        [HttpPost]
        public IActionResult SubmitKiemTraPhieuTra(List<CT_PhieuMuon> model, int id)
        {
            var phieumuon = _context.PhieuMuon.Find(id);
            var ctpm = _context.CT_PhieuMuon.Where(s => s.PhieuMuonID == id).ToList();

            //tinh so ngay muon
            TimeSpan songaymuon = DateTime.Now.Subtract(phieumuon.NgayMuon);
            if (phieumuon.TinhTrangPhieuMuonID == 2) //2 là phiếu mượn đã trả
            {
                return RedirectToAction("index", "phieumuon");
            }
            else
            {
                //so ngay muon ko qua han
                if (songaymuon.Days <= 4)
                {
                    PhieuTra phieutra = new PhieuTra();
                    phieutra.DocGiaID = phieumuon.DocGiaID;
                    phieutra.NgayTra = DateTime.Now;
                    _context.PhieuTra.Add(phieutra);
                    _context.SaveChanges();

                    foreach (var a in ctpm)
                    {
                        CT_PhieuTra ctpt = new CT_PhieuTra();
                        ctpt.SoNgayMuon = songaymuon.Days;
                        ctpt.PhieuTraID = phieutra.PhieuTraID;
                        ctpt.SachID = a.SachID;
                        _context.CT_PhieuTra.Add(ctpt);
                        _context.SaveChanges();
                    }

                    var docgia = _context.DocGia.FirstOrDefault(l => l.DocGiaID == phieutra.DocGiaID);
                    docgia.SLSachDaMuon = 0;
                    _context.SaveChanges();

                    //cập nhật lại tình trạng phiếu mượn
                    phieumuon.TinhTrangPhieuMuonID = 2;//2 là phiếu mượn hoan thanh
                    _context.SaveChanges();

                    double sum = 0;

#pragma warning disable CS0162 // Unreachable code detected
                    for (int i = 0; i < model.Count; i++)
#pragma warning restore CS0162 // Unreachable code detected
                    {

                        if (model[i].IsSelected)
                        {
                            var sach = _context.Sach.FirstOrDefault(x => x.SachID == model[i].SachID);
                            sach.TinhTrangSachID = 1; //1 chua thue
                            _context.SaveChanges();
                        }

                        else
                        {
                            var sach = _context.Sach.FirstOrDefault(x => x.SachID == model[i].SachID);
                            sach.TinhTrangSachID = 3; //3 mat
                            sum += sach.Gia;
                        }
                      
                    }
                    PhieuThuTienPhat pttp = new PhieuThuTienPhat();
                    pttp.DocGiaID = docgia.DocGiaID;
                    pttp.TienCoc = phieumuon.TienCoc;
                    pttp.PhieuTraID = phieutra.PhieuTraID;
                    pttp.TienPhat = sum;
                    pttp.TienConLai = phieumuon.TienCoc - pttp.TienPhat;
                    _context.PhieuThuTienPhat.Add(pttp);
                    docgia.ConLai = pttp.TienConLai;
                    _context.SaveChanges();
                    return RedirectToAction("details", new RouteValueDictionary(new { Controller = "phieuthutienphat", Action = "details", id = pttp.PhieuThuTienPhatID }));
                }
                //so ngay muon qua han
                else
                {
                    PhieuTra phieutra = new PhieuTra();
                    phieutra.DocGiaID = phieumuon.DocGiaID;
                    phieutra.NgayTra = DateTime.Now;
                    _context.PhieuTra.Add(phieutra);
                    _context.SaveChanges();

                    // tien tra tre han
                    double sum = 0;
                    sum = (songaymuon.Days - 4) * 1000;


                    foreach (var a in ctpm)
                    {
                        CT_PhieuTra ctpt = new CT_PhieuTra();
                        ctpt.SoNgayMuon = songaymuon.Days;
                        ctpt.PhieuTraID = phieutra.PhieuTraID;
                        ctpt.SachID = a.SachID;
                        _context.CT_PhieuTra.Add(ctpt);
                        _context.SaveChanges();
                    }

                    var docgia = _context.DocGia.FirstOrDefault(l => l.DocGiaID == phieutra.DocGiaID);
                    docgia.SLSachDaMuon = 0;
                    _context.SaveChanges();

                    //cập nhật lại tình trạng phiếu mượn
                    phieumuon.TinhTrangPhieuMuonID = 2;//2 là phiếu mượn hoan thanh
                    _context.SaveChanges();

                    for (int i = 0; i < model.Count; i++)
                    {
                        // trễ hạn nhưng đủ sách
                        if (model[i].IsSelected == true)
                        {
                            var sach = _context.Sach.FirstOrDefault(x => x.SachID == model[i].SachID);
                            sach.TinhTrangSachID = 1; //1 chua thue
                            _context.SaveChanges();
                        }
                        // trễ hạn nhưng thiếu sách
                        else
                        {
                            var sach = _context.Sach.FirstOrDefault(x => x.SachID == model[i].SachID);
                            sach.TinhTrangSachID = 3; //3 mat
                            sum += sach.Gia;
                        }
                      
                    }
                    PhieuThuTienPhat pttp = new PhieuThuTienPhat();
                    pttp.DocGiaID = docgia.DocGiaID;
                    pttp.PhieuTraID = phieutra.PhieuTraID;
                    pttp.TienCoc = phieumuon.TienCoc;
                    pttp.TienPhat = sum;
                    pttp.TienConLai = phieumuon.TienCoc - pttp.TienPhat;
                    _context.PhieuThuTienPhat.Add(pttp);
                    _context.SaveChanges();

                    return RedirectToAction("details", new RouteValueDictionary(new { Controller = "phieuthutienphat", Action = "details", id = pttp.PhieuThuTienPhatID }));
                }
            }
           
        }

    }
}