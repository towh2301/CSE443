using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public class ProfileViewModel
{
    public User? User { get; set; }
    public IEnumerable<Loan>? Loans { get; set; }
    
    public IEnumerable<Book>? Books { get; set; }
}
