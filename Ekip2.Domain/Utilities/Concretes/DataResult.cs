

namespace Ekip2.Domain.Utilities.Concretes;

public class DataResult<T> : Result, IDataResult<T> where T : class
{
    public T? Data { get; }

    /// <summary>
    /// İşlem sonucunu ve dönen veriyi belirterek yeni bir örnek oluşturur.
    /// </summary>
    /// <param name="data">Dönen veri.</param>
    /// <param name="isSuccess">İşlemin Başarılı olup olmadığını belirtir.</param>
    public DataResult(T data, bool isSuccess) : base(isSuccess)
    {
        Data = data;
    }

    /// <summary>
    /// İşlem sonucunu, dönen veriyi ve mesajları belirterek yeni bir örnek oluşturur.
    /// </summary>
    /// <param name="data">Dönen veri.</param>
    /// <param name="isSuccess">İşlemin başarılı olup olmadığını belirtir.</param>
    /// <param name="message">İşlemle ilgili mesajlar.</param>
    public DataResult(T data, bool isSuccess,string message) : base(isSuccess,message)
    {
        Data = data;
    }
}
