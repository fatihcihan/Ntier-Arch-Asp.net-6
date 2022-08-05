using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        // interface methodlari default olarak public
        // Burada DataAccess product'i kullanacagi icin, Entities'i  referans etmek zorunda
       
    }
}
