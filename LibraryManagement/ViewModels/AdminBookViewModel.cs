using LibraryManagement.Models;

namespace LibraryManagement.ViewModels;

public class AdminBookViewModel
{
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Author> Authors { get; set; }
}
