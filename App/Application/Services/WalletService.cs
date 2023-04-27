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

        public void Add(Wallet item)
        {
            _unitOfWork.WalletRepository.Add(item);
        }

        public void Delete(Wallet item)
        {
            _unitOfWork.WalletRepository.Delete(item);
        }

        public IReadOnlyList<Wallet> GetAll()
        {
            return _unitOfWork.WalletRepository.ListAll();
        }

        public Wallet GetById(int id)
        {
            return _unitOfWork.WalletRepository.GetById(id);
        }

        public void Update(Wallet item)
        {
            _unitOfWork.WalletRepository.Update(item);
        }

        public void Deposit(Wallet wallet, double amount)
        {
            wallet.Balance += amount;
            _unitOfWork.WalletRepository.Update(wallet);
        }

        public void Withdraw(Wallet wallet, double amount)
        {
            wallet.Balance -= amount;
            _unitOfWork.WalletRepository.Update(wallet);
        }
    }
}
