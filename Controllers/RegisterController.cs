using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string FullName, string Email, string Password, string ConfirmPassword)
        {
            if (Password != ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match. Please try again.";
                return View();
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == Email);
            if (existingUser != null)
            {
                TempData["ErrorMessage"] = "This email is already registered. Please try logging in.";
                return View();
            }

            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(new User(), Password);

            var newUser = new User
            {
                FullName = FullName,
                Email = Email,
                Password = hashedPassword,
                Role = "Student",          // রোল স্টুডেন্ট সেট করা হলো
                IsApproved = true          // স্টুডেন্টের ক্ষেত্রে অটো অ্যাপ্রুভ true করে দেওয়া হলো
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Registration successful! Please login to your account.";

            return RedirectToAction("Index", "Login");
        }
    }
}