using Ekip2.Infrastructure.Repositories.ExpenseRepositories;
using Ekip2.Infrastructure.Repositories.LeaveRepositories;
using Ekip2.Infrastructure.Repositories.LeaveTypeRepositories;
using Ekip2.Infrastructure.Repositories.PackageLogRepositories;
using Ekip2.Infrastructure.Repositories.PackageRepositories;
using Ekip2.Infrastructure.SeedData;
using Ekip2.Infrastructure.Repositories.SalaryLogRepositories;
using Ekip2.Infrastructure.Repositories.SalaryRepositories;

namespace Ekip2.Infrastructure.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(configuration.GetConnectionString("AppConnectionDev"));
            });
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<IAdvanceRepository, AdvanceRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPackageLogRepository, PackageLogRepository>();
            services.AddScoped<ISalaryRepository, SalaryRepository>();
            services.AddScoped<ISalaryLogRepository, SalaryLogRepository>();

            AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult();
            return services;
        }
    }
}
