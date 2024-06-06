using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.SalaryDTOs
{
    public class SalaryCreateDTO
    {
        public decimal Amount { get; set; }
        public DateTime SalaryDate { get; set; } // Updated field
        public Guid ManagerId { get; set; }
    }
}
