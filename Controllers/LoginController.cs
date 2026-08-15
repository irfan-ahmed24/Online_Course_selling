using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_project.Data;
using My_project.Models;

namespace My_project.controller
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == username || u.FullName == username);

            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found. Please register first.";
                return View();
            }
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (result == PasswordVerificationResult.Failed)
            {
                TempData["ErrorMessage"] = "Invalid password. Please try again.";
                return View();
            }
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FullName);

            TempData["SuccessMessage"] = $"Welcome back, {user.FullName}!";
            if (user.Role == "Admin")
            {
                return RedirectToAction("Index", "AdminDashboard");
            }
            else if (user.Role == "Teacher")
            {
                return RedirectToAction("Index", "TeacherDashboard");
            }
            else if (user.Role == "Student")
            {
                return RedirectToAction("Index", "StudentDashboard");
            }
            else
            {
                TempData["ErrorMessage"] = "Unauthorized role type!";
                return View();
            }
        }
    }
}