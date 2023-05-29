using Application.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace UI.ViewModels
{
    public partial class DepositViewModel : ObservableObject
    {
        private readonly ICategoryService _catService;


        private readonly IWalletService _walletService;

        private readonly ITransactionService _transactionService;

        public DepositViewModel(ICategoryService catService, IWalletService walletService, ITransactionService transactionService)
        {
            _catService = catService;
            _walletService = walletService;
            _transactionService = transactionService;
        }

        public ObservableCollection<Category> DepositCategories { get; set; } = new();
        public ObservableCollection<Wallet> DepositWallets { get; set; } = new();

        [ObservableProperty]
        string _depositAmount;

        [ObservableProperty]
        string _depositDescription;

        [ObservableProperty]
        Category _depositSelectedCategory;

        [ObservableProperty]
        Wallet _depositSelectedWallet;

        [ObservableProperty]
        DateTime _depositSelectedDate;

        [RelayCommand]
        async void UpdateDepositCatList() => await GetCategories();

        [RelayCommand]
        async void UpdateDepositWalletList() => await GetWallets();


        [RelayCommand]
        async void CreateDeposit() => await MakeDeposit();

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
                DepositCategories.Clear();
                foreach (var pos in poses)
                    if (pos.Type == 0)
                    {
                        DepositCategories.Add(pos);
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
                DepositWallets.Clear();
                foreach (var pos in poses)
                    DepositWallets.Add(pos);
            });
        }

        public async Task MakeDeposit()
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
            if (!decimal.TryParse(DepositAmount, out decimal amount))
            {
                await App.Current.MainPage.DisplayAlert("Amount", "Enter number", "Ок");
            }
            else if (DepositSelectedCategory == null)
            {
                await App.Current.MainPage.DisplayAlert("Category", "Select Category", "Ок");
            }
            else if (DepositSelectedWallet == null)
            {
                await App.Current.MainPage.DisplayAlert("Wallet", "Select Wallet", "Ок");
            }
            else 
            {
                if (DepositDescription == null)
                {
                    DepositDescription = string.Empty;
                }

                var transaction = new Transaction()
                {
                    Description = DepositDescription,
                    Amount = +amount,
                    CategoryId = DepositSelectedCategory.Id,
                    Date = DepositSelectedDate,
                    WalletId = DepositSelectedWallet.Id,
                    TransactionCategory = await _catService.GetByIdAsync(DepositSelectedCategory.Id),
                    Type = 0,
                    UserId = usId
                };
                await _transactionService.AddAsync(transaction);
                await _transactionService.SaveChangesAsync();
                await _walletService.Deposit(DepositSelectedWallet, Convert.ToDouble(amount));
                await _walletService.SaveChangesAsync();
                await App.Current.MainPage.DisplayAlert("Transction", "Transaction successfully created", "Ок");

            }


        }
    }
}
