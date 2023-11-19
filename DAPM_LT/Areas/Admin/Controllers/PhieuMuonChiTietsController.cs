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
    public class PhieuMuonChiTietsController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/PhieuMuonChiTiets
        public ActionResult Index(int? id)
        {
            if (id == null)
            {

                var phieuMuonChiTiets = db.PhieuMuonChiTiets.Include(p => p.Kiemsoat).Include(p => p.PhieuMuon);
                return View(phieuMuonChiTiets.ToList());
            }
            else
            {
                var phieuMuonChiTiets = db.PhieuMuonChiTiets.Include(p => p.Kiemsoat).Include(p => p.PhieuMuon).Where(k => k.IdPhieuMuon == id);
                
                return View(phieuMuonChiTiets.ToList());
            }

        }

        // GET: Admin/PhieuMuonChiTiets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuonChiTiet phieuMuonChiTiet = db.PhieuMuonChiTiets.Find(id);
            if (phieuMuonChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuonChiTiet);
        }

        // GET: Admin/PhieuMuonChiTiets/Create
        public ActionResult Create()
        {
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai");
            ViewBag.IdPhieuMuon = new SelectList(db.PhieuMuons, "Idphieu", "Trangthaimuon");
            return View();
        }

        // POST: Admin/PhieuMuonChiTiets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPhieuMuonChiTiet,IdPhieuMuon,Idkiemsoat,TrangThaiSach")] PhieuMuonChiTiet phieuMuonChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.PhieuMuonChiTiets.Add(phieuMuonChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", phieuMuonChiTiet.Idkiemsoat);
            ViewBag.IdPhieuMuon = new SelectList(db.PhieuMuons, "Idphieu", "Trangthaimuon", phieuMuonChiTiet.IdPhieuMuon);
            return View(phieuMuonChiTiet);
        }

        // GET: Admin/PhieuMuonChiTiets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuonChiTiet phieuMuonChiTiet = db.PhieuMuonChiTiets.Find(id);
            if (phieuMuonChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", phieuMuonChiTiet.Idkiemsoat);
            ViewBag.IdPhieuMuon = new SelectList(db.PhieuMuons, "Idphieu", "Trangthaimuon", phieuMuonChiTiet.IdPhieuMuon);
            return View(phieuMuonChiTiet);
        }

        // POST: Admin/PhieuMuonChiTiets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPhieuMuonChiTiet,IdPhieuMuon,Idkiemsoat,TrangThaiSach")] PhieuMuonChiTiet phieuMuonChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuMuonChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", phieuMuonChiTiet.Idkiemsoat);
            ViewBag.IdPhieuMuon = new SelectList(db.PhieuMuons, "Idphieu", "Trangthaimuon", phieuMuonChiTiet.IdPhieuMuon);
            return View(phieuMuonChiTiet);
        }

        // GET: Admin/PhieuMuonChiTiets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuonChiTiet phieuMuonChiTiet = db.PhieuMuonChiTiets.Find(id);
            if (phieuMuonChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuonChiTiet);
        }

        // POST: Admin/PhieuMuonChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuMuonChiTiet phieuMuonChiTiet = db.PhieuMuonChiTiets.Find(id);
            db.PhieuMuonChiTiets.Remove(phieuMuonChiTiet);
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
