using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers
{
    [Route("TeacherRegister")]
    public class TeacherRegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TeacherRegisterController(ApplicationDbContext context)
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

            var newTeacher = new User
            {
                FullName = FullName,
                Email = Email,
                Password = hashedPassword,
                Role = "Teacher",
                IsApproved = false // --- এখানে IsApproved false করে দেওয়া হলো ---
            };

            _context.Users.Add(newTeacher);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Teacher registration successful! Please wait for admin approval to login.";
            return RedirectToAction("Index", "Login");
        }
    }
}