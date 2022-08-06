using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResult  // void dondurenler icin (true, eklendi) gibi bir bilgilendirme
    {
        bool Success { get; }
        string Message { get; }

    }
}
