using Domain.Entities;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using System.Linq.Expressions;

namespace App.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly  List<User> _users;
        private MyFinanceContext _context;

        public UserRepository()
        {
            _users = new List<User>()
            {
                new User {Name = "Kirill", Email = "kirill@gmail.com", Id = 1},
                new User {Name = "Serg",Email = "serg@gmail.com", Id = 2},
                new User { Name = "Alex", Email="alex@mail.ru", Id = 3}
            };
        }

        public void Add(User entity)
        {
            _users.Add(entity);
        }

        public void Delete(User entity)
        {
            _users.Remove(entity);
        }

        public User FirstOrDefault(Func<User, bool> filter)
        {
            return _users.FirstOrDefault(filter);
        }

        public User GetById(int id)
        {
            return _users.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public IReadOnlyList<User> List(Func<User, bool> filter)
        {
            return _users.Where(filter).ToList();
        }

        public IReadOnlyList<User> ListAll()
        {
            return _users;
        }

        public void Update(User entity)
        {
            var index = _users.FindIndex(x => x.Id == entity.Id);
            if (index != -1) { _users[index] = entity; }
        }
        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }
    }

}
