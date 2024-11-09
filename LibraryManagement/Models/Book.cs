namespace LibraryManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Title cannot exceed 100 characters.")]
        public string Title { get; set; }                  // Title of the book

        [StringLength(1000, ErrorMessage = "The Description cannot exceed 1000 characters.")]
        public string Description { get; set; }            // Description of the book

        [StringLength(20, ErrorMessage = "The Book Code cannot exceed 20 characters.")]
        public string BookCode { get; set; }               // Standard Book Number

        [StringLength(100, ErrorMessage = "The Publisher name cannot exceed 100 characters.")]
        public string Publisher { get; set; }              // Publisher information

        [Range(1800, int.MaxValue, ErrorMessage = "The Published Year should be no earlier than 1800.")]
        public DateTime? PublishedYear { get; set; }       // Year the book was published

        [Required(ErrorMessage = "CategoryId is required.")]
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")] // References the category
        public Category Category { get; set; }

        [Required(ErrorMessage = "AuthorId is required.")]
        public int? AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; } // References the author

        [Range(0, int.MaxValue, ErrorMessage = "Total Copies cannot be negative.")]
        public int TotalCopies { get; set; }               // Total number of physical copies in the library

        [Range(0, int.MaxValue, ErrorMessage = "Available Copies cannot be negative.")]
        public int AvailableCopies { get; set; }           // Number of available copies (excluding loaned ones)

        public DateTime CreatedDate { get; set; } = DateTime.Now; // Date when the book record was created

        [StringLength(255, ErrorMessage = "The Avatar path cannot exceed 255 characters.")]
        public string Avatar { get; set; }                 // Cover image location on the server

        [StringLength(255, ErrorMessage = "The PDF path cannot exceed 255 characters.")]
        public string Pdf { get; set; }

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();// Path for the online PDF version of the book
    }

}
