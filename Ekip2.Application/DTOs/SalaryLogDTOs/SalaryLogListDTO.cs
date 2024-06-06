using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.SalaryLogDTOs
{
    public class SalaryLogListDTO
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime SalaryDate { get; set; }
     
        public string EmployeeName { get; set; }
    }
}
