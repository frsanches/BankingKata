using System.Collections.ObjectModel;
using System.Text;

namespace BankingExercise.Domain
{
    public class Account
    {
        private Collection<Transaction> _transactions;

        public Guid AccountId { get; private set; }
        public double Balance { get; private set; }
        public ReadOnlyCollection<Transaction> Transactions { get { return new ReadOnlyCollection<Transaction>(_transactions); } }

        private Account(double amount)
        {
            AccountId = Guid.NewGuid();
            Balance = amount;
            _transactions = new Collection<Transaction>
            {
                new Transaction (AccountId, amount, Balance, TransactionType.Credit)
            };
        }
        public static Account Create(double amount)
        {
            if (amount < 10)
                throw new ArgumentException("You should deposit minimum 10 $ to create an account");

            return new Account(amount);
        }
        public void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Deposit amount should be superior to 0");

            Balance += amount;

            _transactions.Add(new Transaction(AccountId,amount, Balance, TransactionType.Credit));
        }

        public void Withdraw(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("The amount should be superior to zero");

            if (Balance < amount) throw new InvalidOperationException("You don't have enough balance in your account to do this operation");

            Balance -= amount;

            _transactions.Add(new Transaction(AccountId, amount, Balance, TransactionType.Debit));
        }

        public string PrintStatement()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Date           Amount      Balance\n");

            foreach (Transaction transaction in _transactions)
            {
                sb.AppendLine(transaction.ToString());
            }

            return sb.ToString();
        }
    }
}