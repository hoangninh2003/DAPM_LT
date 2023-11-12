using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAPM_LT.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
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
    }
}