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
            var courses = await _context.Courses.OrderByDescending(c => c.CreatedAt).ToListAsync();

            return View("~/Views/Teachers/My Courses/MyCourses.cshtml", courses);
        }

        [HttpGet("CourseDetails/{id}")]
        public async Task<IActionResult> CourseDetails(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            return View("~/Views/Teachers/My Courses/CourseDetails.cshtml", course);
        }

        [HttpPost("CourseDetails/AddLecture")]
        public async Task<IActionResult> AddLecture(int CourseId, string LectureTitle, string VideoUrl)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == CourseId);
            if (course == null)
            {
                return NotFound();
            }
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

        // কোর্স এবং তার আন্ডারে থাকা সব লেকচার ডিলিট করার জন্য পোস্ট মেথড
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            // লেকচারগুলো রিমুভ করা
            if (course.Lectures != null && course.Lectures.Any())
            {
                _context.CourseLectures.RemoveRange(course.Lectures);
            }

            // কোর্স রিমুভ করা
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Course and all its lectures deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}