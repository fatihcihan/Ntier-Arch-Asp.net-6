using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    // fluent validation'dan gelen abstrac validator'u inherit ediyoruz
    public class ProductValidator : AbstractValidator<Product>  // dto icinde yapilabilir, constructor'ta yapiyoruz
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).MinimumLength(2);    // predicate'tan yararlaniyoruz
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.UnitPrice).NotNull();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 2);

            //custom validation
            RuleFor(p => p.Name).Must(StartWithA).WithMessage("Urun a harfi ile baslamali");
        }

        public bool StartWithA(string arg)      //arg -> product name
        {
            return arg.StartsWith("A");     // false donerse patlar 
        }
    }
}
