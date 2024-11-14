using System.Text;
using SCADA.Services;

namespace SCADA.Authorization
{
    public class Authorization
    {
        private readonly RequestDelegate _next;

        public Authorization(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().Contains("users/login"))
            {
                await _next.Invoke(context);
                return;
            }
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                string username = authHeader;
                try
                {
                   if(UserService.GetIsLoggedIn(username)) await _next.Invoke(context);
                   else
                   {
                       context.Response.StatusCode = 401;
                       context.Response.WriteAsync("Access denied!");
                   }
                }
                catch (ArgumentException)
                {
                    context.Response.StatusCode = 401;
                    context.Response.WriteAsync("Access denied!");
                }
            }
            else
            {
                context.Response.StatusCode = 401;
                context.Response.WriteAsync("Access denied!");
            }
        }
    }
}
