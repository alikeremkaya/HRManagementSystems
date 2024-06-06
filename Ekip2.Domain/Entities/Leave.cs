using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Domain.Entities
{
    public class Leave : AuditableEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus LeaveStatus { get; set; }= LeaveStatus.Pending;
        
        //nav
        public Guid LeaveTypeId { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public Guid ManagerId { get; set; }
        public virtual Manager Manager { get; set;}

    }
}
