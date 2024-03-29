using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace DAPM_LT.Design_Pattern.Decorator
{
    public class SalesOff : ISalesOff
    {
        private dapmEntities db = new dapmEntities();
        public double _price;

        public SalesOff() { }
        public SalesOff(int _productId)
        {
            var sach = db.Saches.FirstOrDefault(p => p.Idsach == _productId);
            _price = (double)sach.GiaMua;
        }

        public double GetSalesPrice()
        {
            return _price;
        }
    }
}