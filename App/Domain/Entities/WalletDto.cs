

namespace Domain.Entities
{
    public class WalletDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Balance { get; set; }
        public int UserId { get; set; }
    }
}
