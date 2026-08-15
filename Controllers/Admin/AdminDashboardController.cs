using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{

    [Route("Admin/Dashboard")]
    public class AdminDashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
            {
                return RedirectToAction("Index", "Login");
            }
            return View("~/Views/Admin/Dashboard/Index.cshtml");
        }
    }
}