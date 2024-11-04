using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }

        // Foreign Key and Navigation Property for User
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }                   // Each loan is associated with one user

        // Foreign Key and Navigation Property for Book
        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }                   // Each loan is associated with one book

        [Required]
        public DateTime LoanDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [Required]
        [Range(0, 2, ErrorMessage = "Status must be 0 (Active), 1 (Returned), or 2 (Overdue).")]
        public int Status { get; set; }
    }
}

