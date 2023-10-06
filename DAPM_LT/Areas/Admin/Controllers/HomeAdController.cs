using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_LT.Areas.Admin.Controllers
{
    public class HomeAdController : Controller
    {
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
    }
}