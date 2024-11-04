using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            // Check if the user didn't login
            // Check if the user didn't login
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login");
            //}
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Profile()
        {
            return View();
        }

    }
}
