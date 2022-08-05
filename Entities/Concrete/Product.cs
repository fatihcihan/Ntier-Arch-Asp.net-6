using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }

        /*  Access Modifier       
         
        public -> Bu class'a diger katmanlar/projeler (classlar'dan) erisebilsin demektir
        default olarak internal'dır (business, data access vs. erisebilir)
        
        internal -> public'de oldugu gibi proje icerisinden ya da namespace icinden erisebiliyoruz
        internal'ın public'den farki ise diger projelerden ulasim iznini vermemesidir (sadece entities erisebilir)

        protected -> sadece tanimlandigi sinif icerisinde ya da o sinifi inherit eden sinif icinden ulasibilmektedir

        private -> sadece tanimlandigi sinif icerisinden ulasilir. en cok sinirlandiran keyworddur ve bir degiskenin default
        erisim belirleyicisi private'dir

        protected internal -> ayni solitoun icerisinde fakat farkli bir proje ya da namespace icerisinde olan baska bir sinifi
        inherit etmis ve ilgili alan protected internal ile tanimlanmissa bu alana ulasabiliriz        
         
       */
    }
}
