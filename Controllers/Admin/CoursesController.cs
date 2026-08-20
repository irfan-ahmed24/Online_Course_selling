using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers
{
    [Route("Admin/Courses")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }
            var courses = await _context.Courses
                .Include(c => c.Teacher)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View("~/Views/Admin/Courses/Index.cshtml", courses);
        }
        [HttpPost("Approve/{id}")]
        public async Task<IActionResult> ApproveCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                course.IsCourseApproved = true;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Course '{course.Title}' has been approved successfully!";
            }
            return RedirectToAction("Index");
        }
        [HttpPost("Reject/{id}")]
        public async Task<IActionResult> RejectCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Course '{course.Title}' has been rejected and removed.";
            }
            return RedirectToAction("Index");
        }
    }
}