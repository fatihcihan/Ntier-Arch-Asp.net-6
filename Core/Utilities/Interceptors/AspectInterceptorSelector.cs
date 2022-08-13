using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>     // classin attributelerini oku
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)      // git ilgili methodun attributelerini oku
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);
            //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));  // otomatik olarak loglama yapar

            return classAttributes.OrderBy(x => x.Priority).ToArray();  // iligi
        }

        // ilgili class ve methodlarin attributelerini oku, onlari bir listeye koy ve calisma sirasini da priority (oncelige gore) sirala
    }
}
