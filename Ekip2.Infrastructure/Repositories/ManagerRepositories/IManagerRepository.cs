namespace Ekip2.Infrastructure.Repositories.ManagerRepositories
{
    public interface IManagerRepository : IAsyncRepository, IAsyncInsertableRepository<Manager>, IAsyncFindableRepository<Manager>,
        IAsyncQueryableRepository<Manager>, IAsyncUpdatableRepository<Manager>, IAsyncDeletableRepository<Manager>,IAsyncTransactionRepository
    {
        Task<Manager?> GetByIdentityId(string identityId);

        //  string tipinde bir IdentityId alır ve bu IdentityId'ye sahip olan Manager nesnesini bulur.
        
    }




}
