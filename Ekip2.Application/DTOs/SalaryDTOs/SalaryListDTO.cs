using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.SalaryDTOs
{
    public class SalaryListDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime SalaryDate { get; set; } 
        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
    }
}
