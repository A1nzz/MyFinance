using Application.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using UI.Pages;

namespace UI.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IUserService _userService;

        public RegisterViewModel(IUserService userService)
        {
            _userService = userService;
        }

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        string _email;

        [ObservableProperty]
        string _password;

        [RelayCommand]
        async void Register() => await TryRegister();

        [RelayCommand]

        async void GoToLogInPage() => await GoLogIn();

        public async Task TryRegister()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await App.Current.MainPage.DisplayAlert("Name", "Please, enter name", "Ок");
            }
            else if (string.IsNullOrWhiteSpace(Email))
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

                User us = await _userService.FirstOrDefaultAsync(u => u.Email == Email);
                if (us != null)
                {
                    await App.Current.MainPage.DisplayAlert("Registrantion", "Account with such email is already exists", "Ок");
                    return;
                }

                await _userService.AddAsync(new User(Name, Email, Password));
                await _userService.SaveChangesAsync();
                await App.Current.MainPage.DisplayAlert("Registrantion", "Account was successfully created", "Ок");

                await Shell.Current.GoToAsync($"//{nameof(LogInPage)}");
            }
        }

        public async Task GoLogIn()
        {
            await Shell.Current.GoToAsync($"//{nameof(LogInPage)}");

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
