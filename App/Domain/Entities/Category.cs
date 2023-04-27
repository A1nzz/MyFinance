namespace Domain.Entities
{
    public class Category : Entity
    {
        public string IconName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int Type { get; set; } //0 - -, 1 - +
    }

}
