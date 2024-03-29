using DAPM_LT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAPM_LT.Controllers.Design_Pattern.Interator
{
    public class FavoriteProductIterator : IFavoriteProductIterator
    {
        private List<Sach> _favoriteProducts;
        private int _index = 0;

        public FavoriteProductIterator(List<Sach> favoriteProducts)
        {
            _favoriteProducts = favoriteProducts;
        }

        public bool HasNext()
        {
            return _index < _favoriteProducts.Count;
        }

        public Sach Next()
        {
            if (!HasNext())
            {
                throw new InvalidOperationException("No more favorite products.");
            }
            return _favoriteProducts[_index++];
        }
    }

}