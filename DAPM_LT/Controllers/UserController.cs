using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ThuVien.Controllers
{
    public class UserController : Controller
    {
        dapmEntities db = new dapmEntities();

        // ĐĂNG KÝ
        public ActionResult Dangky()
        {
            return View();
        }


        // ĐĂNG KÝ PHƯƠNG THỨC POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangky(TaiKhoan taiKhoan)
        {
            try
            {
                Session["userReg"] = taiKhoan;

                // Nếu dữ liệu đúng thì trả về trang đăng nhập
                if (ModelState.IsValid)
                {
                    var check = db.TaiKhoans.FirstOrDefault(s => s.Email == taiKhoan.Email);
                    if (check == null)
                    {
                        // Kiểm tra tồn tạo member chưa (lấy id nó luôn)
                        var memberRole = db.PhanQuyens.FirstOrDefault(pq => pq.TenQuyen == "Member");

                        // Nếu quyền không tồn tại tạo mưới
                        if (memberRole == null)
                        {
                            memberRole = new PhanQuyen { TenQuyen = "Member" };
                            db.PhanQuyens.Add(memberRole);
                        }
                        taiKhoan.PhanQuyen = memberRole;
                        //// Set quyền mặc định cho tài khoản mới(dùng tên luôn)
                        //taiKhoan.PhanQuyen = new PhanQuyen { IDQuyen = 2 };

                        ViewBag.RegOk = "Đăng kí thành công. Đăng nhập ngay";
                        ViewBag.isReg = true;
                        // Thêm người dùng mới
                        db.TaiKhoans.Add(taiKhoan);

                        db.SaveChanges();
                        ViewBag.RegOk = "Đăng kí thành công.";
                        return RedirectToAction("Dangnhap", "User");
                    }
                    else
                    {
                        
                        ViewBag.RegOk = "Email đã tồn tại! Vui lòng chọn 1 email khác";
                        return RedirectToAction("Dangky", "User");
                    }
                }
                else
                {
                    return View("Dangky");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Changepass()
        {
            return View();
        }

        
        [AllowAnonymous]
        public ActionResult Dangnhap()
        {
            return View();

        }


        [HttpPost]
        public ActionResult Dangnhap(FormCollection userlog)
        {
            string userMail = userlog["userMail"].ToString();
            string password = userlog["password"].ToString();

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var taiKhoan = db.TaiKhoans.SingleOrDefault(x => x.Email.Equals(userMail) && x.Matkhau.Equals(password));

            if (taiKhoan != null)
            {
                // Kiểm tra vai trò của người dùng
                string vaiTro = taiKhoan.PhanQuyen.TenQuyen;

                Session["use"] = taiKhoan;

                if (vaiTro == "Adminstrator")
                {
                    return RedirectToAction("Index", "Admin/HomeAd");
                }
                else if(vaiTro == "Nhanvien")
                {
                    return RedirectToAction("Index", "Admin/HomeAd");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Fail = "Tài khoản hoặc mật khẩu không chính xác.";
                return View("Dangnhap");
            }
        }
        public ActionResult DangXuat()
        {
            Session["use"] = null;
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Profile(int? id)
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
        //// GET: Admin/TaiKhoans/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
        //    if (taiKhoan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
        //    return View(taiKhoan);
        //}

        //// POST: Admin/TaiKhoans/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Idtaikhoan,Holot,Ten,Email,Dienthoai,Matkhau,ImgU,IDQuyen,Diachi")] TaiKhoan taiKhoan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(taiKhoan).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Profile", new { id = taiKhoan.Idtaikhoan });
        //    }
        //    ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
        //    return View(taiKhoan);
        //}
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

                    return RedirectToAction("Profile", new { id = existingTaiKhoan.Idtaikhoan });
                }
            }

            // ModelState không hợp lệ hoặc có lỗi xảy ra, trả về view với dữ liệu hiện tại
            ViewBag.IDQuyen = new SelectList(db.PhanQuyens, "IDQuyen", "TenQuyen", taiKhoan.IDQuyen);
            return View(taiKhoan);
        }

        public static byte[] encryptData(string data)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashedBytes;
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(data));
            return hashedBytes;
        }

         protected void SetAlert(string messange, string type)
        {
            TempData["AlertMessage"] = messange;
            switch (type)
            {
                case "success":
                    TempData["AlertType"] = "success"; break;
                case "warning":
                    TempData["AlertType"] = "warning"; break;
                case "error":
                    TempData["AlertType"] = "error"; break;
                default: 
                    TempData["AlertType"] = ""; break;
            }
        }

        public static string md5(string data)
        {
            return BitConverter.ToString(encryptData(data)).Replace("-", "").ToLower();
        }
    }
}