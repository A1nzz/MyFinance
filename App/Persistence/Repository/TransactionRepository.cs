using Domain.Entities;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace App.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly List<Transaction> _transactions;
        private MyFinanceContext _context;

        public TransactionRepository()
        {
            _transactions = new List<Transaction>()
            {
                new Transaction {Name = "CustomName1", Amount = 2.1m, Date = DateTime.Now, CategoryId = 1, WalletId = 1, Id = 1},
                new Transaction {Name = "CustomName2", Amount = 32.1m, Date = DateTime.Now, CategoryId = 1, WalletId = 1, Id = 2},
                new Transaction { Name = "CustomName3", Amount = 22.5m, Date = DateTime.Now, CategoryId = 1, WalletId = 2, Id = 3}
            };
        }

        public void Add(Transaction entity)
        {
            _transactions.Add(entity);
        }

        public void Delete(Transaction entity)
        {
            _transactions.Remove(entity);
        }

        public Transaction FirstOrDefault(Func<Transaction, bool> filter)
        {
            return _transactions.FirstOrDefault(filter)!;
        }

        public Transaction GetById(int id)
        {
            return _transactions.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public IReadOnlyList<Transaction> List(Func<Transaction, bool> filter)
        {
            return _transactions.Where(filter).ToList();          
        }

        public IReadOnlyList<Transaction> ListAll()
        {
            return _transactions;
        }

        public void Update(Transaction entity)
        {
            var index = _transactions.FindIndex(x => x.Id == entity.Id);
            if (index != -1) { _transactions[index] = entity; }
        }
    }
 

}
