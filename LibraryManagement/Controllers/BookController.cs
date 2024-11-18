using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class BookController(ApplicationDbContext context) : Controller
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
            try
            {
                // Get all categories for the navigation
                var categories = await (context.Category?.ToListAsync() ?? Task.FromResult(new List<Category>()));
                ViewBag.Categories = categories;
                ViewBag.CurrentCategoryId = categoryId;

                // Get books query
                var booksQuery = context.Book
                    .Include(b => b.Category)
                    .AsQueryable();

                // Filter by category if specified
                if (categoryId.HasValue && categoryId > 0)
                {
                    booksQuery = booksQuery.Where(b => b.CategoryId == categoryId);
                    ViewBag.CurrentCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
                }

                // Execute query and get books
                var books = await booksQuery.ToListAsync();
                ViewBag.Books = books ?? new List<Book>();

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