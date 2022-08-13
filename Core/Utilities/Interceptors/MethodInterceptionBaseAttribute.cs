using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    // aop icin gerekli altyapiyi hazirliyoruz. (log, cache, transaction, exception vs. attribute olarak kullanacagiz)
    // classlar ve methodlar icin kullanilabilir, birden fazla kullanilabilir ve inherit eden de kullanabilir
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor     // interceptor -> autofac'in interceptor ozelligi var demistik (DynamicProxy)
    {
        public int Priority { get; set; }       // priority -> oncelik (once loglama sonra validation vs. gibi)

        public virtual void Intercept(IInvocation invocation)       // invocation -> bizim methodumuza karsilik geliyor
        {

        }
    }
   
}
