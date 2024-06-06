using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.PackageLogRepositories
{
    public interface IPackageLogRepository : IAsyncRepository, IAsyncInsertableRepository<PackageLog>, IAsyncFindableRepository<PackageLog>,
        IAsyncQueryableRepository<PackageLog>, IAsyncUpdatableRepository<PackageLog>, IAsyncDeletableRepository<PackageLog>, IAsyncOrderableRepository<PackageLog>
    {
    }
}
