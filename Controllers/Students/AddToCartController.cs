using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_project.Data;
using My_project.Models;
using System.Security.Claims;

namespace My_project.Controllers.Students
{
    [Authorize]
    [Route("Students/AddToCart")]
    public class AddToCartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddToCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(rawUserId) || !int.TryParse(rawUserId, out int userId))
            {
                return Redirect("/Identity/Account/Login");
            }

            var cartItems = await _context.CartItems
                .Include(c => c.Course)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return View("~/Views/Students/AddToCart/Index.cshtml", cartItems);
        }

        [HttpPost("Add/{courseId}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Add(int courseId)
        {
            try
            {
                var rawUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(rawUserId) || !int.TryParse(rawUserId, out int userId))
                {
                    return Json(new { success = false, message = "Unauthorized user or invalid ID!" });
                }

                // চেক করি কোর্সটি অলরেডি কার্টে আছে কি না
                var existing = await _context.CartItems
                    .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId);

                if (existing != null)
                {
                    return Json(new { success = false, message = "Already in cart!" });
                }

                var cartItem = new CartItem
                {
                    UserId = userId,
                    CourseId = courseId,
                    DateAdded = DateTime.Now // 👈 ডেটাবেজের DateAdded ফিল্ডের নাল এরর এড়ানোর জন্য এটি দেওয়া হলো
                };

                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();

                int totalCount = await _context.CartItems.CountAsync(c => c.UserId == userId);

                return Json(new { success = true, message = "Successfully added to cart!", cartCount = totalCount });
            }
            catch (Exception ex)
            {
                string detailedError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "Database Error: " + detailedError });
            }
        }

        [HttpPost("Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}