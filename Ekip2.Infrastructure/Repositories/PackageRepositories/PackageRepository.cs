using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.PackageRepositories
{
    public class PackageRepository :EFBaseRepository<Package>,IPackageRepository
    {
        public PackageRepository(AppDbContext context) : base(context) { }
        
    }
}
