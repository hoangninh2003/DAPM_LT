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

        public ActionResult Index()
        {
            
            List<Sach> sachList = db.Saches.ToList();
            List<string> dsl = sachList.Select(s => s.Loai.Tenloai).Distinct().ToList();
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


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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