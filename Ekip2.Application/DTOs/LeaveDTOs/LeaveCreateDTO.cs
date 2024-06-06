using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.LeaveDTOs
{
    public class LeaveCreateDTO
    {
       
        public DateTime StartDate { get; set; }
       
        public DateTime EndDate { get; set; }
        //
        public Guid LeaveTypeId { get; set; }
        public Guid ManagerId { get; set; }



    }
}
