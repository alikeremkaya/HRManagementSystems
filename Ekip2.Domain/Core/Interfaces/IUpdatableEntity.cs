namespace Ekip2.Domain.Core.Interfaces;

public interface IUpdatableEntity : ICreatableEntity
{
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
