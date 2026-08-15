using Microsoft.AspNetCore.Mvc;
namespace My_project.controllers
{
    [Route("Admin/Profile")]
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Admin/Profile.cshtml");
        }
    }
}