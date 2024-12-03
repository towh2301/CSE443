using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using LibraryManagement.Services;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController(ApplicationDbContext context, ILogger<AccountController> logger, IBookService bookService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminView()
        {
            return View();
        }

        // User View
        [HttpGet]
        public async Task<IActionResult> UserView()
        {
            var users = await context.Users.ToListAsync();
            
            // Parse List<User> to List<UserViewModel>
            var adminViewModel = new AdminViewModel
            {
                Users = users
            };
            
            return PartialView("~/Views/Shared/Components/AdminDashboard/_AdminUser.cshtml", adminViewModel);
        }
        
        // Loan View
        [HttpGet]
        public async Task<IActionResult> LoanView()
        {
            try
            {
                var loans = await context.Loan.ToListAsync();
                var books = await context.Book.ToListAsync();
                var users = await context.Users.ToListAsync();
            
                // Create admin loan view model
                var adminViewModel = new AdminViewModel
                {
                    Loans = loans,
                    Books = books,
                    Users = users
                };
            
                logger.LogInformation(adminViewModel.Loans.Count() + " loans found");
            
                return PartialView("~/Views/Shared/Components/AdminDashboard/_AdminLoan.cshtml", adminViewModel);
            } 
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in LoanList: {ex.Message}");
                // Initialize empty lists to prevent null reference exceptions
                ViewBag.Loans = new List<Loan>();
                return PartialView("~/Views/Shared/Components/AdminDashboard/_AdminLoan.cshtml");
            }
            
        }
        
        
        [HttpGet]
        public async Task<IActionResult> BookView(int? categoryId = null)
        {
            logger.LogInformation("Getting books for category {categoryId}", categoryId);
            try
            {
                var categories = await bookService.GetCategoriesAsync();
                var authors = await bookService.GetAuthorsAsync();
                var booksQuery = bookService.getBooksAsyncQueryable();
                
                // Create admin book view model
                var adminBookViewModel = new AdminViewModel
                {
                    Books = booksQuery.ToList(),
                    Categories = categories.ToList(),
                    Authors = authors.ToList()
                };
                
                // Filter by category if specified
                if (categoryId > 0)
                {
                    booksQuery = booksQuery.Where(b => b.CategoryId == categoryId);
                    ViewBag.CurrentCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
                }

                // Execute query and get books
                var books = await booksQuery.ToListAsync();
                
                logger.LogInformation("Found {bookCount} books", books.Count);
                
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
                
                // Return the partial view for AJAX requests
                return PartialView("~/Views/Shared/Components/AdminDashboard/_AdminBook.cshtml", adminBookViewModel);
                //return View();
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
    }
}
