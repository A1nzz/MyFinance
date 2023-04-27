using Domain.Entities;

namespace Application.Abstractions
{
    public interface IWalletService : IBaseService<Wallet>
    {
        void Deposit(Wallet wallet, double amount);
        void Withdraw(Wallet wallet, double amount);
    }
}
