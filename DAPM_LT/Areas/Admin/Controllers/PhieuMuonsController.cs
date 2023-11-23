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
    public class PhieuMuonsController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/PhieuMuons
        public ActionResult Index(string _name, int? _id)
        {
            IQueryable<PhieuMuon> query = db.PhieuMuons;
            if (_id != null)
            {
                query = query.Where(i => i.The.Idthe == _id);
            }
           

            if (!string.IsNullOrEmpty(_name))
            {
                query = query.Where(s => s.The.TaiKhoan.Ten.Contains(_name));
            }

            List<PhieuMuon> sachList = query.ToList();
           
            return View(sachList);
        }
       

        // GET: Admin/PhieuMuons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // GET: Admin/PhieuMuons/Create
        public ActionResult Create()
        {
            ViewBag.Idthe = new SelectList(db.Thes, "Idthe", "Idthe");
            return View();
        }

        // POST: Admin/PhieuMuons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idphieu,Idthe,Ngaymuon,Ngaytra,Trangthaimuon")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra mã thẻ tồn tại
                var the = db.Thes.Find(phieuMuon.Idthe);
                if (the == null)
                {
                    // trả về thông báo lỗi
                    ModelState.AddModelError("Idthe", "Mã thẻ không tồn tại");
                    ViewBag.Idthe = new SelectList(db.Thes, "Idthe", "Idthe", phieuMuon.Idthe);
                    return View(phieuMuon);
                }

                
                db.PhieuMuons.Add(phieuMuon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //hiển thị lại form với thông báo lỗi
            ViewBag.Idthe = new SelectList(db.Thes, "Idthe", "Idthe", phieuMuon.Idthe);
            return View(phieuMuon);
        }


        // GET: Admin/PhieuMuons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.Idthe = new SelectList(db.Thes, "Idthe", "Idthe", phieuMuon.Idthe);
            return View(phieuMuon);
        }

        // POST: Admin/PhieuMuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idphieu,Idsach,Idthe,Ngaymuon,Ngaytra,Trangthaimuon")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuMuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Idthe = new SelectList(db.Thes, "Idthe", "Idthe", phieuMuon.Idthe);
            return View(phieuMuon);
        }

        // GET: Admin/PhieuMuons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // POST: Admin/PhieuMuons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            db.PhieuMuons.Remove(phieuMuon);
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
