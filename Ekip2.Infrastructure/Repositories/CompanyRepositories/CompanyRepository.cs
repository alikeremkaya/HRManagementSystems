using Ekip2.Infrastructure.DataAccess.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Repositories.CompanyRepositories
{
    public class CompanyRepository : EFBaseRepository<Company> , ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context) { }
    }
}
