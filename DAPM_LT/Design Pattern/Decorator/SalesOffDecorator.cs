using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAPM_LT.Models;

namespace DAPM_LT.Design_Pattern.Decorator
{
    public abstract class SalesOffDecorator : ISalesOff
    {
        protected ISalesOff msalesOff;

        public SalesOffDecorator(ISalesOff salesOff)
        {
            msalesOff = salesOff;
        }
        public abstract double GetSalesPrice();
    }
}