using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAPM_LT.Models;
using PagedList.Mvc;
using PagedList;


namespace DAPM_LT.Areas.Admin.Controllers
{
    public class TaiKhoansController : Controller
    {
        private dapmEntities db = new dapmEntities();

        // GET: Admin/TaiKhoans
        public ActionResult Index()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.PhanQuyen);
            return View(taiKhoans.ToList());
        }

        // GET: Admin/TaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Idtaikhoan,Holot,Ten,Email,Dienthoai,Matkhau,ImgU,IDQuyen,Diachi")] TaiKhoan taiKhoan)
        {
            db.TaiKhoans.Add(taiKhoan);
            if (ModelState.IsValid)
            {
                // Kiểm tra có thay đổi ảnh không
                bool hasImageChanged = taiKhoan.ImageFile != null && taiKhoan.ImageFile.ContentLength > 0;

                // Lấy đối tượng từ database để kiểm tra và giữ lại ảnh cũ (nếu có)
                TaiKhoan existingTaiKhoan = db.TaiKhoans.Find(taiKhoan.Idtaikhoan);

                if (existingTaiKhoan != null)
                {
                    // Giữ lại ảnh cũ nếu không có sự thay đổi
                    if (!hasImageChanged)
                    {
                        taiKhoan.ImgU = existingTaiKhoan.ImgU;
                    }

                    // Cập nhật chỉ các trường cần thiết từ taiKhoan
                    db.Entry(existingTaiKhoan).CurrentValues.SetValues(taiKhoan);

                    // Nếu có thay đổi ảnh, cập nhật ảnh
                    if (hasImageChanged)
                    {
                        byte[] imageData;
                        using (Stream inputStream = taiKhoan.ImageFile.InputStream)
                        {
                            using (MemoryStream outputStream = new MemoryStream())
                            {
                                inputStream.CopyTo(outputStream);
                                imageData = outputStream.ToArray();
                            }
                        }
                        existingTaiKhoan.ImgU = imageData;
                    }

                    db.SaveChanges();

                    // Cập nhật lại session cho profile
                    //Session["use"] = existingTaiKhoan;

                    return RedirectToAction("Index", new { id = existingTaiKhoan.Idtaikhoan });
                }
            }

            // ModelState không hợp lệ hoặc có lỗi xảy ra, trả về view với dữ liệu hiện tại
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.Idtaikhoan);
            
            return View(taiKhoan);
        }
            // GET: Admin/TaiKhoans/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra có thay đổi ảnh không
                bool hasImageChanged = taiKhoan.ImageFile != null && taiKhoan.ImageFile.ContentLength > 0;

                // Lấy đối tượng từ database để kiểm tra và giữ lại ảnh cũ (nếu có)
                TaiKhoan existingTaiKhoan = db.TaiKhoans.Find(taiKhoan.Idtaikhoan);

                if (existingTaiKhoan != null)
                {
                    // Giữ lại ảnh cũ nếu không có sự thay đổi
                    if (!hasImageChanged)
                    {
                        taiKhoan.ImgU = existingTaiKhoan.ImgU;
                    }

                    // Cập nhật chỉ các trường cần thiết từ taiKhoan
                    db.Entry(existingTaiKhoan).CurrentValues.SetValues(taiKhoan);

                    // Nếu có thay đổi ảnh, cập nhật ảnh
                    if (hasImageChanged)
                    {
                        byte[] imageData;
                        using (Stream inputStream = taiKhoan.ImageFile.InputStream)
                        {
                            using (MemoryStream outputStream = new MemoryStream())
                            {
                                inputStream.CopyTo(outputStream);
                                imageData = outputStream.ToArray();
                            }
                        }
                        existingTaiKhoan.ImgU = imageData;
                    }
                  
                    db.SaveChanges();

                    // Cập nhật lại session cho profile
                    Session["use"] = existingTaiKhoan;

                    return RedirectToAction("Details", new { id = existingTaiKhoan.Idtaikhoan });
                }
            }

            // ModelState không hợp lệ hoặc có lỗi xảy ra, trả về view với dữ liệu hiện tại
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
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
