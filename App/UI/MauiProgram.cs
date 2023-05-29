using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Domain.Abstractions;
using Persistence.Repository;
using Application.Abstractions;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Data;
using System.Reflection;
using Domain.Entities;
using UI.Pages;
using UI.ViewModels;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UI.Controls;
using UI.Storages;

namespace UI
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            string settingsStream = "UI.appsettings.json";
            var builder = MauiApp.CreateBuilder();
            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream(settingsStream);
            builder.Configuration.AddJsonStream(stream);
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseSkiaSharp(true)

                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            SetupServices(builder.Services);
            AddDbContext(builder);
            SeedData(builder.Services);


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void SetupServices(IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
            services.AddSingleton<ICategoryService, CategoryService>();
            services.AddSingleton<IWalletService, WalletService>();
            services.AddSingleton<IUserService, UserService> ();
            services.AddSingleton<ITransactionService, TransactionService>();

            services.AddTransient<MainPage>();
            services.AddTransient<MainViewModel>();

            services.AddTransient<DepositPage>();
            services.AddTransient<DepositViewModel>();


            services.AddTransient<AddCategoryPage>();
            services.AddTransient<AddCategoryViewModel>();

            services.AddTransient<AddWalletPage>();
            services.AddTransient<AddWalletViewModel>();

            services.AddTransient<TransactionsHistoryPage>();
            services.AddTransient<TransactionsHistoryViewModel>();

            services.AddTransient<PieChartPage>();
            services.AddTransient<PieChartViewModel>();

            services.AddSingleton<LogInPage>();
            services.AddSingleton<LogInViewModel>();

            services.AddSingleton<FlyoutHeaderControl>();

            services.AddSingleton<RegisterPage>();
            services.AddSingleton<RegisterViewModel>();

            services.AddSingleton<TransactionStorage>();

        }

        private static void AddDbContext(MauiAppBuilder builder)
        {
            var connStr = builder.Configuration.GetConnectionString("SqliteConnection");
            string dataDirectory = String.Empty;

#if ANDROID
 dataDirectory = FileSystem.AppDataDirectory+"/";
#endif

            connStr = String.Format(connStr, dataDirectory);
            var options = new DbContextOptionsBuilder<MyFinanceContext>()
            .UseSqlite(connStr)
           .Options;
            builder.Services.AddSingleton<MyFinanceContext>((s) => new MyFinanceContext(options));
        }

        public async static void SeedData(IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var unitOfWork = provider.GetService<IUnitOfWork>();

            await unitOfWork.RemoveDatbaseAsync();
            await unitOfWork.CreateDatabaseAsync();
            // Add cources
            IReadOnlyList<Category> cats = new List<Category>()
            {
            new Category {Name = "Food",Type =1 , UserId = 1, Id = 1},
            new Category {Name = "Transport", UserId = 1, Type = 1, Id = 2},
            new Category { Name = "Entertaiment",UserId = 2, Type = 1 , Id = 3},
            new Category { Name = "Freelance", UserId = 1, Type = 0 , Id = 4},
            new Category { Name = "Main work", UserId = 2, Type = 0 , Id = 5},
            };

            foreach (var cat in cats)
                await unitOfWork.CategoryRepository.AddAsync(cat);
            await unitOfWork.SaveAllAsync();

            IReadOnlyList<Wallet> wallets = new List<Wallet>
            {
                new Wallet {Name = "Card", UserId = 1, Id = 1 },
                new Wallet {Name = "Nal", UserId = 2, Id = 2 }
            };
            foreach (var wal in wallets)
                await unitOfWork.WalletRepository.AddAsync(wal);
            await unitOfWork.SaveAllAsync();

            IReadOnlyList<Transaction> transactions = new List<Transaction>
            {
                new Transaction {Description = "descr1", Type = 1, Amount = 100, CategoryId = 1, Date = DateTime.Now, WalletId = 1, TransactionCategory = cats[0], Id = 1, UserId =1  },
                new Transaction {Description = "descr2", Type = 1, Amount = 200, CategoryId = 2, Date = DateTime.Now, WalletId = 1, TransactionCategory = cats[0], Id = 2, UserId = 2 }
                
            };
            foreach (var tr in transactions)
                await unitOfWork.TransactionRepository.AddAsync(tr);
            await unitOfWork.SaveAllAsync();

            IReadOnlyList<User> users = new List<User>
            {
                new User("Admin","admin@gmail.com","admin"),
                new User("User","user@gmail.com","user")
            };
            foreach (var us in users)
                await unitOfWork.UserRepository.AddAsync(us);
            await unitOfWork.SaveAllAsync();
        }
    }
}