using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountUnitTesting.Models
{
    public class BankAccount
    {
        public double Balance { get; private set; }
        public const double MinimumBalance = 500;

        public BankAccount(double initialBalance = 0)
        {
            if (initialBalance < MinimumBalance)
                throw new ArgumentException($"Initial balance cannot be less than {MinimumBalance}");
            Balance = initialBalance;
        }

        internal void IncreaseBalance(double amount)
        {
            Balance += amount;
        }

        internal void DecreaseBalance(double amount)
        {
            Balance -= amount;
        }
    }
}
