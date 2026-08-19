using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers
{
    [Route("Admin/Profile")]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. PROFILE GET (ডেটাবেস থেকে ইউজার ডেটা এনে ভিউতে পাঠানো) ---
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Index", "Login");

            var adminUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (adminUser == null) return NotFound();

            return View("~/Views/Admin/Profile.cshtml", adminUser);
        }

        // --- 2. UPDATE PROFILE POST (নাম ও ইমেইল আপডেট করা) ---
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateProfile(User model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Index", "Login");

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (existingUser == null) return NotFound();

            // প্রপার্টি আপডেট করা
            existingUser.FullName = model.FullName;
            existingUser.Email = model.Email;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            // সেশনের নাম আপডেট করা যাতে লেআউটে সাথে সাথে পরিবর্তন দেখা যায়
            HttpContext.Session.SetString("UserName", existingUser.FullName);

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToAction("Index");
        }

        // --- 3. CHANGE PASSWORD POST (পাসওয়ার্ড পরিবর্তন করা) ---
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Index", "Login");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId.Value);
            if (user == null) return NotFound();

            // বর্তমান পাসওয়ার্ড ভেরিফাই করা
            var passwordHasher = new PasswordHasher<object>();
            var verificationResult = passwordHasher.VerifyHashedPassword(null!, user.Password, CurrentPassword);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                TempData["ErrorMessage"] = "Current password is incorrect!";
                return RedirectToAction("Index");
            }

            if (NewPassword != ConfirmPassword)
            {
                TempData["ErrorMessage"] = "New passwords do not match!";
                return RedirectToAction("Index");
            }

            // নতুন পাসওয়ার্ড হ্যাশ করে সেভ করা
            user.Password = passwordHasher.HashPassword(null!, NewPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Password changed successfully!";
            return RedirectToAction("Index");
        }
    }
}