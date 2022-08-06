using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result     // base -> Result'i yani inherit edilen classi temsil ediyor
    {
        public SuccessResult(string message) : base(true, message)
        {

        }

        public SuccessResult() : base(true)    // parametresiz de gondeririz base'e default olarak true gonderiyor zaten
        {

        }
    }
}
