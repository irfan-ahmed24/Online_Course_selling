using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data; // আপনার প্রজেক্টের ডেটাবেস নেমস্পেস এখানে থাকবে
using System.Threading.Tasks;

namespace My_project.controller
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // ডেটাবেস কন্টেক্সট ইনজেক্ট করা হলো
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // ডেটাবেস থেকে লেটেস্ট ৩টি কোর্স নিয়ে আসা হচ্ছে
            var featuredCourses = await _context.Courses
                .Include(c => c.Teacher)
                .Take(3)
                .ToListAsync();

            return View(featuredCourses);
        }
    }
}