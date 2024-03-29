using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAPM_LT.Models;

namespace DAPM_LT.Design_Pattern.Decorator
{
    public class BlackFridaySales : SalesOffDecorator
    {
        public BlackFridaySales(ISalesOff sales) : base(sales)
        {
        }
        public override double GetSalesPrice()
        {
            double priceBefore = msalesOff.GetSalesPrice();
            double saleRate = 0.1;
            double priceAfter = priceBefore * saleRate;

            return priceBefore - priceAfter;
        }
    }
}
