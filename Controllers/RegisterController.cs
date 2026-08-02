using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}