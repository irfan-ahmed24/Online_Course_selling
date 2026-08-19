using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace My_project.Middlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower() ?? "";
            if (path.StartsWith("/login") || path.StartsWith("/register") || path.StartsWith("/teacherregister") || path.StartsWith("/home") || path == "/" || path.StartsWith("/css") || path.StartsWith("/js") || path.StartsWith("/lib"))
            {
                await _next(context);
                return;
            }
            string userName = context.Session.GetString("UserName") ?? "";
            string userRole = context.Session.GetString("UserRole") ?? "";
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userRole))
            {
                context.Response.Redirect("/Login");
                return;
            }
            if (path.StartsWith("/teachers") && userRole != "Teacher" && userRole != "Admin")
            {
                context.Response.Redirect("/Login");
                return;
            }
            if (path.StartsWith("/admin") && userRole != "Admin")
            {
                context.Response.Redirect("/Login");
                return;
            }
            if (path.StartsWith("/student") && userRole != "Student" && userRole != "Admin")
            {
                context.Response.Redirect("/Login");
                return;
            }
            await _next(context);
        }
    }
}