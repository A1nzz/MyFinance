using Application.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Storages
{
    public class TransactionStorage
    {
        private readonly ITransactionService _simpleTransactionService;

        public TransactionStorage(ITransactionService simpleTransactionService)
        {
            _simpleTransactionService = simpleTransactionService;
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Load();
            });
        }
        public ObservableCollection<Transaction> SimpleTransactions { get; set; } = new();

        private async Task Load()
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

            var transactions = await _simpleTransactionService.GetAllAsync();
            transactions = transactions.Where(p => p.UserId == usId).ToList();

            foreach (var t in transactions)
            {
                SimpleTransactions.Add(t);
            }
        }

        public async Task Reload()
        {
            SimpleTransactions.Clear();
            await Load();
        }


        public void Add(Transaction st)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SimpleTransactions.Add(st);
            });
        }

        public void Edit(Transaction st, int index)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SimpleTransactions[index] = st;
            });
        }

        public void Remove(Transaction st)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SimpleTransactions.Remove(st);
            });
        }
    }
}
