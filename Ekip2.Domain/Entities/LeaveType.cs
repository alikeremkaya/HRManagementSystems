
namespace Ekip2.Domain.Entities
{
    public class LeaveType : AuditableEntity
    {
        public string Type { get; set; }
        public string? Description { get; set; }
        public LeaveType()
        {
            this.Leaves = new HashSet<Leave>();
        }
        //Nav
        public virtual IEnumerable<Leave> Leaves { get; set; }
    }
}
