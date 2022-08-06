using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // propertyler getter (readonly) olmasina ragmen constructor'da set ettik 
        public Result(bool success, string message) : this(success)     // this -> result class'ini temsil ediyor (tek parametreli constructor)
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}
