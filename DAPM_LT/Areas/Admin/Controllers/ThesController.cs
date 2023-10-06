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
    public class ThesController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/Thes
        public ActionResult Index()
        {
            var thes = db.Thes.Include(t => t.TaiKhoan);
            return View(thes.ToList());
        }

        // GET: Admin/Thes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            The the = db.Thes.Find(id);
            if (the == null)
            {
                return HttpNotFound();
            }
            return View(the);
        }

        // GET: Admin/Thes/Create
        public ActionResult Create()
        {
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot");
            return View();
        }

        // POST: Admin/Thes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idthe,Idtaikhoan,Ngaymothe,Trangthaithe")] The the)
        {
            if (ModelState.IsValid)
            {
                db.Thes.Add(the);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", the.Idtaikhoan);
            return View(the);
        }

        // GET: Admin/Thes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            The the = db.Thes.Find(id);
            if (the == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", the.Idtaikhoan);
            return View(the);
        }

        // POST: Admin/Thes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idthe,Idtaikhoan,Ngaymothe,Trangthaithe")] The the)
        {
            if (ModelState.IsValid)
            {
                db.Entry(the).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", the.Idtaikhoan);
            return View(the);
        }

        // GET: Admin/Thes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            The the = db.Thes.Find(id);
            if (the == null)
            {
                return HttpNotFound();
            }
            return View(the);
        }

        // POST: Admin/Thes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            The the = db.Thes.Find(id);
            db.Thes.Remove(the);
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
