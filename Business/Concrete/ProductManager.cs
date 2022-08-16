using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
{    // IProductService'i implement ederek servis olarak actik ve burada gerekli is kodlarini yaziyoruz
    public class ProductManager : IProductService
    {   // bir is sinifi baska siniflari new'lemez, injection eder, yani ne entity framework ne de in memory ismi gececek
        IProductDal _productDal;
        //ICategoryDal _categoryDal;  bir manager kendi dal'i haricinde hicbir dal'i inject edemez ama service inject edbilir
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            // burada constructor calisinca bizden bir product dal referansi istiyor ve biz de inject ettik
            _productDal = productDal;
            _categoryService = categoryService;
            //_categoryDal = categoryDal; 
        }
        // su an in memory kullaniyoruz daha sonra entity framework kullanabiliriz sonucta

        [ValidationAspect(typeof(ProductValidator))]     // validation'i buraya tasidik aspect sayesinde
        public IResult Add(Product product)
        {     // Error veya succes resultlarin icine string yazmamaliyiz. 
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



            // bir kategoride en fazla 10 urun olabilir (business code)
            // daha sonra 10 urun yerine 15 urun derlerse asagidaki kod patlar, cunku ayni kodu update icin de yazdik 
            // asagidaki kodu private olarak tanimladigimiz methodun icine yaziyp onu hem add'de hem de update'de kullanacagiz


            //var result = _productDal.GetAll(x => x.CategoryId == product.CategoryId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult("");
            //}


            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    // ayni isimde urun eklenemez 
            //    if (CheckIfProductNameExists(product.Name).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }
            //}
            //return new ErrorResult();

            // eger mevcut kategoris sayisi 15'i gectiyse sisteme yeni urun eklenemez (business code)

            // yukarida check ile baslayan is kodlari hep iresult donduruyor, core'da bi calisma motoru yazip (Run) iresultlari ele alabiliriz
            // bu yuzden core -> business -> business rules classina tasiyalim


            IResult result = BusinessRules.Run(
                CheckIfProductNameExists(product.Name),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded()
                 );
            // yukarida is kurallarini parametre olarak verdik ve calistirdik, eger tum kurallardan geciyorsa null'dir yoksa hata dondurecek
            // ilerleyen zamanlarda tekrar bir is kurali yazacak olursan onu bir methoda cevirip direkt yukariya parametre olarak gonderebiliriz
            if (result != null)
            {
                return result;
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

        [ValidationAspect(typeof(ProductValidator))] // add methodundaki validationu burada da rahatca kullanabiliyoruz
        public IResult Update(Product product)
        {
            return new SuccessResult();
        }

        // yukarida add methodundaki is kodunu buraya tasiyoruz (hatta is kurali parcacigi diyebiliriz)
        // bir kategoride en fazla 10 urun olabilirin is kuralini methoda tasidik
        // private yapmamizin sebebi producta ait bir is kodu parcacigi oldugu icin bu classta kullaniriz
        // artik bu methodu hem add hem de update methodunda cagirabiliriz.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(x => x.Name == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        // eger mevcut kategori sayisi 15'i gectiyse sisteme yeni urun eklenemez (business code)
        // product icin category service'i nasil yorumladigiyla alakali
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
