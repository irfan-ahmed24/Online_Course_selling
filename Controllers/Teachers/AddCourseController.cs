using Microsoft.AspNetCore.Mvc;
using My_project.Data;
using My_project.Models;

namespace My_project.Controllers
{
    public class AddCourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env; // ফাইল সেভ করার জন্য এনভায়রনমেন্ট ইনজেক্ট করা

        public AddCourseController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        [Route("Teachers/AddCourse")]
        public IActionResult Index()
        {
            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }

        [HttpPost]
        [Route("Teachers/AddCourse")]
        public async Task<IActionResult> AddCourse(Course course, IFormFile? ThumbnailImage, List<string> lectureTitles, List<string> videoUrls)
        {
            if (ModelState.IsValid)
            {
                // ১. থাম্বনেইল ইমেজ আপলোড হ্যান্ডেল করা
                if (ThumbnailImage != null && ThumbnailImage.Length > 0)
                {
                    // ইউনিক ফাইল নাম তৈরি (যেমন: uuid_filename.jpg)
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/thumbnails");

                    // যদি ফোল্ডার না থাকে তবে তৈরি করে নেবে
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ThumbnailImage.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // ফাইল সেভ করা
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ThumbnailImage.CopyToAsync(fileStream);
                    }

                    // ডেটাবেসে সেভ করার জন্য পাথ সেট করা
                    course.ThumbnailUrl = "/uploads/thumbnails/" + uniqueFileName;
                }

                // ২. মূল কোর্স সেভ করা (যাতে Course.Id জেনারেট হয়)
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                // ৩. প্লেলিস্টের ভিডিওগুলো সেভ করা এবং মোট ভিডিও সংখ্যা গণনা করা
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
                            totalLectures++; // সফলভাবে যোগ হওয়া লেকচার কাউন্ট বাড়বে
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                // ৪. মোট ভিডিও সংখ্যা কোর্সে আপডেট করে আবার সেভ করা
                course.VideoCount = totalLectures;
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Course and Playlist added successfully!";
                return RedirectToAction("Index", "MyCourses"); // সাবমিট হওয়ার পর সরাসরি MyCourses পেজে চলে যাবে
            }

            TempData["ErrorMessage"] = "Failed to add course!";
            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }
    }
}