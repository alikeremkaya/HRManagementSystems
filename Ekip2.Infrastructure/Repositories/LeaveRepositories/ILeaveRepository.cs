using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.LeaveRepositories
{
    public interface ILeaveRepository : IAsyncRepository, IAsyncInsertableRepository<Leave>, IAsyncFindableRepository<Leave>,
        IAsyncQueryableRepository<Leave>, IAsyncUpdatableRepository<Leave>, IAsyncDeletableRepository<Leave>, IAsyncOrderableRepository<Leave>,IAsyncTransactionRepository
    { 

    }
}
