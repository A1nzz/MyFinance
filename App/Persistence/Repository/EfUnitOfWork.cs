using App.Repositories;
using Domain.Abstractions;
using Domain.Entities;
using Persistence.Data;


namespace Persistence.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly MyFinanceContext _context;
        private readonly Lazy<IEfRepository<Category>> _categoryRepository;
        private readonly Lazy<IEfRepository<Transaction>> _transactionRepository;
        private readonly Lazy<IEfRepository<User>> _userRepository;
        private readonly Lazy<IEfRepository<Wallet>> _walletRepository;
        public EfUnitOfWork(MyFinanceContext context)
        {
            _context = context;
            _categoryRepository = new Lazy<IEfRepository<Category>>(() => new EfRepository<Category>(context));
            _transactionRepository = new Lazy<IEfRepository<Transaction>>(() => new EfRepository<Transaction>(context));
            _walletRepository = new Lazy<IEfRepository<Wallet>>(() => new EfRepository<Wallet>(context));
            _userRepository = new Lazy<IEfRepository<User>>(() => new EfRepository<User>(context));
        }



        IEfRepository<Category> IUnitOfWork.CategoryRepository => _categoryRepository.Value;
        IEfRepository<Transaction> IUnitOfWork.TransactionRepository => _transactionRepository.Value;
        IEfRepository<User> IUnitOfWork.UserRepository => _userRepository.Value;
        IEfRepository<Wallet> IUnitOfWork.WalletRepository => _walletRepository.Value;
        public async Task CreateDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
        public async Task RemoveDatbaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
