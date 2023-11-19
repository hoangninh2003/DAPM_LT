using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DAPM_LT.Controllers
{
    public class HomeController : Controller
    {
        private dapmEntities db = new dapmEntities();

        public ActionResult Index(string _selectedValue, string _name)
        {
           

            IQueryable<Sach> query = db.Saches;

            
            if (!string.IsNullOrEmpty(_selectedValue) && _selectedValue != "Tất cả")
            {
                query = query.Where(l => l.Loai.Tenloai.Contains(_selectedValue));
            }

            if (!string.IsNullOrEmpty(_name))
            {
                query = query.Where(s => s.Tieude.Contains(_name));
            }

            
            List<Sach> sachList = query.ToList();

           
            List<string> dsl = db.Saches.Select(l => l.Loai.Tenloai).Distinct().ToList();
            ViewBag.Categories = dsl;

           
            foreach (var sach in sachList)
            {
                sach.GiaMua = (decimal)Math.Floor(sach.GiaMua ?? 0);
            }

           
            Random random = new Random();
            foreach (var sach in sachList)
            {
                
            }

          
            return View(sachList);
        }



        public ActionResult Thanhtoan()
        {
            ViewBag.Message = "Thanh toán thành công.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
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
        [HttpPost]
        public ActionResult MuaSach(int idSach)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dangnhap", "User");
            }

            var sach = db.Saches.Find(idSach);
            if (sach == null)
            {
                return HttpNotFound();
            }

            var randomControl = db.Kiemsoats.Where(k => k.Idsach == idSach).OrderBy(r => Guid.NewGuid()).FirstOrDefault();

            if (randomControl != null)
            {
                db.Kiemsoats.Remove(randomControl);
                db.SaveChanges();

                ViewBag.Message = "Mua hàng thành công!";
            }
            else
            {
                ViewBag.Message = "Hiện tại không có sách để mua!";
            }

            return View("Details", sach);
        }



    }
}