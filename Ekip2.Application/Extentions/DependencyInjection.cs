

using Ekip2.Application.Services.AdvanceServices;
using Ekip2.Application.Services.CompanyServices;
using Ekip2.Application.Services.ExpenseServices;
using Ekip2.Application.Services.HangFireService;
using Ekip2.Application.Services.LeaveService;
using Ekip2.Application.Services.LeaveTypeServices;
using Ekip2.Application.Services.PackageLogServices;
using Ekip2.Application.Services.PackageServices;
using Ekip2.Application.Services.SalaryLogServices;
using Ekip2.Application.Services.SalaryServices;
using Ekip2.Infrastructure.Repositories.SalaryLogRepositories;



namespace Ekip2.Application.Extentions;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IMailService, MailService>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ILeaveTypeService, LeaveTypeService>();
        services.AddScoped<IAdvanceService, AdvanceService>();
        services.AddScoped<ILeaveService, LeaveService>();
        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IPackageLogService, PackageLogService>();
        services.AddScoped<HangfireService>();
        services.AddScoped<CustomJobActivator>();
       
        services.AddScoped<ISalaryService, SalaryService>();
        services.AddScoped<ISalaryLogService, SalaryLogService>();
       

        return services;
    }
}
