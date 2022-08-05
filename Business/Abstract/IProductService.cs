using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService    // business -> hem entites'e hem de dal'a referans verdik
    {
        List<Product> GetAll();
    }
}
