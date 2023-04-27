namespace Domain.Entities 
{ 
    public class User : Entity
    {
        public string Email { get; set; } = string.Empty;

        private string _password = string.Empty;

        public User()
        {
        }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            this._password = password;
        }

        public string? GetPassword()
        {
            return _password;
        }

        public void SetPassword(string password)
        {
            this._password = password;
        }

        public bool Authenticate(string? password)
        {
            return this._password == password;
        }
    }
}
