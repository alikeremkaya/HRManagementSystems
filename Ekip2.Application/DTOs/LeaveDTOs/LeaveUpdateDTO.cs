using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.LeaveDTOs
{
    public class LeaveUpdateDTO
    {
        public Guid Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        public Guid LeaveTypeId { get; set; }
        public Guid ManagerId { get; set; }
        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;

    }
}
