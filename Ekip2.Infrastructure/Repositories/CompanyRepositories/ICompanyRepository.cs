using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.CompanyRepositories
{
    public interface ICompanyRepository :IAsyncRepository, IAsyncInsertableRepository<Company>, IAsyncFindableRepository<Company>,
        IAsyncQueryableRepository<Company>, IAsyncUpdatableRepository<Company>, IAsyncDeletableRepository<Company>,IAsyncTransactionRepository
    {
    }
}
