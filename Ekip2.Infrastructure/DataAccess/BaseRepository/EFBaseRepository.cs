

namespace Ekip2.Infrastructure.DataAccess.BaseRepository
{
    public class EFBaseRepository<TEntity> : IAsyncRepository,
        IAsyncDeletableRepository<TEntity>,
        IAsyncUpdatableRepository<TEntity>, 
        IRepository,
        IAsyncOrderableRepository<TEntity>,
        IAsyncQueryableRepository<TEntity>, 
        IAsyncFindableRepository<TEntity>,
        IAsyncTransactionRepository, 
        IAsyncInsertableRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        protected readonly DbSet<TEntity> _table;

        public EFBaseRepository(DbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entry = await _table.AddAsync(entity);
            return entry.Entity;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _table.AddRangeAsync(entities);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
        }

        protected IQueryable<TEntity> GetAllActives(bool tracking = true)
        {
            var values = _table.Where(x => x.Status != Domain.Enums.Status.Deleted);
            return tracking ? values : values.AsNoTracking();
        }

        public Task DeleteAsync(TEntity entity)
        {
            return Task.FromResult(_table.Remove(entity));
        }

        public Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
        {
            return await GetAllActives(tracking).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives(tracking).Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            return orderByDesc ? await GetAllActives(tracking).OrderByDescending(orderBy).ToListAsync() : await GetAllActives(tracking).OrderBy(orderBy).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true)
        {
            var values = GetAllActives(tracking).Where(expression);
            return orderByDesc ? await values.OrderByDescending(orderBy).ToListAsync() : await values.OrderBy(orderBy).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
        {
            return await GetAllActives(tracking).FirstOrDefaultAsync(x => x.Id == id);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await Task.FromResult(_table.Update(entity).Entity);
        }

        

        public Task<IExecutionStrategy> CreateExecutionStrategy()
        {
            return Task.FromResult(_context.Database.CreateExecutionStrategy());
        }

        public Task<IDbContextTransaction> BeginTransection(CancellationToken cancellationToken = default)
        {
            return _context.Database.BeginTransactionAsync(cancellationToken);
        }
    }
}
