using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence.Data {
    public class MyFinanceContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }

        public MyFinanceContext(DbContextOptions<MyFinanceContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }



    }
}

