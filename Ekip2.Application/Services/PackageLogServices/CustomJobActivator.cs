using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.PackageLogServices
{
    public class CustomJobActivator: JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomJobActivator(IServiceProvider serviceProvider)
        {

            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var job = scope.ServiceProvider.GetRequiredService(jobType);
                return job;
            }
        }
    }
}
