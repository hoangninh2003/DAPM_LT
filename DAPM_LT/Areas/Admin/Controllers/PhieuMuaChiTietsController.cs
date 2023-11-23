using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAPM_LT.Models;

namespace DAPM_LT.Areas.Admin.Controllers
{
    public class PhieuMuaChiTietsController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/PhieuMuaChiTiets
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                var phieuMuaChiTiets = db.PhieuMuaChiTiets.Include(p => p.Kiemsoat).Include(p => p.PhieuMua);
                return View(phieuMuaChiTiets.ToList());
            }
            else
            {

                TempData["Idtruyentiep"] = id;

                var phieuMuonChiTiets = db.PhieuMuaChiTiets.Include(p => p.Kiemsoat).Include(p => p.PhieuMua).Where(k => k.IdPhieuMua == id);
                return View(phieuMuonChiTiets.ToList());
            }
           
        }

        // GET: Admin/PhieuMuaChiTiets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuaChiTiet phieuMuaChiTiet = db.PhieuMuaChiTiets.Find(id);
            if (phieuMuaChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuaChiTiet);
        }

        // GET: Admin/PhieuMuaChiTiets/Create
        public ActionResult Create()
        {
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Trangthaisach");
            ViewBag.IdPhieuMua = new SelectList(db.PhieuMuas, "Idphieu", "Idphieu");
            return View();
        }

        // POST: Admin/PhieuMuaChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPhieuMuaChiTiet,IdPhieuMua,Idkiemsoat,Tongtien,SoLuong")] PhieuMuaChiTiet phieuMuaChiTiet)
        {
            if (ModelState.IsValid)
            {
                int? idtruyen = TempData["Idtruyentiep"] as int?;
                if (idtruyen != null)
                {
                    phieuMuaChiTiet.IdPhieuMua = idtruyen.Value;
                }
                var kiemsoat = db.Kiemsoats.Find(phieuMuaChiTiet.Idkiemsoat);
                if (kiemsoat != null)
                {
                    if (kiemsoat.Muontra == "Mượn" || kiemsoat.Muontra == "Bán")
                    {

                        ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", phieuMuaChiTiet.Idkiemsoat);
                        ViewBag.IdPhieuMuon = new SelectList(db.PhieuMuons, "Idphieu", "Trangthaimuon", phieuMuaChiTiet.IdPhieuMua);
                        ModelState.AddModelError("", "Sách này đã được mượn. Vui lòng chọn sách khác.");
                        return View(phieuMuaChiTiet);
                    }

                    kiemsoat.Muontra = "Bán";


                    phieuMuaChiTiet.Kiemsoat = kiemsoat;

                    db.PhieuMuaChiTiets.Add(phieuMuaChiTiet);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
          
            }

            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Trangthaisach", phieuMuaChiTiet.Idkiemsoat);
            ViewBag.IdPhieuMua = new SelectList(db.PhieuMuas, "Idphieu", "Idphieu", phieuMuaChiTiet.IdPhieuMua);
            return View(phieuMuaChiTiet);
        }

        // GET: Admin/PhieuMuaChiTiets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuaChiTiet phieuMuaChiTiet = db.PhieuMuaChiTiets.Find(id);
            if (phieuMuaChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Trangthaisach", phieuMuaChiTiet.Idkiemsoat);
            ViewBag.IdPhieuMua = new SelectList(db.PhieuMuas, "Idphieu", "Idphieu", phieuMuaChiTiet.IdPhieuMua);
            return View(phieuMuaChiTiet);
        }

        // POST: Admin/PhieuMuaChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPhieuMuaChiTiet,IdPhieuMua,Idkiemsoat,Tongtien,SoLuong")] PhieuMuaChiTiet phieuMuaChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuMuaChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Trangthaisach", phieuMuaChiTiet.Idkiemsoat);
            ViewBag.IdPhieuMua = new SelectList(db.PhieuMuas, "Idphieu", "Idphieu", phieuMuaChiTiet.IdPhieuMua);
            return View(phieuMuaChiTiet);
        }

        // GET: Admin/PhieuMuaChiTiets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuaChiTiet phieuMuaChiTiet = db.PhieuMuaChiTiets.Find(id);
            if (phieuMuaChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuaChiTiet);
        }

        // POST: Admin/PhieuMuaChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuMuaChiTiet phieuMuaChiTiet = db.PhieuMuaChiTiets.Find(id);
            db.PhieuMuaChiTiets.Remove(phieuMuaChiTiet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
