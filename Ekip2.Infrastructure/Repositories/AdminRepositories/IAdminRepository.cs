namespace Ekip2.Infrastructure.Repositories.AdminRepositories
{
    public interface IAdminRepository : IAsyncRepository, IAsyncInsertableRepository<Admin>, IAsyncFindableRepository<Admin>,
        IAsyncQueryableRepository<Admin>, IAsyncUpdatableRepository<Admin>, IAsyncDeletableRepository<Admin>,IAsyncTransactionRepository
    {

        Task<Admin?> GetByIdentityId(string identityId); 

        //  string tipinde bir IdentityId alır ve bu IdentityId'ye sahip olan Admin nesnesini bulur.


    }
}
