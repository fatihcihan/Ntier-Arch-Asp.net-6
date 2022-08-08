using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message)    // data ve mesaj ver
        {
            // Success oldugu icin default olarak true geciyoruz (tum islem sonuclari yani)
        }
        public SuccessDataResult(T data) : base(data, true)     // sadece data ver
        {
            // mesaj olayina girmek istemeyebiliriz. Sadece data ve true gecebiliriz
        }
        public SuccessDataResult(string message) : base(default, true, message)    // sadece mesaj ver  
        {
            // sadece mesaj verdirebiliriz (pek kullanilmaz) 
        }
        public SuccessDataResult() : base(default, true)        // hicbir sey verme
        {
           // default -> T'nin default hali data'ya karsilik gelir
        }
    }
}
