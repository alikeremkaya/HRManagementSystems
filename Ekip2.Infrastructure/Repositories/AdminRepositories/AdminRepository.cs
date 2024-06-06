using Ekip2.Infrastructure.DataAccess.BaseRepository;

namespace Ekip2.Infrastructure.Repositories.AdminRepositories
{
    public class AdminRepository : EFBaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(AppDbContext context) : base(context)
        {

        }

        /// <summary>
        ///   string tipinde bir IdentityId alır ve bu IdentityId'ye sahip olan Admin nesnesini bulur.
        /// </summary>
        /// <param name="identityId">string tipinde bir (identityId) alır</param>
        /// <returns> Eğer veritabanında bu IdentityId'ye sahip bir Admin nesnesi yoksa, null değeri döndürür.</returns>
        /// 

        public Task<Admin?> GetByIdentityId(string identityId)
        {
            return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);

            // FirstOrDefaultAsync metodu, verilen koşullara uyan "İLK öğeyi" asenkron olarak döndürür. Yani, bu metot verilen IdentityId'ye sahip olan Admin nesnesini bulmak için veritabanını sorgular ve bu nesneyi döndürür.
        }
    }
}
