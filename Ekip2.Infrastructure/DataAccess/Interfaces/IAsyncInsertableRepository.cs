namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncInsertableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
    {
        /// <summary>
        /// Belirtilen varlığı asenkron olarak veritabanına ekler.
        /// </summary>
        /// <param name="entity">
        /// Eklenecek olan varlık. Varlığın türü <typeparamref name="TEntity"/> olmalıdır.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, eklenen varlığı döner.
        /// </returns>
        Task<TEntity> AddAsync(TEntity entity);
        /// <summary>
        /// Belirtilen varlık koleksiyonunu (entities) asenkron olarak veritabanına ekler.
        /// </summary>
        /// <param name="entities">
        /// Eklenecek olan varlıkların koleksiyonu. Koleksiyonun türü <see cref="IEnumerable{TEntity}"/> olmalıdır.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task"/>.
        /// </returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
