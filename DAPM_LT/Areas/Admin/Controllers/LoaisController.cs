using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAPM_LT.Facade;
using DAPM_LT.Models;

namespace DAPM_LT.Areas.Admin.Controllers
{
    public class LoaisController : Controller
    {
        private QuanLyLoaiFacade quanLyLoaiFacade = new QuanLyLoaiFacade();

        // GET: Admin/Loais
        public ActionResult Index()
        {
            return View(quanLyLoaiFacade.LayTatCaLoai());
        }

        // GET: Admin/Loais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai loai = quanLyLoaiFacade.TimLoaiTheoId(id.Value);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        // GET: Admin/Loais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Loais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idloai,Tenloai")] Loai loai)
        {
            if (ModelState.IsValid)
            {
                quanLyLoaiFacade.ThemLoai(loai);
                return RedirectToAction("Index");
            }

            return View(loai);
        }

        // GET: Admin/Loais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai loai = quanLyLoaiFacade.TimLoaiTheoId(id.Value);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        // POST: Admin/Loais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Idloai,Tenloai")] Loai loai)
        {
            if (ModelState.IsValid)
            {
                quanLyLoaiFacade.CapNhatLoai(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        // GET: Admin/Loais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loai loai = quanLyLoaiFacade.TimLoaiTheoId(id.Value);
            if (loai == null)
            {
                return HttpNotFound();
            }
            return View(loai);
        }

        // POST: Admin/Loais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            quanLyLoaiFacade.XoaLoai(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                quanLyLoaiFacade.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
