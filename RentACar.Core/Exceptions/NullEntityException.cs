using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Exceptions
{
    public class NullEntityException : Exception
    {
        public NullEntityException()
        {
        }

        public NullEntityException(string message) : base(message)
        {
        }
    }
}
