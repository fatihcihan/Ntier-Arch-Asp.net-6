using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService    // business -> hem entites'e hem de dal'a referans verdik
    {
        IDataResult<List<Product>> GetAll();    // IDataResult<T> -> T her sey olabilir artik (list,dto, entity vs.)
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IResult Add(Product product);       // void donduruyordu artik IResult donduruyor
        IDataResult<Product> GetById(int productId);
    }
}
