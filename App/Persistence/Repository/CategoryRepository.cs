using Persistence.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using System.Linq.Expressions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace App.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly List<Category> _categories;
        private MyFinanceContext _context;

        public CategoryRepository()
        {
            _categories = new List<Category>()
            {
                new Category {Name = "Еда", UserId = 1, Id = 1},
                new Category {Name = "Одежда", UserId = 1, Id = 2},
                new Category { Name = "Транспорт", UserId = 2, Id = 3}
            };
        }

        public void Add(Category entity)
        {
            _categories.Add(entity);
        }

        public void Delete(Category entity)
        {
            _categories.Remove(entity);
        }

        public Category FirstOrDefault(Func<Category, bool> filter)
        {
            return _categories.FirstOrDefault(filter)!;
        }

        public Category GetById(int id)
        {
            return _categories.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public IReadOnlyList<Category> ListAll()
        {
            return _categories;
        }

        public IReadOnlyList<Category> List(Func<Category, bool> filter)
        {
            return _categories.Where(filter).ToList();
        }

        public void Update(Category entity)
        {
            var index = _categories.FindIndex(x => x.Id == entity.Id);
            if (index != -1) { _categories[index] = entity; }
        }
    }

}
