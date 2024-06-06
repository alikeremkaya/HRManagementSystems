using Microsoft.AspNetCore.Mvc;

namespace Ekip2.Presentation.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Employee")]
    public class EmployeeBaseController : BaseController
    {

    }
}
