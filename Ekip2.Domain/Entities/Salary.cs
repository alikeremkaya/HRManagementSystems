using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Domain.Entities
{
    public class Salary:AuditableEntity
    {
       
        public decimal Amount { get; set; }
        public DateTime SalaryDate { get; set; } 

        // Nav
        public Guid ManagerId { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
