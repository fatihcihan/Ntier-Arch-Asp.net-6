using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
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
        List<ProductDetailDto> GetProductDetails();     // tablolarin belirli kolonlarini getirmek istedigimiz icin
       
    }
}
