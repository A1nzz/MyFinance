using Domain.Entities;
using Domain.Abstractions;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace App.Repositories
{
    public class WalletRepository : IRepository<Wallet>
    {
        private readonly List<Wallet> _wallets;
        private MyFinanceContext _context;

        public WalletRepository()
        {
            _wallets = new List<Wallet>()
            {
                new Wallet {Balance = 200, Id = 1, UserId = 1},
                new Wallet {Balance = 300, Id = 2, UserId = 1},
                new Wallet {Balance = 400, Id = 3, UserId = 2}
            };
        }

        public void Add(Wallet entity)
        {
            _wallets.Add(entity);
        }

        public void Delete(Wallet entity)
        {
            _wallets.Remove(entity);
        }

        public Wallet FirstOrDefault(Func<Wallet, bool> filter)
        {
            return _wallets.FirstOrDefault(filter)!;
        }

        public Wallet GetById(int id)
        {
            return _wallets.Where(x => x.Id == id).FirstOrDefault()!;
        }

        public IReadOnlyList<Wallet> List(Func<Wallet, bool> filter)
        {
            return _wallets.Where(filter).ToList();
        }

        public IReadOnlyList<Wallet> ListAll()
        {
            return _wallets;
        }

        public void Update(Wallet entity)
        {
            var index = _wallets.FindIndex(x => x.Id == entity.Id);
            if (index != -1) { _wallets[index] = entity; }
        }
    }

}
