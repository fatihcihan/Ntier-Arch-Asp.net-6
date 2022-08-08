using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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
    {                                   // bir is sinifi baska siniflari new'lemez, injection eder, yani ne entity framework ne de in memory ismi gececek
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;       // burada constructor calisinca bizden bir product dal referansi istiyor ve biz de inject ettik
        }                                   // su an in memory kullaniyoruz daha sonra entity framework kullanabiliriz sonucta

        public IResult Add(Product product)
        {                               // Error veya succes resultlarin icine string yazmamaliyiz. 
            if (product == null)        // magic stringler isimizi gorur. business katmaninda constants icinde static classla yonetebiliriz burayi
            {
                return new ErrorResult(Messages.ProductNotAdded);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);   // (data,mesage)
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId)
        {
            // business code...
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.CategoryId == categoryId));
            // yukaridaki filtrelemeyi ef'deki expressions sayesinde yaptik
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.Id == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(x => x.UnitPrice <= min && x.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
