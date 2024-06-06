using Microsoft.AspNetCore.Mvc;

namespace Ekip2.Presentation.Areas.Employee.Controllers
{
    public class HomeController : EmployeeBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
