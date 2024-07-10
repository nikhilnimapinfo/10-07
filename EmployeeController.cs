using Microsoft.AspNetCore.Mvc;

namespace JWT_Program.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
