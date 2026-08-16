using Microsoft.AspNetCore.Mvc;
namespace My_project.controllers
{
    [Route("Students/EnrolledCourses")]
    public class EnrolledCoursesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Students/EnrolledCourses/Index.cshtml");
        }
    }
}