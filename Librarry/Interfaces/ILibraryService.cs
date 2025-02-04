using System.Collections.Generic;
using Librarry.Entities;

namespace Librarry.Interfaces
{
    public interface ILibraryService
    {
        bool Registration(string username, string password);
        bool Login(string username, string password);
        List<User> GetUsers();
        bool IsAdmin();
        bool DeleteUser(string usernameTodelete);
        void AddBook(string title, string author, string genre, int year);
        List<Book> SearchBook(string searchTerm);
        public void StatusBook(string bookTitle);
        void IssueBook(string title, string username);
        void ReturnBook(string title);
        List<Book> GetBooks();
    }
}