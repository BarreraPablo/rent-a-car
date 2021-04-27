using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Exceptions
{
    public class ArgumentNotDefinedException : Exception
    {
        public ArgumentNotDefinedException()
        {
        }

        public ArgumentNotDefinedException(string message) : base(message)
        {
        }
    }
}
