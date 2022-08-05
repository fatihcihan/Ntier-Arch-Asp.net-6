using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    // tek tabloyu ilgilendiren butun sql operasyonlari hazir
    public class EfOrderDal : EfEntityRepositoryBase<Order, EfContext>, IOrderDal
    {
    }
}
