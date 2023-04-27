using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data {
    public class MyFinanceContext : DbContext
    {
        public List<Category> Categories { get; set; } = new();
        public List<User> Users { get; set; } = new();
        public List<Wallet> Wallets { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();

        


        public MyFinanceContext()
        {
         
        }



    }
}

