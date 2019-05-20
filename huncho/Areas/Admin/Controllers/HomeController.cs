using huncho.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace huncho.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private Person[] data = new Person[] 
        {
             new Person { Name = "Alice", City = "London" },
             new Person { Name = "Bob", City = "Paris" },
             new Person { Name = "Joe", City = "New York" }
         };

        public ViewResult Index() => View(data);
    }
}
