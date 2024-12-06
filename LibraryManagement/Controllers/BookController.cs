using Azure.Core;
using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class BookController(ApplicationDbContext context, ILogger<BookController> logger, IBookService bookService, ILoanService loanService) : Controller
    {

        public IActionResult Index()
        {
            return Redirect("Book/BookList");
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
        
        // Get book details
        public async Task<JsonResult> GetBookDetails([FromQuery] int bookId)
        {
            var book = await context.Book.FindAsync(bookId);
            
            if(book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }
            
            // var category = await context.Category.FindAsync(book.CategoryId);
            // var author = await context.Author.FindAsync(book.AuthorId);
            //
            // var cate = category?.Name;
            // var au = author?.FirstName + " " + author?.LastName;
            
            return Json(new { success = true, book});
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
        
        // Add book method
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> AddBook([FromBody] BookViewModel? book)
        {
            if (book == null)
            {
                return Json(new { success = false, message = "Please check all input fields." });
            }
            var newBook = book.ToNewBook();
            
            await context.Book.AddAsync(newBook);
            await context.SaveChangesAsync();
            return Json(new { success = true, message = "Book added successfully" });
        }
        
        // Edit book method
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<JsonResult> EditBook([FromBody] BookViewModel? book)
        {
            logger.LogInformation(book?.BookId.ToString());
            // Validate input
            if (book == null)
            {
                return Json(new { success = false, message = "Please check all input fields." });
            }

            // Fetch the existing book from the database
            var currentBook = await context.Book.FindAsync(book.BookId);

            // Check if the book exists
            if (currentBook == null)
            {
                return Json(new { success = false, message = "The book does not exist." });
            }

            // Update the book using a transformation method
            try
            {
                currentBook = book.EditBook(currentBook, book);
                context.Book.Update(currentBook);
                await context.SaveChangesAsync();

                return Json(new { success = true, message = "Book edited successfully." });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return Json(new { success = false, message = "An error occurred while editing the book.", details = ex.Message });
            }
        }

        
        
        // Delete book method
        [HttpDelete]
        public async Task<JsonResult> DeleteBook([FromQuery] int bookId)
        {
            logger.LogInformation(bookId.ToString());
            var book = await context.Book.FindAsync(bookId);
            if (book == null)
            {
                return Json(new { success = false, message = "Book not found" });
            }

            context.Book.Remove(book);
            await context.SaveChangesAsync();
            return Json(new { success = true, message = "Book deleted successfully" });
        }
        
        
        [Authorize]
        public async Task<IActionResult>  Loan(int id)
        {
            var book = await context.Book.FindAsync(id);
            
            if(book == null)
            {
                return NotFound();
            }
            
            var viewModel = new LoanViewModel
            {
                UserId = User.Identity.GetUserId(),
                BookId = book.BookId,
                BookTitle = book.Title,
                LoanDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(14), // Default due date
            };
            
            return View(viewModel);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserLoan(LoanViewModel loanViewModel)
        {
            Console.WriteLine(("Loan View Model Here",loanViewModel.ToString()));
            //if (!ModelState.IsValid) return RedirectToAction("ModelErrors" );
            await loanService.CreateLoan(loanViewModel);
            return RedirectToAction("LoanConfirmation");
        }
    }
}