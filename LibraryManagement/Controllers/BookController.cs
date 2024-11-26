using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class BookController(ApplicationDbContext context, ILogger<BookController> logger, IBookService bookService) : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BookDetail(int id)
        {
            var book = await context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public async Task<IActionResult> BookList(int? categoryId = null)
        {
            logger.LogInformation("Getting books for category {categoryId}", categoryId);
            try
            {
                // Get all categories for the navigation
                var categories = await (context.Category?.ToListAsync() ?? Task.FromResult(new List<Category>()));
                ViewBag.Categories = categories;
                ViewBag.CurrentCategoryId = categoryId;

                // Get books
                var booksQuery = bookService.getBooksAsyncQueryable();
                
                // var bookList = await bookService.GetBooksAsync();

                // Filter by category if specified
                if (categoryId > 0)
                {
                    booksQuery = booksQuery.Where(b => b.CategoryId == categoryId);
                    ViewBag.CurrentCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
                }

                // Execute query and get books
                var books = booksQuery.ToList();
                
                logger.LogInformation("Found {bookCount} books", books);
                
                // Parse List<Books> to List<BookViewModel>
                var bookViewModels = books.Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Description = b.Description,
                    BookCode = b.BookCode,
                    Publisher = b.Publisher,
                    PublishedYear = b.PublishedYear,
                    CategoryId = b.CategoryId,
                    Category = b.Category,
                    AuthorId = b.AuthorId,
                    Author = b.Author,
                    TotalCopies = b.TotalCopies,
                    AvailableCopies = b.AvailableCopies,
                    CreatedDate = b.CreatedDate,
                    Avatar = b.Avatar,
                    Pdf = b.Pdf,
                    Loans = b.Loans
                }).ToList();
                
                ViewBag.Books = bookViewModels ?? [];
                
                logger.LogInformation("Found {bookCount} book view model", bookViewModels.Count);
                
                // Return the partial view for AJAX requests
                if (Request.Headers.XRequestedWith == "XMLHttpRequest")
                {
                    return PartialView("_BookListPartial", bookViewModels);
                }

                return View();
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in BookList: {ex.Message}");
                // Initialize empty lists to prevent null reference exceptions
                ViewBag.Categories = new List<Category>();
                ViewBag.Books = new List<Book>();
                return View();
            }
        }

        // Get books by category (API endpoint if needed)
        public async Task<IActionResult> GetBooksByCategory(int categoryId)
        {
            var books = await context.Book
                .Where(b => b.CategoryId == categoryId)
                .Include(b => b.Category)
                .Include(b => b.Author)
                .ToListAsync();

            return Json(books);
        }
        
        // Read the PDF file of the book
        public async Task<IActionResult> ReadPdf(int id)
        {
            var book = await context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
    }
}