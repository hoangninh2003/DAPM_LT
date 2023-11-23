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
    public class HomeAdController : Controller
    {
        private dapmEntities db = new dapmEntities();
        // GET: Admin/HomeAd
        public ActionResult Index()
        {
            //var u = Session["use"] as DAPM_LT.Models.TaiKhoan;
            ////Kiểm tra nếu tên quyền Administrator mới được truy cập trang admin
            //if (u.PhanQuyen.TenQuyen == "Adminstrator")
            //{
            //    return View();
            //}
            //return RedirectPermanent("~/HomeAd/Index");


            return View();

        }
        
        public ActionResult Sachpartial()
        {
            var kiemsoats = db.Kiemsoats.Include(k => k.Sach);
            return View(kiemsoats.ToList());

        }


        public ActionResult TKpartial()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.PhanQuyen);
            return View(taiKhoans.ToList());
        }
    }
}