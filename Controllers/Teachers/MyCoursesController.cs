using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data; // আপনার DbContext এর নেমস্পেস

namespace My_project.Controllers
{
    [Route("Teachers/MyCourses")]
    public class MyCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyCoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // ডেটাবেস থেকে সব কোর্স লিস্ট আকারে নিয়ে আসা
            var courses = await _context.Courses.OrderByDescending(c => c.CreatedAt).ToListAsync();

            return View("~/Views/Teachers/My Courses/MyCourses.cshtml", courses);
        }
    }
}