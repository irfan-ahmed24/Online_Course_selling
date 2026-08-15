using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{

    [Route("Teachers/Dashboard")]
    public class TeacherDashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teachers/Dashboard/Index.cshtml");
        }
    }
}