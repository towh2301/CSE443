using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models.Auth
{
    public class LoginViewModel
    {
        [Required]
        [DisplayName("Email")]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DisplayName("Remember me")]
        public bool RememberMe { get; set; }
    }
}
