namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncOrderableRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Tüm varlıkları belirtilen sıraya göre asenkron olarak getirir.
        /// </summary>
        /// <typeparam name="TKey">
        /// Varlıkların sıralanacağı anahtarın türü.
        /// </typeparam>
        /// <param name="orderBy">
        /// Varlıkların sıralanacağı anahtarı belirten ifade.
        /// </param>
        /// <param name="orderByDesc">
        /// Varlıkların azalan sırada sıralanıp sıralanmayacağını belirten bir değer.
        /// </param>
        /// <param name="tracking">
        /// Varlıkların değişikliklerinin izlenip izlenmeyeceğini belirten bir değer.
        /// Varsayılan değer <c>true</c> olup, izleme etkinleştirilmiştir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, varlıkların koleksiyonunu döner.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy,bool orderByDesc,bool tracking=true);

        /// <summary>
        /// Belirtilen koşulu sağlayan tüm varlıkları belirtilen sıraya göre asenkron olarak getirir.
        /// </summary>
        /// <typeparam name="TKey">
        /// Varlıkların sıralanacağı anahtarın türü.
        /// </typeparam>
        /// <param name="expression">
        /// Getirilecek varlıkların koşulunu temsil eden bir ifade.
        /// </param>
        /// <param name="orderBy">
        /// Varlıkların sıralanacağı anahtarı belirten ifade.
        /// </param>
        /// <param name="orderByDesc">
        /// Varlıkların azalan sırada sıralanıp sıralanmayacağını belirten bir değer.
        /// </param>
        /// <param name="tracking">
        /// Varlıkların değişikliklerinin izlenip izlenmeyeceğini belirten bir değer.
        /// Varsayılan değer <c>true</c> olup, izleme etkinleştirilmiştir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, belirtilen koşulu sağlayan varlıkların koleksiyonunu döner.
        /// </returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression,
            Expression<Func<TEntity,TKey>> orderBy,
            bool orderByDesc,bool tracking=true);
    }
}
