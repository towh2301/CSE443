namespace LibraryManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int UserId { get; set; }                       // Unique identifier for each user

        [Required(ErrorMessage = "Fullname is required.")]
        [StringLength(100, ErrorMessage = "The Fullname cannot exceed 100 characters.")]
        public string Fullname { get; set; }                  // Full name of the user

        [StringLength(500, ErrorMessage = "The Description cannot exceed 500 characters.")]
        public string Description { get; set; }               // Additional info about the user, like interests or bio

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "The Password cannot exceed 100 characters.")]
        public string Password { get; set; }                  // Hashed password for secure authentication

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "The Email cannot exceed 100 characters.")]
        public string Email { get; set; }                     // User's email address, used for communication and verification

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "The Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; }                     // User's contact phone number

        [StringLength(255, ErrorMessage = "The Address cannot exceed 255 characters.")]
        public string Address { get; set; }                   // User's physical address for mailing or location purposes

        [Range(0, int.MaxValue, ErrorMessage = "Status must be a non-negative integer.")]
        public int Status { get; set; }                       // Represents the user's status

        public DateTime CreatedDate { get; set; } = DateTime.Now; // Date and time when the user account was created

        [StringLength(50, ErrorMessage = "The UserCode cannot exceed 50 characters.")]
        public string UserCode { get; set; }                  // Unique code for internal identification of the user

        public bool IsLocked { get; set; } = false;           // Indicates if the account is locked

        public bool IsDeleted { get; set; } = false;          // Marks the account as deleted for soft deletion

        public bool IsActive { get; set; } = true;            // Shows if the account is active

        [StringLength(50, ErrorMessage = "The ActiveCode cannot exceed 50 characters.")]
        public string ActiveCode { get; set; }                // Code for account activation, sent via email during registration

        [StringLength(255, ErrorMessage = "The Avatar path cannot exceed 255 characters.")]
        public string Avatar { get; set; }

        ICollection<Loan> loans { get; set; } = new List<Loan>(); // Avatar’s User - Local location in the server to get the picture
    }

}
