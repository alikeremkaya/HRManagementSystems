
namespace Ekip2.Infrastructure.Repositories.LeaveTypeRepositories
{
    public interface ILeaveTypeRepository : IAsyncRepository, IAsyncInsertableRepository<LeaveType>, IAsyncFindableRepository<LeaveType>,
        IAsyncQueryableRepository<LeaveType>, IAsyncUpdatableRepository<LeaveType>, IAsyncDeletableRepository<LeaveType>,IAsyncTransactionRepository
    {
    }
}
