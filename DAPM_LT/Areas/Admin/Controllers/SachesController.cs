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
    public class SachesController : Controller
    {
        private dapmEntities db = new dapmEntities();

        public ActionResult Index(string _name, string _category, string _author)
        {
            // Lấy danh sách sách từ cơ sở dữ liệu
            var sachList = db.Saches.Include(s => s.Loai);

            // Áp dụng các điều kiện tìm kiếm
            if (!string.IsNullOrEmpty(_name))
            {
                sachList = sachList.Where(s => s.Tieude.Contains(_name));
            }

            if (!string.IsNullOrEmpty(_category))
            {
                sachList = sachList.Where(s => s.Loai.Tenloai == _category);
            }

            if (!string.IsNullOrEmpty(_author))
            {
                sachList = sachList.Where(s => s.Tacgia == _author);
            }

           
            var dsl = db.Loais.Select(l => l.Tenloai).Distinct().ToList();
            ViewBag.Categories = dsl;

            
            var tacGiaList = sachList.Select(s => s.Tacgia).Distinct().ToList();
            ViewBag.Tacgia = tacGiaList;

            return View(sachList.ToList());
        }


        // GET: Admin/Saches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Admin/Saches1/Create
        public ActionResult Create()
        {
            ViewBag.Idloai = new SelectList(db.Loais, "Idloai", "Tenloai");
            return View();
        }

        // POST: Admin/Saches1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idsach,Tieude,ImgSach,Tacgia,GiaMua,Namxuatban,Mota,Idloai")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Idloai = new SelectList(db.Loais, "Idloai", "Tenloai", sach.Idloai);
            return View(sach);
        }

        // GET: Admin/Saches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idloai = new SelectList(db.Loais, "Idloai", "Tenloai", sach.Idloai);
            return View(sach);
        }

        // POST: Admin/Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idsach,Tieude,ImgSach,Tacgia,GiaMua,Namxuatban,Mota,Idloai")] Sach sach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idloai = new SelectList(db.Loais, "Idloai", "Tenloai", sach.Idloai);
            return View(sach);
        }

        // GET: Admin/Saches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // POST: Admin/Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
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
