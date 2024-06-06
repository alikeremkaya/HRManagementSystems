using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.LeaveDTOs
{
    public class LeaveDTO
    {
        public Guid Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Guid LeaveTypeId { get; set; }
        public Guid ManagerId { get; set; }
        public string LeaveTypeType { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public Roles Roles { get; set; }
        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;
        public Guid CompanyId { get; set; }

        public Manager Manager { get; set; }
    }
}
