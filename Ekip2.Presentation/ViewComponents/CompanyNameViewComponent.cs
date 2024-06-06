using System.Security.Claims;

public class CompanyNameViewComponent : ViewComponent
{
    private readonly IManagerService _managerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CompanyNameViewComponent(IManagerService managerService, IHttpContextAccessor httpContextAccessor)
    {
        _managerService = managerService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);

        var companyName = managerResult?.Data?.CompanyName ?? "Şirket adı bulunamadı";
        return View("Default", companyName);
    }
}
