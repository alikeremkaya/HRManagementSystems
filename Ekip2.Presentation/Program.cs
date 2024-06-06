using Ekip2.Application.Extentions;
using Ekip2.Infrastructure.Extentions;
using Ekip2.Presentation.Extentions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Hangfire;
using System.Configuration;
using Ekip2.Application.Services.PackageLogServices;
using Ekip2.Infrastructure.Repositories.PackageLogRepositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("AppConnectionDev"));
    
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddUIService();

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//Localization istekleri için gerekli
app.UseRequestLocalization();

app.UseHangfireDashboard();



app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapDefaultControllerRoute();
app.Run();
