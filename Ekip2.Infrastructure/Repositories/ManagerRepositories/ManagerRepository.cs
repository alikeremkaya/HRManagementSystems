using Ekip2.Infrastructure.DataAccess.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace Ekip2.Infrastructure.Repositories.ManagerRepositories
{
    public class ManagerRepository : EFBaseRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(AppDbContext context) : base(context)
        {

        }

        /// <summary>
        ///   string tipinde bir IdentityId alır ve bu IdentityId'ye sahip olan Manager nesnesini bulur.
        /// </summary>
        /// <param name="identityId">string tipinde bir (identityId) alır</param>
        /// <returns> Eğer veritabanında bu IdentityId'ye sahip bir Manager nesnesi yoksa, null değeri döndürür.</returns>
        /// 

        public Task<Manager?> GetByIdentityId(string identityId)
        {
            return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);

        }

    }
}

