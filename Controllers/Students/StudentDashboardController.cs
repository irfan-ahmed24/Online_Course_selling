using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;

namespace My_project.controllers
{
    [Route("Students/Dashboard")]
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
            {
                return RedirectToAction("Index", "Login");
            }

            var studentId = HttpContext.Session.GetInt32("UserId");
            if (studentId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // ১. যে কোর্সগুলোতে স্টুডেন্ট অলরেডি এনরোল করেছে (Enrollments টেবিল থেকে)
            var enrolledCourses = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course) // Course টেবিলের ডেটা পাওয়ার জন্য
                .Select(e => e.Course)
                .ToListAsync();

            // ২. যে কোর্সগুলোর আইডি এনরোল করা হয়ে গেছে, সেগুলোর আইডি বের করা
            var enrolledCourseIds = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.CourseId)
                .ToListAsync();

            // ৩. পপুলার বা বাকি কোর্সগুলো যেগুলো স্টুডেন্ট এখনো কেনেনি
            var popularCourses = await _context.Courses
                .Where(c => !enrolledCourseIds.Contains(c.Id))
                .Take(6) // সর্বোচ্চ ৬টি দেখাতে পারেন
                .ToListAsync();

            // ভিউতে ডেটা পাঠানোর জন্য একটি ViewModel বা ViewBag ব্যবহার করতে পারেন, 
            // অথবা সরাসরি ViewBag-এ পাঠাতে পারেন। এখানে আমরা ViewBag বা Model-এর মাধ্যমে পাঠাচ্ছি।
            ViewBag.EnrolledCourses = enrolledCourses;
            ViewBag.PopularCourses = popularCourses;

            return View("~/Views/Students/Dashboard/Index.cshtml");
        }
    }
}