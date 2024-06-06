using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.SalaryLogRepositories
{
    public class SalaryLogRepository : EFBaseRepository<SalaryLog>, ISalaryLogRepository
    {
        public SalaryLogRepository(AppDbContext context) : base(context)
        {

        }
    }
}
