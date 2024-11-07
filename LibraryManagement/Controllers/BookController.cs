using LibraryManagement.Models;
using LibraryManagement.Models.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    //[Authorize]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public BookController(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BookDetail(int id)
        {
            var book = await _dbContext.Book.FindAsync(id); // Fetch the book by ID
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult BookList()
        {
            return ShowBookListFromDb();
        }

        public IActionResult ShowBookListFromDb()
        {
            var books = _dbContext.Book.ToList();
            ViewBag.Books = books;
            return View();

        }
    }
}
