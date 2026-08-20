using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers
{
    [Route("Students/Profile")]
    public class StudentProfileController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // সেশন থেকে UserId নেওয়া হচ্ছে (যেহেতু লগইন কন্ট্রোলারে UserId সেট করা হয়েছে)
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login"); // লগইন না থাকলে রিডাইরেক্ট হবে
            }

            // ডেটাবেস থেকে ইউজার আইডি দিয়ে তথ্য খোঁজা
            var student = await _context.Users
                .FirstOrDefaultAsync(s => s.Id == userId);

            if (student == null)
            {
                return NotFound("Student profile not found.");
            }

            return View("~/Views/Students/Profile/Index.cshtml", student);
        }

        // প্রফাইল ইনফরমেশন আপডেট করার জন্য পোস্ট মেথড
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProfile(string FullName, string Email)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var student = await _context.Users.FirstOrDefaultAsync(s => s.Id == userId);

            if (student != null)
            {
                student.FullName = FullName;
                student.Email = Email;

                _context.Users.Update(student);
                await _context.SaveChangesAsync();

                // সেশনের নাম আপডেট করা যাতে টপবার বা সাইডবারে নতুন নাম দেখা যায়
                HttpContext.Session.SetString("UserName", FullName);

                TempData["SuccessMessage"] = "Profile updated successfully!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string CurrentPassword, string NewPassword, string ConfirmPassword)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var student = await _context.Users.FirstOrDefaultAsync(s => s.Id == userId);

            if (student != null)
            {
                var passwordHasher = new PasswordHasher<User>();


                // ১. বর্তমান পাসওয়ার্ড হ্যাশড ভ্যালুর সাথে মিলিয়ে চেক করা
                var verificationResult = passwordHasher.VerifyHashedPassword(student, student.Password, CurrentPassword);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    TempData["ErrorMessage"] = "Current password is incorrect.";
                    return RedirectToAction("Index");
                }

                // ২. নতুন পাসওয়ার্ড এবং কনফার্ম পাসওয়ার্ড মিলছে কিনা চেক করা
                if (NewPassword != ConfirmPassword)
                {
                    TempData["ErrorMessage"] = "New password and confirm password do not match.";
                    return RedirectToAction("Index");
                }

                // ৩. নতুন পাসওয়ার্ড হ্যাশ করে ডেটাবেসে সেট করা
                student.Password = passwordHasher.HashPassword(student, NewPassword);

                _context.Users.Update(student);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Password changed successfully!";
            }

            return RedirectToAction("Index");
        }
    }
}