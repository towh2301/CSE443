using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    
    public class AccountController(
        ApplicationDbContext context,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILogger<AccountController> logger)
        : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            // Check if the user didn't login
            return !User.Identity!.IsAuthenticated ? RedirectToAction("Home") : RedirectToAction("Login");
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state during login");
                return View(model);
            }
    
            var user = await userManager.FindByEmailAsync(model.Email);
    
            if (user == null)
            {
                logger.LogWarning("Login attempt with non-existent email: {Email}", model.Email);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
            
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true);
            
            if (result.Succeeded)
            {
                logger.LogInformation("User logged in successfully: {Email}", user.Email);
        
                //Using Session
                HttpContext.Session.SetString("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.Email!);
                
                return RedirectToAction("Index", "Home");
            }
    
            logger.LogWarning("Failed login attempt for user: {Email}", model.Email);
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new User()
            {
                FullName = model.FullName,
                Email = model.Email,
                UserName = model.Email,
                Role = "User",
                PhoneNumber = "1234567890",
                Address = "123 Main",
                IsActive = true,
                Status = 1,
                CreatedDate = DateTime.Now,
                ActiveCode = "123456",
                Avatar =
                    "https://fastly.picsum.photos/id/825/200/300.jpg?hmac=02AaqBOvx8gwrGt7a3HWzJHnZXrMzYoXbAYwjJWH40E",
                Description = "This is a test user",
                UserCode = "123456",
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Add to default role
                await userManager.AddToRoleAsync(user, "user");

                // Send email confirmation
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                // Send email with confirmation link
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token },
                    Request.Scheme);
                logger.LogInformation("Confirmation link: {ConfirmationLink}", confirmationLink);

                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            await context.SaveChangesAsync();
            return View(model);
        }

        public IActionResult ChangePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(new ChangePasswordViewModel { Email= username });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong. try again.");
                return View(model);
            }

            var user = await userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email not found!");
                return View(model);
            }

            var removeResult = await userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
            {
                foreach (var error in removeResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            var addResult = await userManager.AddPasswordAsync(user, model.NewPassword);
            if (addResult.Succeeded) return RedirectToAction("Login", "Account");
            {
                foreach (var error in addResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        
        public async Task<IActionResult> Profile(String? id)
        {
            if (id != null)
            {
                var user = await userManager.FindByIdAsync(id);
                var loans = await context.Loan.Where(l => l.UserId == id).ToListAsync();
                var books = await context.Book.ToListAsync();
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                
                // Create Profile View Model
                var profileViewModel = new ProfileViewModel
                {
                    Loans = loans,
                    User = user,
                    Books = books
                };
                
            
                return View(profileViewModel);
            }
            return RedirectToAction("Login", "Account");
        }
        
        [HttpDelete]
        public async Task<JsonResult> DeleteAccount([FromQuery] string userEmail)
        {
            logger.LogInformation(userEmail);
            // Find user by id
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null) return Json(new { success = false, message = "User not found." });
            
            // Soft delete
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded) return Json(new { success = true, message = "User delete successfully!" });
            
            // Return errors
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return Json(new { success = false, message = "Failed to delete user." });
        }
        
        
        // Create account for admin
        [HttpPost]
        public async Task<JsonResult> CreateAccount([FromBody] UserViewModel? userViewModel)
        {
            if (userViewModel == null) return Json(new { success = false, message = "Invalid model state" });
            
            var newUser = new User
            {
                FullName = userViewModel.FullName,
                Email = userViewModel.Email,
                UserName = userViewModel.Email,
                Role = "User",
                PhoneNumber = "1234567890",
                Address = "123 Main",
                IsActive = true,
                Status = 1,
                CreatedDate = DateTime.Now,
                ActiveCode = "123456",
                Avatar = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava1-bg.webp",
                Description = "This is a test user",
                UserCode = "123456",
            };
            
            var result = await userManager.CreateAsync(newUser, userViewModel.Password);
            if (result.Succeeded)
            {
                // Add to default role
                await userManager.AddToRoleAsync(newUser, "user");
                return Json(new { success = true, message = "User added successfully" });
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            
            return Json(new { success = false, message = "Failed to add user" });
        }
        
        // Get account by email
        [HttpGet]
        public async Task<JsonResult> GetAccountDetails([FromQuery] string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            return user == null ? Json(new { success = false, message = "User not found" }) : Json(new { success = true, user });
        }
        
        // Update account details
        [HttpPut]
        public async Task<JsonResult> EditUser([FromBody] UserUpdateModel? userUpdateModel)
        {
            if (userUpdateModel == null) return Json(new { success = false, message = "Invalid model state" });
            
            var user = await userManager.FindByIdAsync(userUpdateModel.Id);
            if (user == null) return Json(new { success = false, message = "User not found" });
            
            try
            {
                user = userUpdateModel.EditUser(user, userUpdateModel);
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded) return Json(new { success = true, message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return Json(new { success = false, message = "Failed to update user" });
            }
            return Json(new { success = false, message = "Failed to update user" });
        }
    }
}
