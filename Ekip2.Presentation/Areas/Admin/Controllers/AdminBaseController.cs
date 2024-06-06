
using Microsoft.AspNetCore.Mvc;

namespace Ekip2.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminBaseController : BaseController
    {
       
    }
}
