using Ekip2.Infrastructure.DataAccess.BaseRepository;
using Ekip2.Infrastructure.Repositories.AdvanceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.ExpenseRepositories;

public class ExpenseRepository : EFBaseRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(AppDbContext context) : base(context)
    {
    }

   
}