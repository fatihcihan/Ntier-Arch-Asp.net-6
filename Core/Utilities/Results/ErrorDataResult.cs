using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{

    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message)    // data ve mesaj ver
        {
            // Error oldugu icin default olarak false geciyoruz (tum islem sonuclari yani)
        }
        public ErrorDataResult(T data) : base(data, false)     // sadece data ver
        {
            // mesaj olayina girmek istemeyebiliriz. Sadece data ve false gecebiliriz
        }
        public ErrorDataResult(string message) : base(default, false, message)    // sadece mesaj ver  
        {
            // sadece mesaj verdirebiliriz (pek kullanilmaz) 
        }
        public ErrorDataResult() : base(default, false)        // hicbir sey verme
        {
            // default -> T'nin default hali data'ya karsilik gelir
        }
    }
}