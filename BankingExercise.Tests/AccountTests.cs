using BankingExercise.Domain;

namespace BankingExercise.Tests
{
    public class AccountTests
    {
        [Fact]
        public void Should_Create_NewAccount()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            Assert.NotNull(account);
        }

        [Fact]
        public void Should_HavePositiveBalance_WhenAccountCreated()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            Assert.Equal(amount, account.Balance);
        }

        [Fact]
        public void Should_AddNewTransaction_WhenAccountCreated()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            Assert.Single(account.Transactions);
            Assert.Equal(amount, account.Transactions.First().Amount);
        }

        [Fact]
        public void Should_Deposit_WhenAmountSuperiorToZero()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);
            account.Deposit(amount: 100);

            Assert.Equal(110, account.Balance);
        }

        [Fact]
        public void Should_AddNewTransaction_WhenNewDepositIsMade()
        {
            var amount = 10;
            var depositAmount = 100;

            var account = InitAccount(amount: amount);
            account.Deposit(amount: depositAmount);

            Assert.Equal(2, account.Transactions.Count);
            Assert.Equal(depositAmount, account.Transactions.Last().Amount);
            Assert.Equal(TransactionType.Credit, account.Transactions.Last().Type);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Should_NotDeposit_WhenAmountEqualOrInferiorToZero(double depositAmount)
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            var ex = Assert.Throws<ArgumentException>(() => account.Deposit(depositAmount));
        }

        [Fact]
        public void Should_Withdraw_WhenAmountSuperiorToZeroAndInferiorToAccountBalance()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            account.Withdraw(amount: 5);

            Assert.Equal(5, account.Balance);
        }

        [Fact]
        public void Should_AddNewTransaction_WhenNewWithdrawIsMade()
        {
            var amount = 10;
            var withdrawAmount = 5;

            var account = InitAccount(amount: amount);
            account.Withdraw(amount: withdrawAmount);

            Assert.Equal(2, account.Transactions.Count);
            Assert.Equal(withdrawAmount, account.Transactions.Last().Amount);
            Assert.Equal(TransactionType.Debit, account.Transactions.Last().Type);
        }

        [Fact]
        public void Should_NotWithdraw_WhenAmountSuperiorToZeroAndSuperiorToAccountBalance()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            var ex = Assert.Throws<InvalidOperationException>(() => account.Withdraw(amount: 15));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Should_NotWithdraw_WhenAmountInferiorOrEqualToZero(double withdrawAmount)
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            var ex = Assert.Throws<ArgumentException>(() => account.Withdraw(amount: withdrawAmount));
        }

        [Fact]
        public void Should_AddNewTransaction_WhenAmountSuperiorToZeroAndSuperiorToAccountBalance()
        {
            var amount = 10;

            var account = InitAccount(amount: amount);

            var ex = Assert.Throws<InvalidOperationException>(() => account.Withdraw(amount: 15));
        }

        [Fact]
        public void Should_ReturnTransactionStatement_WhenPrintStatement()
        {
            var amount = 10;
            var depositAmount = 1000;
            var withdrawAmount = 230;

            var account = InitAccount(amount: amount);
            account.Deposit(amount: depositAmount);
            account.Withdraw(amount: withdrawAmount);
            var accountStatement = account.PrintStatement();

            Assert.Contains("Date", accountStatement);
            Assert.Contains("Balance", accountStatement);
            Assert.Contains("Amount", accountStatement);
            Assert.Contains(depositAmount.ToString(), accountStatement);
            Assert.Contains(withdrawAmount.ToString(), accountStatement);
            Assert.Contains(amount.ToString(), accountStatement);
        }

        private Account InitAccount(double amount)
        {
            return Account.Create(amount: amount);
        }
    }
}