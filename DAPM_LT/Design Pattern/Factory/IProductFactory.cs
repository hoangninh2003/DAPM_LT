using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_LT.Controllers.Design_Pattern.Interator
{
    public interface IProductFactory
    {
        Sach CreateProduct(int productId);
    }
}
