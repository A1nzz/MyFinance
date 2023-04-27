using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Category> CategoryRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Wallet> WalletRepository { get; }

    }
}
