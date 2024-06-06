namespace Ekip2.Infrastructure.DataAccess.Interfaces
{
    public interface IRepository
    {
        /// <summary>
        /// Yapılan değişiklikleri veritabanına kaydeder.
        /// </summary>
        /// <returns>
        /// Veritabanına kaydedilen değişikliklerin sayısını döner.
        /// Eğer bir hata oluşursa, hata kodu olarak negatif bir değer döner.
        /// </returns>
        int SaveChanges();
    }
}
