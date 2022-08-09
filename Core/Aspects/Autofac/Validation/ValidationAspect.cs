using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))    //validatorType bir ivalidator degilse hata firlat
            {
                throw new System.Exception("Bu dogrulama classi degil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);   // reflection -> calisma aninda islem yapmayi saglar
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];  
            // yukarida product validator'un calisma tipini bul (abstractvalidator<product>) generic argumanlarindan ilkini bul
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            // invocation -> method demek. Methodun parametrelerine bak (product)  birden fazla da olabilir
            foreach (var entity in entities)
            {
                // her birini tek tek gez ve validation tool'u kullanarak validation et
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
