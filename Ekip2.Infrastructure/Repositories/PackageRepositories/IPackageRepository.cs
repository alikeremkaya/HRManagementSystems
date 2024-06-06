using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.PackageRepositories
{
    public interface IPackageRepository : IAsyncRepository, IAsyncInsertableRepository<Package>, IAsyncFindableRepository<Package>,
        IAsyncQueryableRepository<Package>, IAsyncUpdatableRepository<Package>, IAsyncDeletableRepository<Package>, IAsyncOrderableRepository<Package>
    {
    }
}
