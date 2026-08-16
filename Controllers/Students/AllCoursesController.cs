using Microsoft.AspNetCore.Mvc;
namespace My_project.controllers
{
    [Route("Students/AllCourses")]
    public class AllCoursesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Students/AllCourses/Index.cshtml");
        }
    }
}