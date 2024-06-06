using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ekip2.Presentation.Controllers;

public class BaseController : Controller
{
    //protected INotyfService notyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;

    //protected void NotifySuccess(string message)
    //{
    //    notyfService.Success(message);
    //}
    //protected void NotifyError(string message)
    //{
    //    notyfService.Error(message);
    //}
    //protected void NotifyWarning(string message)
    //{
    //    notyfService.Warning(message);
    //}
}
