using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IDataResult<T> : IResult //IResult'tan success ve message bilgisi geliyor zaten datayi da buradan aliyor
    {
        T Data { get; }
    }
}
