using BankAccountUnitTesting.Controller;
using BankAccountUnitTesting.Exceptions;
using BankAccountUnitTesting.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountUnitTesting.Tests
{
        //Use Fact when you have a straightforward, single-condition test.
        //Use Theory when you want to run the same test multiple times with different data values.
    public class UnitTest1
    {
        private readonly BankAccountController _controller = new BankAccountController();

        [Theory]
        [InlineData(1000, 150, 1150)]
        [InlineData(500, 100, 600)]
        public void Deposit_ValidAmount_IncreaseBalance(double initialBalance, double depositAmount, double expectedBalance)
        {
            // Arrange
            var account = new BankAccount(initialBalance);

            // Act
            _controller.Deposit(account, depositAmount);

            // Assert
            Assert.Equal(expectedBalance, account.Balance);
        }

        [Theory]
        [InlineData(1000, -46)]
        [InlineData(500, 0)]
        public void Deposit_InvalidAmount_ThrowsException(double initialBalance, double depositAmount)
        {
            // Arrange
            var account = new BankAccount(initialBalance);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _controller.Deposit(account, depositAmount));
        }

        [Theory]
        [InlineData(1000, 400, 600)]
        [InlineData(800, 200, 600)]
        public void Withdraw_ValidAmountWithSufficientBalance_DecreaseBalance(double initialBalance, double withdrawAmount, double expectedBalance)
        {
            // Arrange
            var account = new BankAccount(initialBalance);

            // Act
            _controller.Withdraw(account, withdrawAmount);

            // Assert
            Assert.Equal(expectedBalance, account.Balance);
        }

        [Theory]
        [InlineData(600, 250)]
        [InlineData(800, 400)]
        public void Withdraw_ValidAmountWithInsufficientBalance_ThrowsException(double initialBalance, double withdrawAmount)
        {
            // Arrange
            var account = new BankAccount(initialBalance);

            // Act & Assert
            Assert.Throws<InsufficientBalanceException>(() => _controller.Withdraw(account, withdrawAmount));
        }

        [Theory]
        [InlineData(1000, -10)]
        [InlineData(800, 0)]
        public void Withdraw_InvalidAmount_ThrowsException(double initialBalance, double withdrawAmount)
        {
            // Arrange
            var account = new BankAccount(initialBalance);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _controller.Withdraw(account, withdrawAmount));
        }

        [Theory]
        [InlineData(1000, 200, 800, 700)]
        public void Transfer_ValidAmountWithSufficientBalance_UpdatesBothAccounts(double fromInitialBalance, double transferAmount, double expectedFromBalance, double expectedToBalance)
        {
            // Arrange
            var fromAccount = new BankAccount(fromInitialBalance);
            var toAccount = new BankAccount(500);

            // Act
            _controller.Transfer(fromAccount, toAccount, transferAmount);

            // Assert
            Assert.Equal(expectedFromBalance, fromAccount.Balance);
            Assert.Equal(expectedToBalance, toAccount.Balance);
        }

        [Theory]
        [InlineData(500, 600)]
        [InlineData(700, 800)]
        public void Transfer_ValidAmountWithInsufficientBalance_ThrowsException(double fromInitialBalance, double transferAmount)
        {
            // Arrange
            var fromAccount = new BankAccount(fromInitialBalance);
            var toAccount = new BankAccount(500);

            // Act & Assert
            Assert.Throws<InsufficientBalanceException>(() => _controller.Transfer(fromAccount, toAccount, transferAmount));
        }

        //[Fact]
        //public void Transfer_InvalidAccount_ThrowsException()
        //{
        //    // Arrange
        //    var fromAccount = new BankAccount(1000);

        //    // Act & Assert
        //    Assert.Throws<InvalidAccountException>(() => _controller.Transfer(fromAccount, null, 200));
        //}
        [Fact]
        public void Deposit_NullAccount_ThrowsAccountNotFoundException()
        {
            // Act & Assert
            Assert.Throws<AccountNotFoundException>(() => _controller.Deposit(null, 100));
        }

        [Fact]
        public void Withdraw_NullAccount_ThrowsAccountNotFoundException()
        {
            // Act & Assert
            Assert.Throws<AccountNotFoundException>(() => _controller.Withdraw(null, 100));
        }

        [Fact]
        public void Transfer_FromNullAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            var toAccount = new BankAccount(500);

            // Act & Assert
            Assert.Throws<AccountNotFoundException>(() => _controller.Transfer(null, toAccount, 100));
        }

        [Fact]
        public void Transfer_ToNullAccount_ThrowsAccountNotFoundException()
        {
            // Arrange
            var fromAccount = new BankAccount(1000);

            // Act & Assert
            Assert.Throws<AccountNotFoundException>(() => _controller.Transfer(fromAccount, null, 100));
        }
    }
}
