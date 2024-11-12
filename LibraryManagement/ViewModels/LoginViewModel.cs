using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [DisplayName("Email")]
    [EmailAddress]
    public String Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}