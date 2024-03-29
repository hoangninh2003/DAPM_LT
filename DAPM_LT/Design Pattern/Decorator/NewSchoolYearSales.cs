using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAPM_LT.Models;

namespace DAPM_LT.Design_Pattern.Decorator
{
    public class NewSchoolYearSales : SalesOffDecorator
    {
        public NewSchoolYearSales(ISalesOff sales) : base(sales)
        {
        }

        public override double GetSalesPrice()
        {
            double priceBefore = msalesOff.GetSalesPrice();
            double saleRate = 0.2;
            double priceAfter = priceBefore * saleRate;

            return priceBefore - priceAfter;
        }
    }
}