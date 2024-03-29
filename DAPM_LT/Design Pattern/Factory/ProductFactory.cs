using DAPM_LT.Controllers.Design_Pattern.Interator;
using DAPM_LT.Controllers.Singleton;
using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_LT.Controllers.Design_Pattern.Factory
{
    public class ProductFactory : IProductFactory
    {
        private dapmEntities db = DatabaseManager.GetInstance().GetDatabase();
        public ProductFactory(dapmEntities dbContext)
        {
            db = dbContext;
        }

        public Sach CreateProduct(int productId)
        {
            return db.Saches.FirstOrDefault(p => p.Idsach == productId);
        }
    }
}
