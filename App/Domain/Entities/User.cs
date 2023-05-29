namespace Domain.Entities 
{ 
    public class User : Entity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User()
        {
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

       


    }
}
