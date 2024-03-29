using DAPM_LT.Controllers.Design_Pattern.Factory;
using DAPM_LT.Controllers.Design_Pattern.Interator;
using DAPM_LT.Controllers.Singleton;
using DAPM_LT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DAPM_LT.Controllers
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
            var saches = db.Saches.Where(n => n.Idsach == id).ToList();

            List<Sach> favoriteProductList = new List<Sach>();
            foreach (var sach in saches)
            {
                int sachId = sach.Idsach; // Truy cập vào thuộc tính Idsach của từng đối tượng Sach
                favoriteProductList.Add(productFactory.CreateProduct(sachId));
            }


            ViewBag.ProductInfor = favoriteProductList; // Updated ViewBag
            return View(favoriteProductList);
        }

        // POST: FavoriteProduct/InsertListFavorite
        [HttpPost]
        public ActionResult InsertListFavorite(Sach favoriteProd)
        {
            if (ModelState.IsValid)
            {
                var productAvail = db.Saches.FirstOrDefault(p => p.Idsach == favoriteProd.Idsach && p.Idloai == favoriteProd.Idloai);
                if (productAvail != null)
                    return RedirectToAction("Index/" + favoriteProd.Idsach, "Details");
                else
                {

                    db.Saches.Add(favoriteProd);
                    db.SaveChanges();

                    return RedirectToAction("FavoriteList/" + favoriteProd.Idsach, "FavoriteProduct");
                }
            }
            return View("Index/" + favoriteProd.Idsach, "Details");
        }

        public ActionResult DeleteProduct(Sach favoriteProd)
        {
            if (ModelState.IsValid)
            {
                var prod = db.Saches.FirstOrDefault(p => p.Idsach == favoriteProd.Idsach && p.Idloai == favoriteProd.Idloai);
                db.Saches.Remove(prod);
                db.SaveChanges();
            }
            return RedirectToAction("FavoriteList/" + favoriteProd.Idloai, "FavoriteProduct");
        }
    }
}