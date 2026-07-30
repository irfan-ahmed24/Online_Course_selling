using Microsoft.AspNetCore.Mvc;

namespace My_project.controller
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}