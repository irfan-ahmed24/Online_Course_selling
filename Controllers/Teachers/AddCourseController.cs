using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;
using My_project.Models;

namespace My_project.Controllers
{
    public class AddCourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AddCourseController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // --- 1. ADD COURSE GET ---
        [HttpGet]
        [Route("Teachers/AddCourse")]
        public IActionResult Index()
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }

        // --- 2. ADD COURSE POST ---
        [HttpPost]
        [Route("Teachers/AddCourse")]
        public async Task<IActionResult> AddCourse(Course course, IFormFile? ThumbnailImage, List<string> lectureTitles, List<string> videoUrls)
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                course.TeacherId = teacherId.Value;

                if (ThumbnailImage != null && ThumbnailImage.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/thumbnails");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ThumbnailImage.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ThumbnailImage.CopyToAsync(fileStream);
                    }
                    course.ThumbnailUrl = "/uploads/thumbnails/" + uniqueFileName;
                }

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                int totalLectures = 0;
                if (lectureTitles != null && videoUrls != null)
                {
                    for (int i = 0; i < lectureTitles.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(lectureTitles[i]) && !string.IsNullOrEmpty(videoUrls[i]))
                        {
                            var lecture = new CourseLecture
                            {
                                LectureTitle = lectureTitles[i],
                                VideoUrl = videoUrls[i],
                                CourseId = course.Id
                            };
                            _context.CourseLectures.Add(lecture);
                            totalLectures++;
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                course.VideoCount = totalLectures;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Course and Playlist added successfully!";
                return RedirectToAction("Index", "MyCourses");
            }

            TempData["ErrorMessage"] = "Failed to add course! Please check your input.";
            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }

        // --- 3. EDIT COURSE GET ---
        [HttpGet]
        [Route("Teachers/EditCourse/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            var course = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacherId.Value);

            if (course == null) return NotFound();

            return View("~/Views/Teachers/My Courses/EditCourse.cshtml", course);
        }

        // --- 4. EDIT COURSE POST ---
        [HttpPost]
        [Route("Teachers/EditCourse/{id}")]
        public async Task<IActionResult> Edit(int id, Course course, IFormFile? ThumbnailImage, List<string> lectureTitles, List<string> videoUrls)
        {
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            // ডেটাবেস থেকে বর্তমান কোর্সটি নিয়ে আসা
            var existingCourse = await _context.Courses
                .Include(c => c.Lectures)
                .FirstOrDefaultAsync(c => c.Id == id && c.TeacherId == teacherId.Value);

            if (existingCourse == null) return NotFound();

            // প্রপার্টিগুলো ম্যানুয়ালি আপডেট করা
            existingCourse.Title = course.Title;
            existingCourse.Category = course.Category;
            existingCourse.Price = course.Price;
            existingCourse.Description = course.Description;

            // নতুন থাম্বনেইল আপলোড করা হলে পরিবর্তন হবে, না হলে আগেরটা থাকবে
            if (ThumbnailImage != null && ThumbnailImage.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/thumbnails");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ThumbnailImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ThumbnailImage.CopyToAsync(fileStream);
                }
                existingCourse.ThumbnailUrl = "/uploads/thumbnails/" + uniqueFileName;
            }
            else if (!string.IsNullOrEmpty(course.ThumbnailUrl))
            {
                existingCourse.ThumbnailUrl = course.ThumbnailUrl;
            }

            // পুরনো লেকচারগুলো রিমুভ করা
            if (existingCourse.Lectures != null && existingCourse.Lectures.Any())
            {
                _context.CourseLectures.RemoveRange(existingCourse.Lectures);
            }

            // নতুন লেকচারগুলো যোগ করা
            int totalLectures = 0;
            if (lectureTitles != null && videoUrls != null)
            {
                for (int i = 0; i < lectureTitles.Count; i++)
                {
                    if (!string.IsNullOrEmpty(lectureTitles[i]) && !string.IsNullOrEmpty(videoUrls[i]))
                    {
                        var lecture = new CourseLecture
                        {
                            LectureTitle = lectureTitles[i],
                            VideoUrl = videoUrls[i],
                            CourseId = existingCourse.Id
                        };
                        _context.CourseLectures.Add(lecture);
                        totalLectures++;
                    }
                }
            }

            existingCourse.VideoCount = totalLectures;
            existingCourse.TeacherId = teacherId.Value;

            // ট্র্যাকিং ঝামেলা এড়াতে স্টেট মডিফাইড বলে দেওয়া
            _context.Entry(existingCourse).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Course updated successfully!";
            return RedirectToAction("Index", "MyCourses");
        }
    }
}