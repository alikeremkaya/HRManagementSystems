namespace Ekip2.Domain.Utilities.Concretes;
public class SuccessDataResult<T> : DataResult<T> where T : class
{

    /// <summary>
    /// Başarı içeren veri sonucu oluşturur.
    /// </summary>
    public SuccessDataResult() : base(default, true)
    {
        
    }
    /// <summary>
    /// Başarı içeren veri sonucu oluşturur ve mesaj belirler.
    /// </summary>
    /// <param name="messages">Başarı mesajı.</param>
    public SuccessDataResult(string messages) : base(default, true,messages)
    {
        
    }


    /// <summary>
    /// Başarı içeren veri sonucu oluşturur, veri ve mesaj belirler.
    /// </summary>
    /// <param name="data">Dönen veri.</param>
    /// <param name="messages">Başarı mesajı.</param>
    public SuccessDataResult(T data, string messages) : base(data,true,messages) 
    {
        
    }
}
