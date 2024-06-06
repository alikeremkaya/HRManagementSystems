namespace Ekip2.Domain.Utilities.Interfaces;

/// <summary>
/// İşlem sonucunu (başarı durumu ve mesaj) temsil eden interfacedir.
/// </summary>
public interface IResult
{
    public bool IsSuccess { get;  }

    public string Messages { get; }
}
