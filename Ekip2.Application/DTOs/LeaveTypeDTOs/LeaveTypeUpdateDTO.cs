using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.LeaveTypeDTOs
{
    public class LeaveTypeUpdateDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
       
    }
}
