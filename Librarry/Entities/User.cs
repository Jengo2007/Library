using System;
using System.Collections.Generic;

namespace Librarry.Entities
{
    public enum Role
    {
        Admin,
        Reader
    }
    public sealed class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        #nullable enable
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        #nullable disable
        public Role UserRole { get; set; }
        public List<Book> BorrowedBooks { get; set; }


        public User()
        {
            ID = Guid.NewGuid();
            BorrowedBooks = new List<Book>();
            Username = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            UserRole = Role.Reader;
        }

        public User(string username, string password, Role role)
        {
            ID = Guid.NewGuid();
            Username = username;
            Password = password;
            UserRole = role;
        }
    }
    
}