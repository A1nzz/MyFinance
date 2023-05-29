using Application.Abstractions;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Entities;
using System.Collections.ObjectModel;

namespace UI.ViewModels
{
    public partial class AddWalletViewModel : ObservableObject
    {
        private readonly IWalletService _walletService;

        public AddWalletViewModel(IWalletService walletService)
        {
            this._walletService = walletService;
            MessagingCenter.Subscribe<MainViewModel>(this, "update", (sender) => UpdateWalletsList());

        }

        public ObservableCollection<Wallet> WalletsList { get; set; } = new();


        [ObservableProperty]
        string _walletName;

        [ObservableProperty]
        string _walletBalance;


        [RelayCommand]
        async void UpdateWalletsList() => await GetWallets();

        [RelayCommand]
        async void AddWallet() => await AddWall();

        [RelayCommand]
        async void DoRemove(Wallet wl) => await RemoveWall(wl);

        public async Task AddWall()
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

            if (WalletsList.Any(c => c.Name == WalletName))
            {
                await App.Current.MainPage.DisplayAlert("Wallet", "This wallet already exists", "Ок");
                return;
            }

            if (WalletName == null)
            {
                await App.Current.MainPage.DisplayAlert("Name", "Input wallet name", "Ок");

            } else if (!double.TryParse(WalletBalance, out double wallBalance))
            {
                await App.Current.MainPage.DisplayAlert("Amount", "Enter number", "Ок");
            } else
            {
                await _walletService.AddAsync(new Wallet() { Name = WalletName, UserId = usId, Balance = wallBalance });
                await _walletService.SaveChangesAsync();
                await GetWallets();
                await App.Current.MainPage.DisplayAlert("Success", "Wallet successfully created", "Ок");

            }

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
                WalletsList.Clear();
                foreach (var pos in poses)
                    WalletsList.Add(pos);
            });
        }

        public async Task RemoveWall(Wallet wl)
        {
            await _walletService.DeleteAsync(wl);
            await _walletService.SaveChangesAsync();
            await GetWallets();
        }
    }
}
