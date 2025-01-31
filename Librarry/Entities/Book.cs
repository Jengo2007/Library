using System;

namespace Librarry.Entities
{
    public class Book
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime? DueDate { get; set; }
        public User Borrower { get; set; }
        public Book()
        {
            ID = Guid.NewGuid();
            Author = string.Empty;
            Title = string.Empty;
            Genre = string.Empty;
            Year = 0;
            IsAvailable = true;
            DueDate = null;
            Borrower = null;
        }
        
    }
}