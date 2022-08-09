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
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
   
}
