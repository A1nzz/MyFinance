using Application.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;

using System.Collections.ObjectModel;

namespace UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        private readonly ICategoryService _catService;


        private readonly IWalletService _walletService;

        private readonly ITransactionService _transactionService;

        public MainViewModel(ICategoryService catService, IWalletService walletService, ITransactionService transactionService)
        {
            _catService = catService;
            _walletService = walletService;
            _transactionService = transactionService;
        }

        public ObservableCollection<Category> Categories { get; set; } = new();
        public ObservableCollection<Wallet> Wallets { get; set; } = new();

        [ObservableProperty]
        string _amount;

        [ObservableProperty]
        string _description;

        [ObservableProperty]
        Category _selectedCategory;

        [ObservableProperty]
        Wallet _selectedWallet;

        [ObservableProperty]
        DateTime _selectedDate;

        [RelayCommand]
        async void UpdateCatList() => await GetCategories();

        [RelayCommand]
        async void UpdateWalletList() => await GetWallets();

        [RelayCommand]
        async void CreateWithdraw() => await MakeWithdraw();

        public async Task GetCategories()
        {
            var userInfo = Preferences.Get(nameof(App.UserDetails), null);
            int usId;
            if (userInfo != null)
            {
                usId = App.UserDetails.Id;
            }
            else
            {
                usId = -1;
                await App.Current.MainPage.DisplayAlert("Name", "???", "Ок");

            }

            var poses = await _catService.GetAllAsync();

            poses = poses.Where(p => p.UserId == usId).ToList();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Categories.Clear();
                foreach (var pos in poses)
                    if (pos.Type == 1)
                    {
                        Categories.Add(pos);
                    }                
            });
        }

        public async Task GetWallets()
        {
            var userInfo = Preferences.Get(nameof(App.UserDetails), null);
            int usId;
            if (userInfo != null)
            {
                usId = App.UserDetails.Id;
            }
            else
            {
                usId = -1;
                await App.Current.MainPage.DisplayAlert("Name", "???", "Ок");

            }

            var poses = await _walletService.GetAllAsync();

            poses = poses.Where(p => p.UserId == usId).ToList();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Wallets.Clear();
                foreach (var pos in poses)
                    Wallets.Add(pos);
            });
        }

        public async Task MakeWithdraw()
        {

            var userInfo = Preferences.Get(nameof(App.UserDetails), null);
            int usId;
            if (userInfo != null)
            {
                usId = App.UserDetails.Id;
            }
            else
            {
                usId = -1;
                await App.Current.MainPage.DisplayAlert("Name", "???", "Ок");

            }

            if (!decimal.TryParse(Amount, out decimal amount))
            {
                await App.Current.MainPage.DisplayAlert("Amount", "Enter number", "Ок");
            }
            else if (SelectedCategory == null)
            {
                await App.Current.MainPage.DisplayAlert("Category", "Select Category", "Ок");
            }
            else if (SelectedWallet == null)
            {
                await App.Current.MainPage.DisplayAlert("Wallet", "Select Wallet", "Ок");
            }
            else
            {
                if (Description == null)
                {
                    Description = string.Empty;
                }
                var transaction = new Transaction()
                {
                    Description = Description,
                    Amount = -amount,
                    CategoryId = SelectedCategory.Id,
                    Date = SelectedDate,
                    WalletId = SelectedWallet.Id,
                    TransactionCategory = await _catService.GetByIdAsync(SelectedCategory.Id),
                    Type = 1,
                    UserId = usId
                };

                await _transactionService.AddAsync(transaction);
                await _transactionService.SaveChangesAsync();

                await _walletService.Withdraw(SelectedWallet, Convert.ToDouble(amount));
                await _walletService.SaveChangesAsync();
                MessagingCenter.Send(this, "update");

                await App.Current.MainPage.DisplayAlert("Transction", "Transaction successfully created", "Ок");

            }


        }

    }
}
