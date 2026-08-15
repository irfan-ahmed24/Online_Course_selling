using Microsoft.AspNetCore.Mvc;

namespace Online_Course_selling.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}