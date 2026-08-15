using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    [Route("Teachers/Profile")]
    public class Profile : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teachers/Profile/Profile.cshtml");
        }
    }
}