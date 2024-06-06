namespace Ekip2.Domain.Utilities.Concretes;

public class SuccessResult : Result
{

    /// <summary>
    /// Başarı sonucu oluşturur.
    /// </summary>
    public SuccessResult() : base(true)
    {
        
    }


    /// <summary>
    /// Başarı sonucu oluşturur ve mesaj belirler.
    /// </summary>
    /// <param name="message">Başarı mesajı.</param>
    public SuccessResult(string message) :  base(true, message) 
    {
        
    }
}
