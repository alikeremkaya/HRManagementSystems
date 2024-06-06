using Microsoft.EntityFrameworkCore.Storage;

namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IAsyncTransactionRepository
    {
        /// <summary>
        /// Yeni bir veritabanı işlemi başlatır.
        /// </summary>
        /// <param name="cancellationToken">
        /// İsteğe bağlı olarak işlemin iptal edilmesini sağlayan bir <see cref="CancellationToken"/>.
        /// Varsayılan değer <see cref="CancellationToken.None"/>.
        /// </param>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>.
        /// Görev tamamlandığında, başlatılan veritabanı işlemini temsil eden bir <see cref="IDbContextTransaction"/> döner.
        /// </returns>
        Task<IDbContextTransaction> BeginTransection(CancellationToken cancellationToken = default);
        /// <summary>
        /// Bir yürütme stratejisi oluşturur.
        /// </summary>
        /// <returns>
        /// Asenkron operasyonun sonucunu temsil eden bir <see cref="Task{TResult}"/>.
        /// Görev tamamlandığında, oluşturulan yürütme stratejisini temsil eden bir <see cref="IExecutionStrategy"/> döner.
        /// </returns>
        Task<IExecutionStrategy> CreateExecutionStrategy();
    }
}
