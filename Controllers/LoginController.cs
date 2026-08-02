using Microsoft.AspNetCore.Mvc;

namespace My_project.controller
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}