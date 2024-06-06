namespace Ekip2.Domain.Utilities.Interfaces;

/// <summary>
/// Veri içeren işlem sonucunu temsil eden interfacedir.
/// </summary>
/// <typeparam name="T">Veri türü</typeparam>
public interface IDataResult <T> : IResult where T : class
{
    public T? Data { get;  }
}
