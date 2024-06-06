using Ekip2.Application.Services.PackageServices;
using Ekip2.Presentation.Areas.Admin.Models.PackageVMs;

namespace Ekip2.Presentation.Areas.Manager.Controllers;

public class PackageController : ManagerBaseController
{
    private readonly IPackageService _packageService;
    private readonly IManagerService _managerService;
    public PackageController(IPackageService packageService, IManagerService manager)
    {
        _packageService = packageService;
        _managerService = manager;
    }

    public async Task<IActionResult> Index()
    {
        var packages = await _packageService.GetAllAsync();
        if (!packages.IsSuccess)
        {
            await Console.Out.WriteLineAsync(packages.Messages);
            return View(new List<PackageListVM>());
        }
        var packageVM = packages.Data.Adapt<List<PackageListVM>>();
        await Console.Out.WriteLineAsync(packages.Messages);
        return View(packageVM);
    }

   



}