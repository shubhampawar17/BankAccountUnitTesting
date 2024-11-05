using BankAccountUnitTesting.Exceptions;
using BankAccountUnitTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountUnitTesting.Controller
{
    public class BankAccountController
    {
        public void Deposit(BankAccount account, double amount)
        {
            if (account == null)
                throw new AccountNotFoundException("Account not found");

            if (amount <= 0)
                throw new ArgumentException("Deposit amount must be positive");

            account.IncreaseBalance(amount);
        }

        public void Withdraw(BankAccount account, double amount)
        {
            if (account == null)
                throw new AccountNotFoundException("Account not found");

            if (amount <= 0)
                throw new ArgumentException("Withdraw amount must be positive");

            if (account.Balance - amount < BankAccount.MinimumBalance)
                throw new InsufficientBalanceException($"Insufficient balance, minimum balance is {BankAccount.MinimumBalance}");

            account.DecreaseBalance(amount);
        }

        public void Transfer(BankAccount fromAccount, BankAccount toAccount, double amount)
        {
            if (fromAccount == null)
                throw new AccountNotFoundException("Source account not found");

            if (toAccount == null)
                throw new AccountNotFoundException("Destination account not found");

            Withdraw(fromAccount, amount); // Withdraw from the sender's account
            Deposit(toAccount, amount);    // Deposit into the receiver's account
        }

    }
    }
