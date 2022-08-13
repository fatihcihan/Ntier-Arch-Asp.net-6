using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Validation
{
    // genellikle static olur (tek bir instance olusturur oradan devam edersin)
    public static class ValidationTool
    {
        // kendi validation toolumuz
        // validator -> product validator, category validator... vs
        public static void Validate(IValidator validator, object entity)    // entity de olabilir dto da olabilir o yuzden object 
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

    }
}
