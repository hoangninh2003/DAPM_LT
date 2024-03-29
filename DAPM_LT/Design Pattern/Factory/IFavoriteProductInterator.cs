using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileShopOnline.Controllers.Iterator
{
    public interface IFavoriteProductIterator
    {
        bool HasNext();
        Sach Next();
    }
}
