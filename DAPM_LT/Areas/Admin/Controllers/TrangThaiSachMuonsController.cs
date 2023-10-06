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
    public class TrangThaiSachMuonsController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/TrangThaiSachMuons
        public ActionResult Index()
        {
            var trangThaiSachMuons = db.TrangThaiSachMuons.Include(t => t.Kiemsoat).Include(t => t.Sach);
            return View(trangThaiSachMuons.ToList());
        }

        // GET: Admin/TrangThaiSachMuons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiSachMuon trangThaiSachMuon = db.TrangThaiSachMuons.Find(id);
            if (trangThaiSachMuon == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiSachMuon);
        }

        // GET: Admin/TrangThaiSachMuons/Create
        public ActionResult Create()
        {
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai");
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude");
            return View();
        }

        // POST: Admin/TrangThaiSachMuons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idtrangthaisachmuon,Idsach,Idkiemsoat,Idphieu,Trangthaism")] TrangThaiSachMuon trangThaiSachMuon)
        {
            if (ModelState.IsValid)
            {
                db.TrangThaiSachMuons.Add(trangThaiSachMuon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", trangThaiSachMuon.Idkiemsoat);
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude", trangThaiSachMuon.Idsach);
            return View(trangThaiSachMuon);
        }

        // GET: Admin/TrangThaiSachMuons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiSachMuon trangThaiSachMuon = db.TrangThaiSachMuons.Find(id);
            if (trangThaiSachMuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", trangThaiSachMuon.Idkiemsoat);
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude", trangThaiSachMuon.Idsach);
            return View(trangThaiSachMuon);
        }

        // POST: Admin/TrangThaiSachMuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idtrangthaisachmuon,Idsach,Idkiemsoat,Idphieu,Trangthaism")] TrangThaiSachMuon trangThaiSachMuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trangThaiSachMuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idkiemsoat = new SelectList(db.Kiemsoats, "Idkiemsoat", "Imgtrangthai", trangThaiSachMuon.Idkiemsoat);
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude", trangThaiSachMuon.Idsach);
            return View(trangThaiSachMuon);
        }

        // GET: Admin/TrangThaiSachMuons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangThaiSachMuon trangThaiSachMuon = db.TrangThaiSachMuons.Find(id);
            if (trangThaiSachMuon == null)
            {
                return HttpNotFound();
            }
            return View(trangThaiSachMuon);
        }

        // POST: Admin/TrangThaiSachMuons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrangThaiSachMuon trangThaiSachMuon = db.TrangThaiSachMuons.Find(id);
            db.TrangThaiSachMuons.Remove(trangThaiSachMuon);
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
