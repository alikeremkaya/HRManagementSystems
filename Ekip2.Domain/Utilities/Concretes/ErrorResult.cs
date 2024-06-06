namespace Ekip2.Domain.Utilities.Concretes
{
    public class ErrorResult : Result
    {
        /// <summary>
        /// Hata sonucu oluşturur.
        /// </summary>
        public ErrorResult() : base(false)
        {
            
        }


        /// <summary>
        /// Hata sonucu oluşturur ve mesaj belirler.
        /// </summary>
        /// <param name="message">Hata mesajı.</param>
        public ErrorResult(string message) : base(false,message) 
        {
            
        }
    }
}
