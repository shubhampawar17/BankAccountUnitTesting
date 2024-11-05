using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountUnitTesting.Exceptions
{
    public class InvalidAccountException : Exception
    {
        public InvalidAccountException(string message) : base(message)
        {
        }
    }
}
