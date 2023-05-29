using CommunityToolkit.Mvvm.ComponentModel;

namespace Domain.Entities
{
    public partial class Wallet : Entity
    {
        [ObservableProperty]
        double balance;
        public int UserId { get; set; }


    }
}
