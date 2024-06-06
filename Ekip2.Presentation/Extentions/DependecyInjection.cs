using AspNetCoreHero.ToastNotification;
using Ekip2.Infrastructure.AppContext;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using System.Reflection;

namespace Ekip2.Presentation.Extentions
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddUIService(this IServiceCollection services)
        {
            AddIdentityService(services);
            services.AddNotyf(config =>
            {
                config.HasRippleEffect = true;
                config.DurationInSeconds = 5;
                config.Position = NotyfPosition.BottomRight;
                config.IsDismissable = true;

            });

            services.AddLocalization(opt => opt.ResourcesPath = "Resources");
            services.AddMvc()
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
            services.Configure<RequestLocalizationOptions>(opt =>
            {
                var supCultures = new List<CultureInfo>()
                {
                    new CultureInfo("tr"),
                    new CultureInfo("en")


                };

                opt.DefaultRequestCulture = new RequestCulture("tr");
                opt.SupportedUICultures = supCultures;
            });


            services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddControllersWithViews(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

           
            

            return services;
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
