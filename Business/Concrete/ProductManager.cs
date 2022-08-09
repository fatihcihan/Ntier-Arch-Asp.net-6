using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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

        [ValidationAspect(typeof(ProductValidator))]     // validation'i buraya tasidik aspect sayesinde
        public IResult Add(Product product)
        {                               // Error veya succes resultlarin icine string yazmamaliyiz. 
                                        // magic stringler isimizi gorur. business katmaninda constants icinde static classla yonetebiliriz burayi

            //if (product == null)       // buraya gerek yok fluent validation ile gerekli kontrolleri yaptik
            //{
            //    return new ErrorResult(Messages.ProductNotAdded);
            //}


            //product ile ilgili validationlari burada yapiyoruz

            //var context = new ValidationContext<Product>(product);  // context -> ilgili thread
            //ProductValidator productValidator = new ProductValidator();     // product validator ile (ilgili kurallar icin)
            //var result = productValidator.Validate(context);
            //productValidator.ValidateAndThrow(product);

            // yukaridaki validation'ı core katmaninda cross cutting concerns altindaki validation'a tasidik

            // yukaridaki tum kodlari core'a tasiyip sadece ilgili validator referansi ve objeyi vererek daha okunur hale getirdik

            //ValidationTool.Validate(new ProductValidator(), product); 
            // core katmanina tasidigimiz yapiyi da aop sayesinde bir attribute olarak kullanarak kod karmasasindan kurtulacagiz


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
