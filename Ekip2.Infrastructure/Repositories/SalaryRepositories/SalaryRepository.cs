using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.SalaryRepositories
{
    public class SalaryRepository : EFBaseRepository<Salary>, ISalaryRepository
    {
        public SalaryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
