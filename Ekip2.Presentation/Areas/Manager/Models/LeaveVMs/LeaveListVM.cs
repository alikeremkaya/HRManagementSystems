using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.LeaveVMs
{
    public class LeaveListVM
    {
        public Guid Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;
        public Roles Roles { get; set; }
        public string LeaveTypeType { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ManagerId { get; set; }
    }
}
