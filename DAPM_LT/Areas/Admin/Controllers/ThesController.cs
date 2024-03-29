using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAPM_LT.Models;
using DAPM_LT.State;

namespace DAPM_LT.Areas.Admin.Controllers
{
    public class ThesController : Controller
    {
        private dapmEntities db = new dapmEntities();
        private ThesState state = new ThesState();

        // GET: Admin/Thes
        public ActionResult Index()
        {
            state.SetState("Index");
            var thes = db.Thes.Include(t => t.TaiKhoan);
            ViewBag.Controller = state;
            return View(thes.ToList());
        }

        // GET: Admin/Thes/Details/5
        public ActionResult Details(int? id)
        {
            state.SetState("Details");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            The the = db.Thes.Find(id);
            if (the == null)
            {
                return HttpNotFound();
            }
            ViewBag.Controller = state;
            return View(the);
        }

        // GET: Admin/Thes/Create
        public ActionResult Create()
        {
            state.SetState("Create");
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot");
            ViewBag.Controller = state;
            return View();
        }

        // POST: Admin/Thes/Create
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
            state.SetState("Create");
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", the.Idtaikhoan);
            ViewBag.Controller = state;
            return View(the);
        }

        // GET: Admin/Thes/Edit/5
        public ActionResult Edit(int? id)
        {
            state.SetState("Edit");
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
            ViewBag.Controller = state;
            return View(the);
        }

        // POST: Admin/Thes/Edit/5
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
            state.SetState("Edit");
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", the.Idtaikhoan);
            ViewBag.Controller = state;
            return View(the);
        }

        // GET: Admin/Thes/Delete/5
        public ActionResult Delete(int? id)
        {
            state.SetState("Delete");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            The the = db.Thes.Find(id);
            if (the == null)
            {
                return HttpNotFound();
            }
            ViewBag.Controller = state;
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
