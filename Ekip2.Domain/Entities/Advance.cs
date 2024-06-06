

namespace Ekip2.Domain.Entities
{
    public class Advance : AuditableEntity
    {
        public double Amount { get; set; }
        public DateTime AdvanceDate { get; set; }
        public byte[]? Image { get; set; }
        public Guid ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
    }
}
