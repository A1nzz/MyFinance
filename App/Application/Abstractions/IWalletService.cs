using Domain.Entities;

namespace Application.Abstractions
{
    public interface IWalletService : IBaseService<Wallet>
    {
        Task Deposit(Wallet wallet, double amount);
        Task Withdraw(Wallet wallet, double amount);
    }
}
