using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{

    [Route("Students/Dashboard")]
    public class StudentDashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
            {
                return RedirectToAction("Index", "Login");
            }
            return View("~/Views/Students/Dashboard/Index.cshtml");
        }
    }

}