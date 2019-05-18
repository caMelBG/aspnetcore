using Microsoft.AspNetCore.Mvc;

namespace huncho.Components
{
    public class ErrorController : Controller
    {
        public ViewResult Error() => View();
    }
}