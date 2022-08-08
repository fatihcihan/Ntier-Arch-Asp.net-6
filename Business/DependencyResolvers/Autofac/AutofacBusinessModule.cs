using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module   // Autofac'ten gelen module'den inherit ederek burada gerekli ioc yapilanmasını yapiyoruz
    {
        protected override void Load(ContainerBuilder builder)  // single instance ->tek bir tane instance tut yeter
        {
            // IProductService istenirse ona ProductManager'i ver. 
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
        }
    }
}
