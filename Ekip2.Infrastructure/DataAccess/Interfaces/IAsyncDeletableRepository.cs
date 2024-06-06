namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncDeletableRepository<TEntity> : IAsyncRepository where TEntity : BaseEntity
    {
        /// <summary>
        /// Belirtilen varlığı (entity) asenkron olarak veritabanından siler.
        /// </summary>
        /// <param name="entity">
        /// Silinecek olan varlık. Varlığın türü <typeparamref name="TEntity"/> olmalıdır.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task"/>.
        /// Operasyon tamamlandığında, görev tamamlanır.
        /// </returns>
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Belirtilen varlık koleksiyonunu (entities) asenkron olarak veritabanından siler.
        /// </summary>
        /// <param name="entities">
        /// Silinecek olan varlıkların koleksiyonu. Koleksiyonun türü <see cref="IEnumerable{TEntity}"/> olmalıdır.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task"/>.
        /// Operasyon tamamlandığında, görev tamamlanır.
        /// </returns>
        Task DeleteRangeAsync(IEnumerable <TEntity> entities);
    }
}
