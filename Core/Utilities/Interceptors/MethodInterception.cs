using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        // her yere gidip try catch yazmiyoruz, bu bizim temel try catchimiz gibi dusunebiliriz. Daha sonra attribute olarak kullanabiliriz.
        // burasi bizim butun methodlarin catisi, method direkt calismayacak once buradan gececek
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }
        protected virtual void OnSuccess(IInvocation invocation) { }
        // virtual -> ezmeni bekleyen methodlardir
        public override void Intercept(IInvocation invocation)      // invocation -> calistirmak istedigimiz method oluyor
        {
            var isSuccess = true;
            OnBefore(invocation);       // methodun basinda calisir
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);     // hata aldiginda calisir
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);      // basarili olunca calisir
                }
            }
            OnAfter(invocation);    // methoddan sonra calisir
        }
    }
}
