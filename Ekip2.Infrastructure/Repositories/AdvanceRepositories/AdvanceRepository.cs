using Ekip2.Infrastructure.DataAccess.BaseRepository;

namespace Ekip2.Infrastructure.Repositories.AdvanceRepositories;

public class AdvanceRepository : EFBaseRepository<Advance> , IAdvanceRepository
{
    public AdvanceRepository(AppDbContext context) : base(context)
    {
        
    }
}
