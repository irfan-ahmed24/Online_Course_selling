using Microsoft.AspNetCore.Mvc;
namespace My_project.controllers
{
    [Route("Admin/Settings")]
    public class SettingsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Admin/Settings/Index.cshtml");
        }
    }
}