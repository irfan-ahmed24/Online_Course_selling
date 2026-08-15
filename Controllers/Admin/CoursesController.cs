using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    [Route("Admin/Courses")]
    public class CoursesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Courses/Index.cshtml");
        }
    }
}