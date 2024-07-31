namespace BankingExercise.Domain
{
    public class Transaction
    {
        public Guid TransactionId { get; private set; }
        public Guid AccountId { get; private set; }
        public DateTime Date { get; private set; }
        public double Amount { get; private set; }
        public double Balance { get; private set; }
        public TransactionType Type { get; private set; }

        public Transaction(Guid accountId, double amount, double balance, TransactionType transactionType)
        {
            TransactionId = Guid.NewGuid();
            AccountId = accountId;
            Date = DateTime.UtcNow;
            Amount = amount;
            Balance = balance;
            Type = transactionType;
        }

        public override string ToString()
        {
            var culture = new System.Globalization.CultureInfo("fr-FR");
            string output = String.Format(
                culture, "{0,-14:d} {1,-11} {2}",
                Date,
                Type is TransactionType.Credit ? Amount : -Amount,
                Balance);

            return output;
        }
    }
}