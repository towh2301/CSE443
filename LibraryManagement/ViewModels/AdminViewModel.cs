using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public class AdminViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Author> Authors { get; set; }
    
    public IEnumerable<User> Users { get; set; }
}
