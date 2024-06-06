using Ekip2.Domain.Enums;


namespace Ekip2.Domain.Core.Interfaces
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public Status Status { get; set; }
    }
}
