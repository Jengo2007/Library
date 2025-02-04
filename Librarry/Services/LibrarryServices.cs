using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Librarry.Entities;
using Librarry.Interfaces;

namespace Librarry.Services
{
    public class LibrarryServices:ILibraryService
    {
        public List<User> RegisteredUsers { get; set; } = new List<User>();
        public List<Book> Books { get; set; } = new List<Book>();
        public User CurrentUser { get; set; } = null;

        public bool Registration(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))

            {
                Console.WriteLine("Имя пользователя обизательно");
            }
            
            foreach (var user in RegisteredUsers)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Пользователь уже зарегистрирован");
                    return false;
                }
            }

            var newUSer = new User
            {
                Username = username,
                Password = password
            };
            RegisteredUsers.Add(newUSer);
            Console.WriteLine("Пользователь успешно зарегистирован");
            return true;
        }

        public bool Login(string username, string password)
        {
            foreach (var registeredUser in RegisteredUsers)
            {
                if (registeredUser == null)
                {
                    
                    Console.WriteLine("Поле не должно быть пустым");
                   return false;
                }
                if (registeredUser.Username == username && registeredUser.Password == password)
                {
                    CurrentUser = registeredUser;
                    Console.WriteLine("Вы успешно зарегистрировались");
                    return true;
                }
                
            }
            return true;
        }

        public List<User> GetUsers()
        {
            Console.WriteLine("Список зарегистрированных пользователей:");
            foreach (var user in RegisteredUsers)
            {
                Console.WriteLine($"Имя ползователей {user.Username} Роль {user.UserRole}");
            }

            return RegisteredUsers;
        }
        public bool IsAdmin()
        {
            return CurrentUser != null && CurrentUser.UserRole == Role.Admin;
        }

        public bool DeleteUser(string usernameTodelete)
        {
            if (!IsAdmin())
            {
                Console.WriteLine("Only admin can delete users");
                return false;
            }

            bool userDeleted = false;
            
            for (int i = 0; i < RegisteredUsers.Count; i++)
            {
                if (RegisteredUsers[i].Username == usernameTodelete)
                {
                    RegisteredUsers.RemoveAt(i);
                    Console.WriteLine("Удалён пользователь с таким именем");
                    userDeleted = true;
                    break;
                }
            }
            return false;
        }

        public void AddBook(string title, string author, string genre, int year)
        {
            foreach (var book in Books)
            {
                if (book.Title == title && book.Author == author)
                {
                    Console.WriteLine("Эта книга уже добавлена");
                }
            }

            var newBook = new Book
            {
                Title = title,
                Author = author,
                Genre = genre,
                Year = year
            };
            Books.Add(newBook);
            Console.WriteLine($"Книга '{title}' успешно добавлена в библиотеку.");
        }

        public List<Book> SearchBook(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Поле поиска не должно быть пустым.");
                return new List<Book>();
            }

            var foundBooks = new List<Book>();
            foreach (var book in Books)
            {
                if (book.Title.Contains(searchTerm) || book.Author.Contains(searchTerm) || book.Genre.Contains(searchTerm))
                {
                    foundBooks.Add(book);
                }
            }
              
            if (foundBooks.Any())
            {
                Console.WriteLine("Найденные книги:");
                foreach (var book in foundBooks)
                {
                    Console.WriteLine($"Название: {book.Title}, Автор: {book.Author}, Жанр: {book.Genre}, Год выпуска: {book.Year}");
                }
            }
            else
            {
                Console.WriteLine("Книги не найдены.");
            }

            return foundBooks;
        }

        public void StatusBook(string bookTitle)
        {
            bool founBook = false;
            
            foreach (var foundBook in Books)
            {
                {
                    if (foundBook.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))

                    {
                        founBook = true;
                        if (foundBook.IsAvailable)
                        {
                            Console.WriteLine($"Книга '{foundBook.Title}' доступна для выдачи.");
                            
                        }
                        else if (!founBook)
                        {
                            Console.WriteLine("Книга ненайден");
                        }

                        else
                        {
                            Console.WriteLine($"Книга '{foundBook.Title}' выдана. Должна быть возвращена до {foundBook.DueDate?.ToString("ddd.MMM.yyy")}.");

                        }
                        break;
                    }
                }
            }
        }
        public void IssueBook(string title, string username)
        {
            Book foundBook = null;
            User users = null;
            
            foreach (var book in Books)
            {
                if (book.Title == title)
                {
                    foundBook = book;
                    break;
                }
            }

            foreach (var user in RegisteredUsers)
            {
                if (user.Username == username)
                {
                    users = user;
                    break;
                }
            }
            
            if (foundBook == null)
            {
                Console.WriteLine("Книга не найдена.");
                return;
            }

            if (users == null)
            {
                Console.WriteLine("Пользователь не найден.");
                return;
            }
            if (!foundBook.IsAvailable)
            {
                Console.WriteLine("Книга уже выдана.");
                return;

            }

            foundBook.IsAvailable = false;
            foundBook.Borrower = users;
            foundBook.DueDate = DateTime.Now.AddDays(8);
            Console.WriteLine($"Книга '{foundBook.Title}' выдана пользователю {users.Username}. Должна быть возвращена до {foundBook.DueDate?.ToString("ddd.MMM.yyy")}.");
        }
        public void ReturnBook(string title)
        {
            Book foundBook = null;
            foreach (var book in Books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))

                {
                    foundBook = book;
                    break;
                }
            }

            if(foundBook==null)
                {
                    Console.WriteLine($"Несушествует книга под таким названиeм");
                }


                if (foundBook.IsAvailable)
                {
                        Console.WriteLine("Книга уже доступна в библиотеке.");
                }
                else 
                {
                        foundBook.IsAvailable = true;
                        foundBook.Borrower = null;
                        foundBook.DueDate = null;
                        Console.WriteLine($"Книга '{foundBook.Title}' успешно возвращена.");
                }
        }

        public List<Book> GetBooks()
        {
            Console.WriteLine("Список Книг:");
            foreach (var book in Books)
            {
                Console.WriteLine($"Книги: {book.Title}");
            }

            return Books;
        }
    }
}