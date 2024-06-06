using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.SalaryRepositories
{
    public interface ISalaryRepository : IAsyncRepository, IAsyncInsertableRepository<Salary>, IAsyncFindableRepository<Salary>,
        IAsyncQueryableRepository<Salary>, IAsyncUpdatableRepository<Salary>, IAsyncDeletableRepository<Salary>, IAsyncOrderableRepository<Salary>
    {
    }
}
