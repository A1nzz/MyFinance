namespace Domain.Entities
{
    public class Transaction : Entity
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int WalletId { get; set; }
        public int Type { get; set; } //1 - +, 0 - -;

        public int UserId { get; set; }

        public Category? TransactionCategory { get; set; }
    }

}
