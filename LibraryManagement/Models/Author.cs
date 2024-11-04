namespace LibraryManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "The First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "The Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(1000, ErrorMessage = "The Biography cannot exceed 1000 characters.")]
        public string Biography { get; set; }

        [StringLength(50, ErrorMessage = "The Nationality cannot exceed 50 characters.")]
        public string Nationality { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "The Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        [StringLength(100, ErrorMessage = "The Website URL cannot exceed 100 characters.")]
        public string Website { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        [StringLength(255, ErrorMessage = "The Avatar path cannot exceed 255 characters.")]
        public string Avatar { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }


}
