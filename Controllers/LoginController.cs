using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers // অথবা আপনার প্রোজেক্টের স্পেলিং অনুযায়ী controller/controllers রাখবেন
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

            // --- এখানে শিক্ষকের অ্যাপ্রুভাল চেক করা হচ্ছে ---
            if (user.Role == "Teacher" && !user.IsApproved)
            {
                TempData["ErrorMessage"] = "Your account is pending admin approval. Please wait until an admin verifies your account.";
                return View();
            }
            // -------------------------------------------------

            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FullName);
            HttpContext.Session.SetInt32("UserId", user.Id);

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