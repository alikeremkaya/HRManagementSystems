using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.LeaveVMs
{
    public class LeaveDetailsVM
    {
        public Guid Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public LeaveStatus LeaveStatus { get; set; }

        public string LeaveTypeType { get; set; }

        public string ManagerFullName => $"{ManagerFirstName} {ManagerLastName}";

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string EmployeeName { get; set; }

        public string Description { get; set; } // Eğer izinle ilgili açıklama varsa

        public Guid CompanyId { get; set; }

        public Roles Roles { get; set; }
    }
}
