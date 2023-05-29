using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

using UI.Pages;
using Domain.Entities;
using Application.Abstractions;
using UI.Controls;

namespace UI.ViewModels
{
    public partial class LogInViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        public LogInViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [ObservableProperty]
        string _email;

        [ObservableProperty]
        string _password;

        [RelayCommand]
        async void LogIn() => await TryLogIn();

        [RelayCommand]

        async void GoToRegisterPage() => await Register();

        public async Task TryLogIn()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                await App.Current.MainPage.DisplayAlert("Email", "Please, enter email", "Ок");
            }
            else if (string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Password", "Please, enter password", "Ок");
            }
            else if (!IsValidEmail(Email))
            {
                await App.Current.MainPage.DisplayAlert("Email", "Please, enter email correctly", "Ок");
            }
            else
            {
                var userDetails = await _userService.FirstOrDefaultAsync(u => u.Email == Email);
                if (userDetails is null)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid email or password", "Ok");
                }
                else if (userDetails.Password != Password)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Invalid email or password", "Ok");
                } else
                {
                    if (Preferences.ContainsKey(nameof(User)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }
                    string userDetailStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = userDetails;
                    AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                    await Shell.Current.GoToAsync("//DashboardPage");
                }        
            }
        }

        public async Task Register()
        {
            await Shell.Current.GoToAsync($"//{nameof(RegisterPage)}");
        }

        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
