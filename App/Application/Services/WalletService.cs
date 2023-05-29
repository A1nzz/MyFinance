using Application.Abstractions;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services
{
    public class WalletService : IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public Task AddAsync(Wallet item)
        {
            return _unitOfWork.WalletRepository.AddAsync(item);
        }

        public Task DeleteAsync(Wallet item)
        {
            return _unitOfWork.WalletRepository.DeleteAsync(item);
        }

        public Task SaveChangesAsync()
        {
            return _unitOfWork.SaveAllAsync();
        }

        public Task UpdateAsync(Wallet item)
        {
            return _unitOfWork.WalletRepository.UpdateAsync(item);
        }

        Task<IReadOnlyList<Wallet>> IBaseService<Wallet>.GetAllAsync()
        {
            return _unitOfWork.WalletRepository.ListAllAsync();
        }

        Task<Wallet> IBaseService<Wallet>.GetByIdAsync(int id)
        {
            return _unitOfWork.WalletRepository.GetByIdAsync(id);
        }

        public Task Deposit(Wallet wallet, double amount)
        {
            wallet.Balance += amount;
            return _unitOfWork.WalletRepository.UpdateAsync(wallet);
        }

        public Task Withdraw(Wallet wallet, double amount)
        {
            wallet.Balance -= amount;
           return  _unitOfWork.WalletRepository.UpdateAsync(wallet);
        }
    }
}
