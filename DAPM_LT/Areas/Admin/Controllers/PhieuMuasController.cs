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
    public class PhieuMuasController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/PhieuMuas
        public ActionResult Index()
        {
            var phieuMuas = db.PhieuMuas.Include(p => p.TaiKhoan);
            return View(phieuMuas.ToList());
        }

        // GET: Admin/PhieuMuas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMua phieuMua = db.PhieuMuas.Find(id);
            if (phieuMua == null)
            {
                return HttpNotFound();
            }
            return View(phieuMua);
        }

        // GET: Admin/PhieuMuas/Create
        public ActionResult Create()
        {
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot");
            return View();
        }

        // POST: Admin/PhieuMuas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idphieu,Idtaikhoan,Ngaymua")] PhieuMua phieuMua)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mã thẻ tồn tại
                var the = db.Thes.Find(phieuMua.Idtaikhoan);
                if (the == null)
                {
                    // trả về thông báo lỗi
                    ModelState.AddModelError("Idtaikhoan", "Mã tài khoản không tồn tại");
                    ViewBag.Idthe = new SelectList(db.TaiKhoans, "Idtaikhoan", "Idtaikhoan", phieuMua.Idtaikhoan);
                    return View(phieuMua);
                }

                db.PhieuMuas.Add(phieuMua);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", phieuMua.Idtaikhoan);
            return View(phieuMua);
        }

        // GET: Admin/PhieuMuas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMua phieuMua = db.PhieuMuas.Find(id);
            if (phieuMua == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", phieuMua.Idtaikhoan);
            return View(phieuMua);
        }

        // POST: Admin/PhieuMuas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idphieu,Idsach,Idtaikhoan,Ngaymua")] PhieuMua phieuMua)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuMua).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idtaikhoan = new SelectList(db.TaiKhoans, "Idtaikhoan", "Holot", phieuMua.Idtaikhoan);
            return View(phieuMua);
        }

        // GET: Admin/PhieuMuas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMua phieuMua = db.PhieuMuas.Find(id);
            if (phieuMua == null)
            {
                return HttpNotFound();
            }
            return View(phieuMua);
        }

        // POST: Admin/PhieuMuas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuMua phieuMua = db.PhieuMuas.Find(id);
            db.PhieuMuas.Remove(phieuMua);
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
