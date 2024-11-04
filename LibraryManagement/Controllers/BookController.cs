using LibraryManagement.Models;
using LibraryManagement.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
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

        public IActionResult BookDetail()
        {
            return View();
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
