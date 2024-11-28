using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public class BookViewModel
{
        public int BookId { get; set; }
        public string Title { get; set; }                  // Title of the book
        public string Description { get; set; }            // Description of the book
        public string BookCode { get; set; }               // Standard Book Number
        public string Publisher { get; set; }              // Publisher information
        public DateTime? PublishedYear { get; set; }       // Year the book was published
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; } // References the author
        
        public int TotalCopies { get; set; }               // Total number of physical copies in the library
        public int AvailableCopies { get; set; }           // Number of available copies (excluding loaned ones)
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Date when the book record was created
        public string Avatar { get; set; }                 // Cover image location on the server
        public string Pdf { get; set; }
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();// Path for the online PDF version of the book
        
        // Method to convert BookViewModel to Book
        public Book ToBook()
        {
            return new Book
            {
                BookId = BookId,
                Title = Title,
                Description = Description,
                BookCode = BookCode,
                Publisher = Publisher,
                PublishedYear = PublishedYear,
                CategoryId = CategoryId,
                Category = Category,
                AuthorId = AuthorId,
                Author = Author,
                TotalCopies = TotalCopies,
                AvailableCopies = AvailableCopies,
                CreatedDate = CreatedDate,
                Avatar = Avatar,
                Pdf = Pdf,
                Loans = Loans
            };
        }

        public Book ToNewBook()
        {
            return new Book
            {
                BookId = BookId,
                Title = Title,
                Description = Description,
                BookCode = BookCode,
                Publisher = Publisher,
                PublishedYear = PublishedYear,
                CategoryId = CategoryId,
                Category = Category,
                AuthorId = AuthorId,
                Author = Author,
                TotalCopies = TotalCopies,
                AvailableCopies = TotalCopies,
                CreatedDate = CreatedDate,
                Avatar = Avatar,
                Pdf = Pdf,
                Loans = Loans
            };
        }
        
        // Method to Edit Book From BookViewModel
        public Book EditBook(Book currentBook, BookViewModel book)
        {
            currentBook.Title = book.Title;
            currentBook.Description = book.Description;
            currentBook.BookCode = book.BookCode;
            currentBook.Publisher = book.Publisher;
            currentBook.PublishedYear = book.PublishedYear;
            currentBook.CategoryId = book.CategoryId;
            currentBook.Category = book.Category;
            currentBook.AuthorId = book.AuthorId;
            currentBook.Author = book.Author;
            currentBook.TotalCopies = book.TotalCopies;
            currentBook.AvailableCopies = book.AvailableCopies;
            currentBook.CreatedDate = book.CreatedDate;
            currentBook.Avatar = book.Avatar;
            currentBook.Pdf = book.Pdf;
            currentBook.Loans = book.Loans;
            return currentBook;
        }
}
