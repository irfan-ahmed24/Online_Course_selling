using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    [Route("Students/Profile")]
    public class StudentProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Students/Profile/Index.cshtml");
        }
    }
}