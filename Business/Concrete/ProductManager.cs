using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService   // IProductService'i implement ederek servis olarak actik ve burada gerekli is kodlarini yaziyoruz
    {                                               // bir is sinifi baska siniflari new'lemez, injection eder, yani ne entity framework ne de in memory ismi gececek
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;       // burada constructor calisinca bizden bir product dal referansi istiyor ve biz de inject ettik
        }                                   // su an in memory kullaniyoruz daha sonra entity framework kullanabiliriz sonucta
        public List<Product> GetAll()
        {
            // yazilacak olan is kodlarindan gectikten sonra...
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int categoryId)
        {
            return _productDal.GetAll(x => x.CategoryId == categoryId); // bu filtrelemeyi ef'deki expressions sayesinde yaptik
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(x => x.UnitPrice <= min && x.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
