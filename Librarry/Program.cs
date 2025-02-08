using System;
using System.Diagnostics;
using Librarry.Entities;
using Librarry.Services;

namespace Librarry
{
    class Program
    {
        static void Main(string[] args)
        {
            LibrarryServices librarry = new();
            User admin = new User("Admin", "1234",Role.Admin);
            librarry.RegisteredUsers.Add(admin);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1-Регистрация пользователя");
                Console.WriteLine("2-ЛОГ--ИН");
                Console.WriteLine("3-Просмотр списка пользователей");
                Console.WriteLine("4-Удаление пользователей");
                Console.WriteLine("5-Добавление книг в библиотеку");
                Console.WriteLine("6-Поиск книги");
                Console.WriteLine("7-Просмотр стутус книги");
                Console.WriteLine("8-Выдача книг пользователю");
                Console.WriteLine("9-Пометка книг как возврашенной");
                Console.WriteLine("10-Список книг");
                Console.WriteLine("11-Список доступных и недоступных книг ");
                string input = Console.ReadLine();
                int number;
                if (!int.TryParse(input, out number))
                {
                    Console.WriteLine("Oshibka vvoda");
                    continue;
                }

                switch (number)
                {
                    case 1:
                        Console.WriteLine("Введите имя ползователя:");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string password = Console.ReadLine();
                        librarry.Registration(name, password);
                        break;
                    case 2:
                        Console.WriteLine("Введите имя ползователя:");
                        string name2 = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        string password2 = Console.ReadLine();
                        librarry.Login(name2, password2);
                        break;
                    case 3:
                        if (librarry.IsAdmin())
                        {
                            librarry.GetUsers();
                        }
                        break;
                    case 4:
                        if (librarry.IsAdmin())
                        {
                            Console.WriteLine("Введите имя пользователя для удаления:");
                            string deletusername = Console.ReadLine();
                            if(librarry.DeleteUser(deletusername))     
                            {
                                Console.WriteLine("Пользователь: "+deletusername+"  успешно удалён");
                            }
                        }
                        break;
                    case 5:
                        if (librarry.IsAdmin())
                        {
                            Console.WriteLine("Введите имя книги:");
                            string name1 = Console.ReadLine();
                            Console.WriteLine("Введите автор книги:");
                            string author=Console.ReadLine();
                            Console.WriteLine("Введите жанр книги:");
                            string genre = Console.ReadLine();
                            Console.WriteLine("Введите год выпуска книги:");
                            int year=Convert.ToInt32(Console.ReadLine());
                            librarry.AddBook(name1,author,genre,year);
                        }
                        break;
                    case 6:
                        Console.WriteLine("Введите имя книги:");
                        string name3 = Console.ReadLine();
                        librarry.SearchBook(name3);
                        break;
                    case 7:
                        Console.WriteLine("Введите имя книги:");
                        string name4 = Console.ReadLine();
                        librarry.StatusBook(name4);
                         break;
                    case 8:
                        Console.WriteLine("Введите имя книги:");
                        string name5 = Console.ReadLine();
                        Console.WriteLine("Введите имя ползователя:");
                        string user = Console.ReadLine();
                        Console.WriteLine("Введите дату возврата книги");
                        int day = Convert.ToInt32(Console.ReadLine());
                        librarry.IssueBook(name5, user,day);
                        break;
                    case 9:
                        Console.WriteLine("Введите имя книги:");
                        string title = Console.ReadLine();
                        librarry.ReturnBook(title);
                        break;
                    case 10:
                        librarry.GetBooks();
                        break;
                    case 11:
                        librarry.SetBooks();
                        break;
                    default:
                        Console.WriteLine("Spasibo");
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}