using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Services;

public class BookService(ApplicationDbContext _context) : IBookService
{
    public IQueryable<Book> getBooksAsyncQueryable()
    {
        return _context.Book
            .Include(b => b.Category).Include(book => book.Author).Include(book => book.Loans)
            .AsQueryable();
        
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        return await _context.Book.Include(book => id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Book>> GetBooksByCategoryIdAsync(int? categoryId = null)
    {
        return await _context.Book.Where(b => b.CategoryId == categoryId).ToListAsync();
    }

    public Task<Book> AddBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task<Book> UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task<Book> DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await (_context.Category?.ToListAsync() ?? Task.FromResult(new List<Category>()));
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync()
    {
        return await (_context.Author?.ToListAsync() ?? Task.FromResult(new List<Author>()));
    }
}
