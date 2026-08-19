using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;
using My_project.Models;

namespace My_project.controllers
{
    [Route("Admin/Dashboard")]
    public class AdminDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardController(ApplicationDbContext context)
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

            // ডেটাবেস থেকে প্রয়োজনীয় ডাটা ফেচ করা
            var pendingCoursesList = await _context.Courses
                .Include(c => c.Teacher) // কোর্সদাতার তথ্য পাওয়ার জন্য
                .Where(c => !c.IsCourseApproved)
                .ToListAsync();

            var pendingTeachersList = await _context.Users
                .Where(u => u.Role == "Teacher" && !u.IsApproved)
                .ToListAsync();

            var viewModel = new AdminDashboardViewModel
            {
                PendingCoursesCount = pendingCoursesList.Count,
                PendingTeachersCount = pendingTeachersList.Count,
                TotalEnrollments = 0, // এনরোলমেন্ট টেবিল থাকলে সেখানে লজিক বসাবেন
                TotalRevenue = 0.0m,  // পেমেন্ট সিস্টেম থাকলে তার হিসাব
                PendingCourses = pendingCoursesList,
                PendingTeachers = pendingTeachersList
            };

            return View("~/Views/Admin/Dashboard/Index.cshtml", viewModel);
        }

        // ড্যাশবোর্ড থেকে কোর্স অ্যাপ্রুভ করার অ্যাকশন
        [HttpPost("ApproveCourse/{id}")]
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

        // ড্যাশবোর্ড থেকে শিক্ষক অ্যাপ্রুভ করার অ্যাকশন
        [HttpPost("ApproveTeacher/{id}")]
        public async Task<IActionResult> ApproveTeacher(int id)
        {
            var teacher = await _context.Users.FindAsync(id);
            if (teacher != null && teacher.Role == "Teacher")
            {
                teacher.IsApproved = true;
                _context.Users.Update(teacher);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Teacher '{teacher.FullName}' has been verified successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}