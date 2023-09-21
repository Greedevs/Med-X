using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedX.Service.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException(string message) : base(message)
        { }
        public AlreadyExistException(string message,Exception innerException) : base(message,innerException)
        { }
        public int StatusCode { get; set; } = 403;
    }
}
namespace MedX.Service.Exceptions
{
}
namespace MedX.Service.Exceptions
{
}
