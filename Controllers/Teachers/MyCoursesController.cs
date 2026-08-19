using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;
using My_project.Models;

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
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            var courses = await _context.Courses
                .Where(c => c.TeacherId == teacherId.Value)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View("~/Views/Teachers/My Courses/MyCourses.cshtml", courses);
        }

        [HttpGet("CourseDetails/{id}")]
        public async Task<IActionResult> CourseDetails(int id)
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            var course = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacherId.Value);

            if (course == null) return NotFound();

            return View("~/Views/Teachers/My Courses/CourseDetails.cshtml", course);
        }

        [HttpPost("CourseDetails/AddLecture")]
        public async Task<IActionResult> AddLecture(int CourseId, string LectureTitle, string VideoUrl)
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == CourseId && c.TeacherId == teacherId.Value);

            if (course == null) return NotFound();

            var lecture = new CourseLecture
            {
                CourseId = CourseId,
                LectureTitle = LectureTitle,
                VideoUrl = VideoUrl
            };

            _context.CourseLectures.Add(lecture);
            course.VideoCount += 1;
            _context.Courses.Update(course);

            await _context.SaveChangesAsync();
            return RedirectToAction("CourseDetails", new { id = CourseId });
        }

        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            var course = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacherId.Value);

            if (course == null) return NotFound();

            if (course.Lectures != null && course.Lectures.Any())
            {
                _context.CourseLectures.RemoveRange(course.Lectures);
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Course and all its lectures deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}