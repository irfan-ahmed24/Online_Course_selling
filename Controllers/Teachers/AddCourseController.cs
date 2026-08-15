using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    [Route("Teachers/AddCourse")]
    public class AddCourseController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teachers/My Courses/AddCourse.cshtml");
        }
    }
}