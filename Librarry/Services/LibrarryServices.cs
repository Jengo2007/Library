using System;
using System.Collections.Generic;
using Librarry.Entities;

namespace Librarry.Services
{
    public class LibrarryServices
    {
        public List<User> RegisteredUsers { get; set; } = new List<User>();
        public List<Book> Books { get; set; } = new List<Book>();
        public User CurrentUser { get; set; } = null;

        public void Registration(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)||string.IsNullOrWhiteSpace(password))
            {
                    Console.WriteLine("Имя пользователя обизательно");
            }
            foreach (var user in RegisteredUsers)
            {
                if (user.Username == username)
                {
                    Console.WriteLine("Пользователь уже зарегистрирован");
                    
                }
                var newUSer = new User
                {
                    Username = username,
                    Password = password
                };
                RegisteredUsers.Add(newUSer);
                Console.WriteLine("Пользователь успешно зарегистрирован");
            }
        }

        public void Login(string username, string password)
        {
            foreach (var registeredUser in RegisteredUsers)
            {
                if (registeredUser == null)
                {
                    
                    Console.WriteLine("Поле не должно быть пустым");
                    
                }
                if (registeredUser.Username == username && registeredUser.Password == password)
                {
                    Console.WriteLine("Вы успешно зарегистрировались");
                }
            }
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

        public void DeleteUser(string usernameTodelete)
        {
            if (!IsAdmin())
            {
                Console.WriteLine("Only admin can delete users");
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
        }
    }
}