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
    public class ValidationAspect : MethodInterception  // aspect -> methodun sonunda basinda calisacak yapi
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)     // type -> attribute tip
        {
            // defensive coding -> aspectin typeof'u bu sekilde olmali [ValidationAspect(typeof(ProductValidator))]
            // yani typeof'u bir ivalidator olmali (ornek -> productvalidator) yoksa entity vs de verilebilirdi.
            // bunun onune gecmek icin ivalidator olmali diyoruz
            if (!typeof(IValidator).IsAssignableFrom(validatorType))    //validatorType (productValidator vs.)  bir ivalidator degilse hata firlat
            {
                throw new System.Exception("Bu dogrulama classi degil");
            }
            _validatorType = validatorType;
        }

        // validation methodun basinda yapilir onun icin virtual olan methodu override diyip ezerek icini dolduruyoruz
        protected override void OnBefore(IInvocation invocation)    // method interceptiondan geliyor 
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);   // reflection -> calisma aninda islem yapmayi saglar (product validatiorin instanceini olustur, ivalidator tipine cast et)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // _validatorType -> productValidator
            // product validator'in base'ine git (abstractvalidator<product>) onun generic arg. getir (product)
            // yukarida product validator'un calisma tipini bul, generic argumanlarindan ilkini bul 
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
