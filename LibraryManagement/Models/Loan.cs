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
        public string UserId { get; set; }
        [ForeignKey("Id")]
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
        // Set the status based on the due date and return date
        public int Status { get; set; }
            
            
        public int SetStatus(DateTime dueDate, DateTime? returnDate)
        {
            if (returnDate.HasValue && returnDate > dueDate)
            {
                return 2;
            }
            else if (!returnDate.HasValue)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
    

}

