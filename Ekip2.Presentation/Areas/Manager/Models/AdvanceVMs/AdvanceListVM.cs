using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs
{
    public class AdvanceListVM
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AdvanceDate { get; set; }
        public byte[] Image { get; set; }
        public Roles Roles { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public Guid CompanyId { get; set; }
    }
}
