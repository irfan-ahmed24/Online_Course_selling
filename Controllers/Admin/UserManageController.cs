using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Models;
using My_project.Data;

namespace My_project.controllers
{
    [Route("Admin/UserManage")]
    public class UserManageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserManageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ডেটাবেস থেকে সব ইউজার এনে ভিউতে পাঠানো
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View("~/Views/Admin/UserManage/Index.cshtml", users);
        }

        [HttpGet("Add")]
        public IActionResult AddUser()
        {
            return View("~/Views/Admin/UserManage/AddUser.cshtml");
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser(User model, string Password)
        {
            if (ModelState.IsValid)
            {
                bool userExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
                if (userExists)
                {
                    TempData["ErrorMessage"] = "This email address is already registered!";
                    return View("~/Views/Admin/UserManage/AddUser.cshtml", model);
                }

                var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
                model.Password = passwordHasher.HashPassword(model, Password);

                // অ্যাডমিন প্যানেল থেকে ক্রিয়েট করলে অটো অ্যাপ্রুভ ধরে নিতে পারেন
                model.IsApproved = true;

                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "User added successfully!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Please fill up all required fields correctly.";
            return View("~/Views/Admin/UserManage/AddUser.cshtml", model);
        }

        // শিক্ষক বা ইউজারের অ্যাকাউন্ট অ্যাপ্রুভ করার অ্যাকশন
        [HttpPost("Approve/{id}")]
        public async Task<IActionResult> ApproveUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsApproved = true;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"{user.FullName}'s account has been approved!";
            }
            return RedirectToAction("Index");
        }

        // ইউজার ডিলিট করার অ্যাকশন
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "User deleted successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}