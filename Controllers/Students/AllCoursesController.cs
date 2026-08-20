using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;

namespace My_project.controllers
{
    [Route("Students/AllCourses")]
    public class AllCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AllCoursesController(ApplicationDbContext context)
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
            var courses = await _context.Courses
                .Include(c => c.Teacher)
                .Where(c => c.IsCourseApproved == true) // অথবা c.IsApproved == true (আপনার ডেটাবেসের ফিল্ড অনুযায়ী ঠিক করে নেবেন)
                .ToListAsync();

            return View("~/Views/Students/AllCourses/Index.cshtml", courses);
        }

        // নির্দিষ্ট কোর্সের বিস্তারিত দেখার জন্য মেথড
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(c => c.Id == id && c.IsCourseApproved == true); // নিশ্চিত করা হলো যেন আনপ্রুভড কোর্স ডিটেইলেও দেখা না যায়

            if (course == null)
            {
                return NotFound();
            }

            return View("~/Views/Students/AllCourses/CourseView.cshtml", course);
        }
    }
}