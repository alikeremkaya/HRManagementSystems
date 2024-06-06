using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Employee.Models.EmployeeAdvanceVMs
{
    public class AdvanceUpdateVM
    {
        public Guid Id { get; set; }

        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AdvanceDate { get; set; }

        public IFormFile NewImage { get; set; }

        public byte[] ExistingImage { get; set; }
        public Guid ManagerId { get; set; }
        public SelectList Managers { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
    }
}
