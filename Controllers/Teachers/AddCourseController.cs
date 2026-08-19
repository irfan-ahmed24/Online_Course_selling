using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("Teachers/AddCourse")]
        public IActionResult Index()
        {
            // সিকিউরিটি চেক: লগইন ছাড়া কেউ এই পেজ দেখতে পারবে না
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }

        [HttpPost]
        [Route("Teachers/AddCourse")]
        public async Task<IActionResult> AddCourse(Course course, IFormFile? ThumbnailImage, List<string> lectureTitles, List<string> videoUrls)
        {
            // ১. সেশন থেকে বর্তমান টিচারের আইডি নিশ্চিত করা
            int? teacherId = HttpContext.Session.GetInt32("UserId");
            if (teacherId == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                // ২. টিচারের আইডি কোর্সের সাথে যুক্ত করা
                course.TeacherId = teacherId.Value;

                // ৩. থাম্বনেইল ইমেজ আপলোড হ্যান্ডেল করা
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

                // ৪. মূল কোর্স সেভ করা
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                // ৫. প্লেলিস্টের ভিডিওগুলো সেভ করা
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
                }

                // ৬. মোট ভিডিও সংখ্যা আপডেট করা
                course.VideoCount = totalLectures;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Course and Playlist added successfully!";
                return RedirectToAction("Index", "MyCourses");
            }

            TempData["ErrorMessage"] = "Failed to add course! Please check your input.";
            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }
    }
}