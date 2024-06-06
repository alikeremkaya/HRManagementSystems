using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.SalaryLogRepositories
{
    public interface ISalaryLogRepository : IAsyncRepository, IAsyncInsertableRepository<SalaryLog>, IAsyncFindableRepository<SalaryLog>,
        IAsyncQueryableRepository<SalaryLog>, IAsyncUpdatableRepository<SalaryLog>, IAsyncDeletableRepository<SalaryLog>, IAsyncOrderableRepository<SalaryLog>
    {

    }
}
