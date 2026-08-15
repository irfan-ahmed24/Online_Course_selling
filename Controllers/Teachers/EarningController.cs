using Microsoft.AspNetCore.Mvc;

namespace My_project.controllers
{
    [Route("Teachers/Earnings")]
    public class EarningsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/Teachers/Earnings.cshtml");
        }
    }
}