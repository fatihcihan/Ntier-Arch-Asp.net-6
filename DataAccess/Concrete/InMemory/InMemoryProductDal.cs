using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal   // InMemory bir IProductDal'dir. Bellekte calisirken yazilan kodlar farklidir
    {                                               // Sanki bellekte data varmis gibi calisiyoruz

        List<Product> _products;
        public InMemoryProductDal()                // constructor -> bellekte referans alinca ilk calisan method
        {
            _products = new List<Product>{
                new Product{Id=1, CategoryId=1,Name="Product 1",UnitPrice=10},
                new Product{Id=2, CategoryId=1,Name="Product 2",UnitPrice=20},
                new Product{Id=3, CategoryId=2,Name="Product 3",UnitPrice=30},
                new Product{Id=4, CategoryId=2,Name="Product 4",UnitPrice=40}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = _products.FirstOrDefault(x => x.Id == product.Id);    // id uzerinden referansi yakaladik 
            if (productToDelete != null)
                _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public void Update(Product product)         // -> ui'dan gelen data
        {
            Product productToUpdate = _products.FirstOrDefault(x => x.Id == product.Id);    // id'yi yakaladik sonra mapledik
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.CategoryId = product.CategoryId;
                productToUpdate.UnitPrice = product.UnitPrice;
            }
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(x => x.CategoryId == categoryId).ToList();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }
    }
}
