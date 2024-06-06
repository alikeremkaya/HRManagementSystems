using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.LeaveRepositories
{
    public class LeaveRepository : EFBaseRepository<Leave>, ILeaveRepository
    {
        public LeaveRepository(AppDbContext context) : base(context)
        {
        }

    }
}
