using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_LT.Controllers
{
    public class HomeController : Controller
    {
        private dapmEntities db = new dapmEntities();
        public ActionResult Index()
        {
            return View(db.Saches.ToList());
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
    }
}