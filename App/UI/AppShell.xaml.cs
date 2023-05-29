using CommunityToolkit.Mvvm.Input;
using UI.Pages;
using UI.ViewModels;

namespace UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddCategoryPage), typeof(AddCategoryPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddWalletPage), typeof(AddWalletPage));
            Routing.RegisterRoute(nameof(PieChartPage), typeof(PieChartPage));
            BindingContext = new AppShellViewModel();
        }

        
    }
}