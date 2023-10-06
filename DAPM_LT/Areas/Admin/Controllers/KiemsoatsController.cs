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
    public class KiemsoatsController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/Kiemsoats
        public ActionResult Index()
        {
            var kiemsoats = db.Kiemsoats.Include(k => k.Sach);
            return View(kiemsoats.ToList());
        }

        // GET: Admin/Kiemsoats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kiemsoat kiemsoat = db.Kiemsoats.Find(id);
            if (kiemsoat == null)
            {
                return HttpNotFound();
            }
            return View(kiemsoat);
        }

        // GET: Admin/Kiemsoats/Create
        public ActionResult Create()
        {
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude");
            return View();
        }

        // POST: Admin/Kiemsoats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idkiemsoat,Imgtrangthai,Trangthaisach,Idsach")] Kiemsoat kiemsoat)
        {
            if (ModelState.IsValid)
            {
                db.Kiemsoats.Add(kiemsoat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude", kiemsoat.Idsach);
            return View(kiemsoat);
        }

        // GET: Admin/Kiemsoats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kiemsoat kiemsoat = db.Kiemsoats.Find(id);
            if (kiemsoat == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude", kiemsoat.Idsach);
            return View(kiemsoat);
        }

        // POST: Admin/Kiemsoats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idkiemsoat,Imgtrangthai,Trangthaisach,Idsach")] Kiemsoat kiemsoat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kiemsoat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idsach = new SelectList(db.Saches, "Idsach", "Tieude", kiemsoat.Idsach);
            return View(kiemsoat);
        }

        // GET: Admin/Kiemsoats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kiemsoat kiemsoat = db.Kiemsoats.Find(id);
            if (kiemsoat == null)
            {
                return HttpNotFound();
            }
            return View(kiemsoat);
        }

        // POST: Admin/Kiemsoats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kiemsoat kiemsoat = db.Kiemsoats.Find(id);
            db.Kiemsoats.Remove(kiemsoat);
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
