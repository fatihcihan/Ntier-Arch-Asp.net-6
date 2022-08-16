using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
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
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();

            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();


            // asagidaki kodlar -> calisan uygulama icerisinde implemente edilmis interfaceleri bulur ve onlar icin aspect interceptor selectori cagirir
            // yani yukaridaki classlar icin once asagidaki selectoru cagiriyor (aspecti var mi diye check ediyor)

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
