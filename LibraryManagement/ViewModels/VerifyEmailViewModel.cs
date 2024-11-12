using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.ViewModels;

public class VerifyEmailViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }
}
