using DAPM_LT.Controllers.Design_Pattern.Factory;
using DAPM_LT.Controllers.Design_Pattern.Interator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAPM_LT.Controllers.Singleton;
using DAPM_LT.Models;

namespace DAPM_LT.Areas.Admin.Controllers
{
    public class FavoriteProductController : Controller
    {
        private dapmEntities db = DatabaseManager.GetInstance().GetDatabase();
        private IProductFactory productFactory;

        public FavoriteProductController()
        {
            productFactory = new ProductFactory(db);
        }

        // GET: FavoriteProduct/FavoriteList/5
        public ActionResult FavoriteList(int id)
        {
            var saches = db.Saches.Where(n => n.GiaMua == id).ToList();

            List<Sach> favoriteProductList = new List<Sach>();
            foreach (var sach in saches)
            {
                int sachId = sach.Idsach;
                favoriteProductList.Add(productFactory.CreateProduct(sachId));
            }


            ViewBag.ProductInfor = favoriteProductList; // Updated ViewBag
            return View(favoriteProductList);
        }

       [HttpPost]
        public ActionResult InsertListFavorite(Sach favoriteProd) // Corrected parameter type
        {
            if (ModelState.IsValid)
            {
                var productAvail = db.Saches.FirstOrDefault(p => p.Idsach == favoriteProd.Idsach && p.GiaMua == favoriteProd.GiaMua);
                if (productAvail != null)
                {
                    return RedirectToAction("Details", "Home", new { id = favoriteProd.Idsach });
                }
                else
                {
                    db.Saches.Add(favoriteProd); // Corrected DbSet
                    db.SaveChanges();
                    return RedirectToAction("FavoriteList", new { id = favoriteProd.GiaMua }); // Corrected parameter
                }
            }
            // If model state is invalid, return to details page
            return RedirectToAction("Details", "Home", new { id = favoriteProd.Idsach });
        }

        // POST: FavoriteProduct/DeleteProduct
        [HttpPost]
        public ActionResult DeleteProduct(int sachId, int giaMua) // Corrected parameter names
        {
            var prod = db.Saches.FirstOrDefault(p => p.Idsach == sachId && p.GiaMua == giaMua); // Corrected query
            if (prod != null)
            {
                db.Saches.Remove(prod); // Corrected DbSet
                db.SaveChanges();
            }
            return RedirectToAction("FavoriteList", new { id = giaMua }); // Corrected parameter
        }
    }
}