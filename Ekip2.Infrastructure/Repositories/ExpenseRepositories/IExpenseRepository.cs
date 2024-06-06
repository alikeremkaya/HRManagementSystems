using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.ExpenseRepositories
{
    public interface IExpenseRepository : IAsyncRepository, IAsyncInsertableRepository<Expense>, IAsyncFindableRepository<Expense>,
        IAsyncQueryableRepository<Expense>, IAsyncUpdatableRepository<Expense>, IAsyncDeletableRepository<Expense>, IAsyncOrderableRepository<Expense>
    {
    }
}
