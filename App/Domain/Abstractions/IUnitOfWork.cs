using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IEfRepository<Category> CategoryRepository { get; }
        IEfRepository<Transaction> TransactionRepository { get; }
        IEfRepository<User> UserRepository { get; }
        IEfRepository<Wallet> WalletRepository { get; }

        public Task RemoveDatbaseAsync();
        public Task CreateDatabaseAsync();
        public Task SaveAllAsync();

    }
}
