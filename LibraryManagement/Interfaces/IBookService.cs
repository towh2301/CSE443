using LibraryManagement.Models;

namespace LibraryManagement.Interfaces;

public interface IBookService
{
    
    // Get all books
    IQueryable<Book> getBooksAsyncQueryable();
    
    // Get book by id
    Task<Book> GetBookByIdAsync(int id);
    
    //Get book by category id
    Task<IEnumerable<Book>> GetBooksByCategoryIdAsync(int? categoryId = null);
    
    // Add book
    Task<Book> AddBookAsync(Book book);
    
    // Update book
    Task<Book> UpdateBookAsync(Book book);
    
    // Delete book
    Task<Book> DeleteBookAsync(int id);
    
    // Get all categories
    Task<IEnumerable<Category>> GetCategoriesAsync();
    
    // Get all authors
    Task<IEnumerable<Author>> GetAuthorsAsync();
}