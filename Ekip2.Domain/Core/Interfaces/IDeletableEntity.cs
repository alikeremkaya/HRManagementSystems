namespace Ekip2.Domain.Core.Interfaces;

public interface IDeletableEntity
{
    public string? DeletedBy { get; set; }
    public DateTime? DeletedDate { get; set; }
}
