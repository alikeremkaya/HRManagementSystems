using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Domain.Entities
{
    public class SalaryLog:AuditableEntity
    {
      
        public decimal Amount { get; set; }
        public DateTime SalaryDate { get; set; }
        public string EmployeeName { get; set; }
       
       
    }
}
