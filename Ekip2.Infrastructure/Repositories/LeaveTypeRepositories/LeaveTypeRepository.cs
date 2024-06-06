using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.LeaveTypeRepositories
{
    public class LeaveTypeRepository: EFBaseRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(AppDbContext context) : base(context) { }
    }
}
