using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        // params -> run icerisine parametre olarak istedigimiz kadar iresult verebiliyoruz
        public static IResult Run(params IResult[] logics)     // is kurallarini (manager'da) calistirmak icin boyle bi class olusturduk
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)  // parametre ile gonderilen is kurallarindan basarisiz olani business'a haber ediyoruz
                {                       
                    return logic;
                }
            }
            return null;
        }


    }
}
