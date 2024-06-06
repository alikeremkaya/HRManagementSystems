namespace Ekip2.Domain.Utilities.Concretes;

public class ErrorDataResult<T>: DataResult<T> where T : class
{
    /// <summary>
    /// Hata içeren veri sonucu oluşturur.
    /// </summary>
    public ErrorDataResult() : base(default,false)
    {
        
    }

    /// <summary>
    /// Hata içeren veri sonucu oluşturur ve mesaj belirler.
    /// </summary>
    /// <param name="messages">Hata mesajı.</param>
    public ErrorDataResult(string messages) : base(default,false,messages) 
    {
        
    }

    /// <summary>
    /// Hata içeren veri sonucu oluşturur, veri ve mesaj belirler.
    /// </summary>
    /// <param name="data">Dönen veri.</param>
    /// <param name="messages">Hata mesajı.</param>
    public ErrorDataResult(T data , string messages) : base(data,false,messages) 
    {
        
    }
}
