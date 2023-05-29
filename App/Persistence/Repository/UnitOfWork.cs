using App.Repositories;
using Domain.Abstractions;
using Domain.Entities;
using Persistence.Data;
using System;
namespace Persistence.Repository
{
/*    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyFinanceContext _context;
        private readonly Lazy<IRepository<Category>> _categoryRepository;
        private readonly Lazy<IRepository<Transaction>> _transactionRepository;
        private readonly Lazy<IRepository<User>> _userRepository;
        private readonly Lazy<IRepository<Wallet>> _walletRepository;
        public UnitOfWork(MyFinanceContext context)
        {
            _context = context;
            _categoryRepository = new Lazy<IRepository<Category>>(() => new CategoryRepository());
            _transactionRepository = new Lazy<IRepository<Transaction>>(() => new TransactionRepository());
            _walletRepository = new Lazy<IRepository<Wallet>>(() => new WalletRepository());
            _userRepository = new Lazy<IRepository<User>>(() => new UserRepository());
        }



        IRepository<Category> IUnitOfWork.CategoryRepository => _categoryRepository.Value;
        IRepository<Transaction> IUnitOfWork.TransactionRepository => _transactionRepository.Value;
        IRepository<User> IUnitOfWork.UserRepository => _userRepository.Value;
        IRepository<Wallet> IUnitOfWork.WalletRepository => _walletRepository.Value;
    }*/
}
