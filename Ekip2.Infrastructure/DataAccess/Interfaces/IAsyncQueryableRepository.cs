namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncQueryableRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Veritabanındaki tüm varlıkları asenkron olarak getirir.
        /// </summary>
        /// <param name="tracking">
        /// Varlıkların değişikliklerinin izlenip izlenmeyeceğini belirten bir değer.
        /// Varsayılan değer <c>true</c> olup, izleme etkinleştirilmiştir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, tüm varlıkların koleksiyonunu döner.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true);
       

        /// <summary>
        /// Belirtilen koşulu sağlayan tüm varlıkları asenkron olarak getirir.
        /// </summary>
        /// <param name="expression">
        /// Getirilecek varlıkların koşulunu temsil eden bir ifade.
        /// </param>
        /// <param name="tracking">
        /// Varlıkların değişikliklerinin izlenip izlenmeyeceğini belirten bir değer.
        /// Varsayılan değer <c>true</c> olup, izleme etkinleştirilmiştir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, belirtilen koşulu sağlayan varlıkların koleksiyonunu döner.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>> expression, bool tracking = true);
    }
}
