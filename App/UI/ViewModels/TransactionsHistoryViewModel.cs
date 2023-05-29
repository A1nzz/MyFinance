using Application.Abstractions;
using Application.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using System.Collections.ObjectModel;
using UI.Pages;

namespace UI.ViewModels
{
    public partial class TransactionsHistoryViewModel : ObservableObject
    {
        public readonly ITransactionService _transService;
        public readonly ICategoryService _categService;

        public TransactionsHistoryViewModel(ITransactionService transactionService, ICategoryService categService)
        {
            this._transService = transactionService;
            this._categService = categService;
        }

        public ObservableCollection<Transaction> TransactionsHistoryList { get; set; } = new ();

        [RelayCommand]
        async void ShowStatistics() => await ShowStats();

        [RelayCommand]
        async void UpdateTransactionsList() => await GetTransactionsHistory();

        [RelayCommand]
        async void DoRemove(Transaction tr) => await RemoveTransaction(tr);

        public async Task ShowStats()
        {
            await Shell.Current.GoToAsync(nameof(PieChartPage));

        }

        public async Task GetTransactionsHistory()
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

            var poses = await _transService.GetAllAsync();

            poses = poses.Where(p => p.UserId == usId).ToList();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                TransactionsHistoryList.Clear();
                foreach (var pos in poses)
                    TransactionsHistoryList.Add(pos);
            });
        }

        public async Task RemoveTransaction(Transaction tr)
        {
            await _transService.DeleteAsync(tr);
            await _transService.SaveChangesAsync();
            await GetTransactionsHistory();
        }




    }
}
