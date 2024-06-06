using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.PackageLogRepositories
{
    public class PackageLogRepository : EFBaseRepository<PackageLog>,IPackageLogRepository
    {
        public PackageLogRepository(AppDbContext context) : base(context) { }
    }
}
