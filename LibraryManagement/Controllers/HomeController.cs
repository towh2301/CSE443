using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using LibraryManagement.Interfaces;
using LibraryManagement.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers
{
    [Authorize]
    public class HomeController(ILogger<HomeController> logger, IAuthService authService) : Controller
    {
        public IActionResult Index()
        {
            // if (!authService.IsUserLoggedIn())
            // {
            //     return RedirectToAction("Login", "Account");
            // }

            // logger.LogInformation("User {user} is logged in", authService.GetCurrentUserName() ?? "unknown");

            var viewModel = new HomeViewModel
            {
                UserName = authService.GetCurrentUserName(),
                UserRole = authService.GetUserRole(),
                IsAuthenticated = authService.IsUserLoggedIn()
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
