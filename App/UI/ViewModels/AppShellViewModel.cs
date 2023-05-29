using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public partial class AppShellViewModel
    {
        [RelayCommand]
        async void LogOut() => await TryLogOut();
        public async Task TryLogOut()
        {
            await Shell.Current.GoToAsync("//LogInPage");

        }
    }
}
