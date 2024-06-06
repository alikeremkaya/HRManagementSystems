namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncFindableRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Belirtilen koşulu sağlayan herhangi bir varlığın mevcut olup olmadığını asenkron olarak kontrol eder.
        /// </summary>
        /// <param name="expression">
        /// Kontrol edilecek koşulu temsil eden bir ifade. Varsayılan değer <c>null</c> olup,
        /// bu durumda tüm varlıklar kontrol edilir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, koşulu sağlayan en az bir varlık varsa <c>true</c>, aksi takdirde <c>false</c> döner.
        /// </returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression = null);
        /// <summary>
        /// Belirtilen kimlik (ID) ile eşleşen varlığı asenkron olarak getirir.
        /// </summary>
        /// <param name="id">
        /// Getirilecek varlığın kimliği (ID).
        /// </param>
        /// <param name="tracking">
        /// Varlığın değişikliklerinin izlenip izlenmeyeceğini belirten bir değer.
        /// Varsayılan değer <c>true</c> olup, izleme etkinleştirilmiştir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, belirtilen kimliğe sahip varlığı döner. Eğer varlık bulunamazsa <c>null</c> döner.
        /// </returns>
        Task<TEntity?> GetByIdAsync(Guid id,bool tracking = true);
        /// <summary>
        /// Belirtilen koşulu sağlayan ilk varlığı asenkron olarak getirir.
        /// </summary>
        /// <param name="expression">
        /// Getirilecek varlığın koşulunu temsil eden bir ifade.
        /// </param>
        /// <param name="tracking">
        /// Varlığın değişikliklerinin izlenip izlenmeyeceğini belirten bir değer.
        /// Varsayılan değer <c>true</c> olup, izleme etkinleştirilmiştir.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>. 
        /// Görev tamamlandığında, belirtilen koşulu sağlayan ilk varlığı döner. Eğer varlık bulunamazsa <c>null</c> döner.
        /// </returns>
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);

       
    }
}
