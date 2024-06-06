using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.LeaveDTOs
{
    public class LeaveListDTO
    {
        public Guid Id { get; set; }
      
        public DateTime StartDate { get; set; } 
       
        public DateTime EndDate { get; set; }
        public string LeaveTypeType { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public Roles Roles { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ManagerId { get; set; }
        public Manager Manager { get; set; }
        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;

    }
}
