using Microsoft.AspNetCore.Mvc;
using My_project.Models;
using My_project.Data;

namespace My_project.controllers
{
    [Route("Admin/UserManage")]
    public class UserManageController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Admin/UserManage/Index.cshtml");
        }
        [HttpGet("Add")]
        public IActionResult AddUser()
        {
            return View("~/Views/Admin/UserManage/AddUser.cshtml");
        }
        private readonly ApplicationDbContext _context;

        public UserManageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser(User model, string Password)
        {
            if (ModelState.IsValid)
            {
                bool userExists = _context.Users.Any(u => u.Email == model.Email);
                if (userExists)
                {
                    TempData["ErrorMessage"] = "This email address is already registered!";
                    return View("~/Views/Admin/UserManage/AddUser.cshtml", model);
                }
                var passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
                model.Password = passwordHasher.HashPassword(model, Password);
                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "User added successfully!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Please fill up all required fields correctly.";
            return View("~/Views/Admin/UserManage/AddUser.cshtml", model);
        }
    }
}