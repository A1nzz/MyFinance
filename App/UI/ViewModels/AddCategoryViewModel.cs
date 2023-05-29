using Application.Abstractions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Entities;
using System.Collections.ObjectModel;


namespace UI.ViewModels
{
    public partial class AddCategoryViewModel : ObservableObject
    {

        private readonly ICategoryService _catService;

        public AddCategoryViewModel(ICategoryService catService)
        {
            this._catService = catService;
        }

        public ObservableCollection<Category> CategoriesList { get; set; } = new();

        [ObservableProperty]
        string _categoryName;


        [ObservableProperty]
        string _selectedCategoryType;

        [RelayCommand]
        async void AddCat() => await AddCategory();

        [RelayCommand]
        async void UpdateCategoriesList() => await GetCats();

        [RelayCommand]
        async void DoRemove(Category ct) => await RemoveCat(ct);

        public async Task AddCategory()
        {
            int type = 0;

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


            if (CategoriesList.Any(c => c.Name == CategoryName))
            {
                await App.Current.MainPage.DisplayAlert("Category", "This category already exists", "Ок");
                return;
            }
            if (CategoryName == null)
            {
                await App.Current.MainPage.DisplayAlert("Name", "Input category name", "Ок");
            }
            else if (SelectedCategoryType == null)
            {
                await App.Current.MainPage.DisplayAlert("Type", "Select Type", "Ок");
            }
            else if (SelectedCategoryType == "Withdraw")
            {
                
                type = 1;
                await _catService.AddAsync(new Category() { Name = CategoryName, UserId = usId, Type = type });
                await _catService.SaveChangesAsync();
                await GetCats();

                await App.Current.MainPage.DisplayAlert("Success", "Category successfully created", "Ок");

            }
            else if (SelectedCategoryType == "Deposit")
            {
                
                type = 0;

                await _catService.AddAsync(new Category() { Name = CategoryName, UserId = usId, Type = type });
                await _catService.SaveChangesAsync();
                await GetCats();

                await App.Current.MainPage.DisplayAlert("Success", "Category successfully created", "Ок");

            }
        }

        public async Task GetCats()
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
                CategoriesList.Clear();
                foreach (var pos in poses)
                    CategoriesList.Add(pos);
            });
        }

        public async Task RemoveCat(Category ct)
        {
            await _catService.DeleteAsync(ct);
            await _catService.SaveChangesAsync();
            await GetCats();
        }

    }
}
