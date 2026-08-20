using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;

namespace My_project.controllers
{
    [Route("Students/EnrolledCourses")]
    public class EnrolledCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrolledCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // সেশন চেক (স্টুডেন্ট লগইন করা আছে কিনা)
            if (HttpContext.Session.GetString("UserRole") != "Student")
            {
                return RedirectToAction("Index", "Login");
            }

            var studentId = HttpContext.Session.GetInt32("UserId");
            if (studentId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            // ডেটাবেস থেকে স্টুডেন্টের এনরোল করা কোর্সগুলো ফেচ করা
            var enrolledCourses = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course) // Course টেবিলের সাথে রিলেশন যুক্ত করা
                .Select(e => e.Course)
                .ToListAsync();

            return View("~/Views/Students/EnrolledCourses/Index.cshtml", enrolledCourses);
        }
    }
}