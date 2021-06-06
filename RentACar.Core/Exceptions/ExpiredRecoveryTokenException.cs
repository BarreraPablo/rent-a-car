using System;

namespace RentACar.Core.Exceptions
{
    public class ExpiredRecoveryTokenException : Exception
    {
        public ExpiredRecoveryTokenException()
        {
        }

        public ExpiredRecoveryTokenException(string message) : base(message)
        {
        }
    }
}
