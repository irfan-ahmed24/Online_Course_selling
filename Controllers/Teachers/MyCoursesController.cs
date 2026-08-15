using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    [Route("Teachers/MyCourses")]
    public class MyCoursesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teachers/My Courses/MyCourses.cshtml");
        }
    }
}