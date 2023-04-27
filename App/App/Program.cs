using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions;
using Domain.Abstractions;
using Persistence.Repository;
using Persistence.Data;
using Application.Services;
using Domain.Entities;

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

class Program
{

    
    static void Main(string[] args)
    {

        // Создание конфигурации приложения
        var configuration = new ConfigurationBuilder()
            .Build();

        // Создание ServiceProvider
        var serviceProvider = new ServiceCollection()
            .AddSingleton(configuration)
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<ITransactionService, TransactionService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IWalletService, WalletService>()
            .AddScoped<MyFinanceContext>()
            .BuildServiceProvider();

        // Получение экземпляров сервисов
        var categoryService = serviceProvider.GetService<ICategoryService>();
        var userService = serviceProvider.GetService<IUserService>();
        var transactionService = serviceProvider.GetService<ITransactionService>();
        var walletService = serviceProvider.GetService<IWalletService>();

        User currUser = null;



        //Функция для входа или регистрации
        void LogInFunc()
        {
            Console.WriteLine("Введите 0 - чтобы войти в аккаунт\nВВедите 1 - чтобы зарегистрироваться");

            int enter = Convert.ToInt32(Console.ReadLine());
            if (enter == 0)
            {
                //Вход в аккаунт


                
                Console.WriteLine("Войдите в аккаунт: ");
                while (true)
                {

                    Console.Write("Введите Email: ");
                    string? email = Console.ReadLine();

                    if (userService!.GetAll().Any(u => u.Email == email))
                    {
                        currUser = userService!.GetAll().Where(u => u.Email == email).FirstOrDefault()!;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный email. Попробуйте снова: ");
                        continue;
                    }
                }
                while (true)
                {
                    Console.Write("Введите пароль: ");
                    string? password = Console.ReadLine();
                    if (currUser.Authenticate(password))
                    {
                        Console.WriteLine("Вы успешно вошли в аккаунт");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Неверный пароль. Попробуйте снова: ");
                        continue;
                    }
                }




            }
            //Регистрация
            else if (enter == 1)
            {
                string newEmail, newPassword, newName;
                while (true)
                {
                    Console.Write("Введите email для вашего аккаунта: ");
                    string? email = Console.ReadLine();
                    if (email == string.Empty || email == null)
                    {
                        Console.WriteLine("Вы ничего не ввели");
                        continue;
                    }
                    else if (!IsValidEmail(email))
                    {
                        Console.WriteLine("Некоректный email. Попробуйте еще раз: ");
                        continue;
                    }
                    else
                    {
                        newEmail = email;
                        break;
                    }
                }
                while (true)
                {
                    Console.Write("Введите пароль для вашего аккаунта: ");
                    string? password = Console.ReadLine();
                    if (password == string.Empty || password == null)
                    {
                        Console.WriteLine("Вы ничего не ввели");
                        continue;
                    }
                    else
                    {
                        newPassword = password;
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Введите имя пользователя для вашего аккаунта: ");
                    string? name = Console.ReadLine();
                    if (name == string.Empty || name == null)
                    {
                        Console.WriteLine("Вы ничего не ввели");
                        continue;
                    }
                    else
                    {
                        newName = name;
                        break;
                    }
                }
                User newUser = new(newName, newEmail, newPassword);

                userService?.Add(newUser);
                walletService?.Add(new Wallet { Name=newName, Balance = 0, Id = newUser.Id, UserId = newUser.Id});
            } else
            {
                Console.WriteLine("Некорркектный ввод!(введите 0 или 1)");
            }

            
        }

        void AddCategory()
        {
            if (currUser == null)
            {
                Console.WriteLine("Войдите в аккаунт");
                return;
            }
            while (true)
            {
                Console.Write("Введите название категории: ");
                string? newCategoryName = Console.ReadLine();
                if (newCategoryName == null || newCategoryName == string.Empty)
                {
                    Console.WriteLine("Вы ничего не ввели");
                    continue;
                }
                if (categoryService!.GetAll().Any(c => c.Name == newCategoryName))
                {
                    Console.WriteLine("Такая категория уже сущесвует.");
                    continue;

                }
                else
                {
                    categoryService?.Add(new Category { Name = newCategoryName!, UserId = currUser.Id});
                    break;
                }
            }
            

        }

        void MakeTransaction()
        {
            if(currUser == null)
            {
                Console.WriteLine("Войдите в аккаунт");
                return;
            }

            //Wallet
            Wallet transactionWallet;
            Console.WriteLine("Выберите счет для транзакции(введите Id): ");
            var walletList = walletService?.GetAll();
            var userWallets = walletList?.Where(w => w.UserId == currUser.Id);
            foreach(Wallet wallet in userWallets!)
            {
                Console.WriteLine($"Id: {wallet.Id}" + $"Name: {wallet.Name}");

            }
            while(true)
            {
                Console.Write("Введите ID: ");
                int walletId = Convert.ToInt32(Console.ReadLine());
                if (walletService!.GetAll().Where(w => w.Id == walletId).ToList().Count == 0)
                {
                    Console.WriteLine("Такого счета нет.");
                    continue;
                }
                transactionWallet = walletService!.GetAll().Where(c => c.Id == walletId).FirstOrDefault()!;
                break;
            }

            //Type
            Console.WriteLine("Введите тип транзакции(0 - если траты, 1 - если пополнение): ");
            int type = Convert.ToInt32(Console.ReadLine());

            //Amount
            Console.WriteLine("Введите сумму транзакции: ");
            int amount = Convert.ToInt32(Console.ReadLine());

            //Category 
            Category transactionCategory;
            Console.WriteLine("Выберите категорию для транзакции: ");
            var categoryList = categoryService?.GetAll();
            foreach (var cat in categoryList!)
            {
                Console.WriteLine(cat.Name);
            }
            while (true)
            {
                Console.Write("Введите название категори: ");
                var transactionCategoryName = Console.ReadLine();
                if (transactionCategoryName == null || transactionCategoryName == string.Empty)
                {
                    Console.WriteLine("Вы ничего не ввели");
                    continue;
                }
                if (categoryService!.GetAll().Where(c => c.Name == transactionCategoryName).ToList().Count == 0)
                {
                    Console.WriteLine("Такой категории нет.");
                    continue;
                }
                transactionCategory = categoryService!.GetAll().Where(c => c.Name == transactionCategoryName).FirstOrDefault()!;
                break;
            }

            //Date
            DateTime transactionDate = DateTime.Now;

            //Description
            Console.WriteLine("Введите комментарии к транзакции");
            string? transactionDescription = Console.ReadLine();

            Transaction newTransaction = new() { WalletId = transactionWallet.Id, CategoryId = transactionCategory.Id, Type = type, Amount = amount, Date = transactionDate, Description = transactionDescription };
            transactionService?.Add(newTransaction);
            if (newTransaction.Type == 0)
            {
                walletService.Withdraw(transactionWallet ,amount);
            } else if (newTransaction.Type == 1)
            {
                walletService.Deposit(transactionWallet, amount);
            }
        }

        void AddWallet()
        {
            if (currUser == null)
            {
                Console.WriteLine("Войдите в аккаунт");
                return;
            }
            Console.WriteLine("Введите тип счета: ");
            string? walletType = Console.ReadLine();
            walletService?.Add(new Wallet() { Balance = 0, UserId = currUser.Id, Name = walletType!});

        }

        void ShowReps()
        {
            Console.WriteLine("Выберети репозиторий для просмотра: \n1 - Категории\n2 - Пользователи\n3 - Счета\n4 - Транзакции");
            int rep = Convert.ToInt32(Console.ReadLine());
            if (rep == 1)
            {
                foreach(var item in categoryService.GetAll()!)
                {
                    Console.WriteLine(item.Name);
                }
            } else if (rep == 2)
            {
                foreach (var item in userService?.GetAll()!)
                {
                    Console.WriteLine(item.Name + " Email: " + item.Email);
                }
            } else if(rep == 3)
            {
                foreach (var item in walletService?.GetAll()!)
                {
                    Console.WriteLine(item.UserId);
                }
            } else if(rep == 4)
            {
                foreach (var item in transactionService?.GetAll()!)
                {
                    Console.WriteLine(item.Type + " " + item.Amount + " " + item.Description + item.Date);
                }
            }
            
        }



        // Валидация email
        static bool IsValidEmail(string email)
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

        while(true)
        {
            Console.WriteLine("Введите команду для выполнения: \n" +
                          "0 - для завершения выполнения программы\n" +
                          "1 - для входа или регистрации аккаунта\n" +
                          "2 - для добавления категории\n" +
                          "3 - для создания транзакции\n" +
                          "4 - для добавления счета\n"+
                          "5 - показать содержимое репозиториев");
            int command = Convert.ToInt32(Console.ReadLine());
            if(command == 0)
            {
                break;
            }
            else if (command == 1)
            {
                LogInFunc();
                continue;
            }
            else if (command == 2)
            {
                AddCategory();
                continue;
            } else if (command == 3)
            {
                MakeTransaction();
                continue;
            } else if (command == 4)
            {
                AddWallet();
            } else if (command == 5)
            {
                ShowReps();
            }
        }
        
    }
}
