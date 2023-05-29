using Domain.Entities;

namespace UI
{
    public partial class App : IApplication
    {
        public static User UserDetails;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}