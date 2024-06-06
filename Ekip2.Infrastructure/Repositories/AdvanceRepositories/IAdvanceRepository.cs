namespace Ekip2.Infrastructure.Repositories.AdvanceRepositories;

public interface IAdvanceRepository : IAsyncRepository, IAsyncInsertableRepository<Advance>, IAsyncFindableRepository<Advance>,
        IAsyncQueryableRepository<Advance>, IAsyncUpdatableRepository<Advance>, IAsyncDeletableRepository<Advance>,IAsyncOrderableRepository<Advance>
{
}
