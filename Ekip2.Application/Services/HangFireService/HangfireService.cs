using Ekip2.Application.Services.PackageLogServices;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.HangFireService
{
    public class HangfireService
    {
        private readonly IRecurringJobManager _jobManager;
        private readonly IAccountService _accountService;

        public HangfireService(IRecurringJobManager jobManager,  IAccountService accountService)
        {
            _jobManager = jobManager;
            _accountService = accountService;
        }
        public void Configuration()
        {
            _jobManager.AddOrUpdate(
                "check-subscription-status",
            () => _accountService.CheckSubscriptionStatus(),
            Cron.Minutely);
        }
    }
}
