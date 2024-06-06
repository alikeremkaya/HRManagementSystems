using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs
{
    public class AdvanceDetailsVM
    {
        public Guid Id { get; set; }

        public double Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AdvanceDate { get; set; }

        public AdvanceStatus AdvanceStatus { get; set; }

        public byte[] Image { get; set; }

        public string ManagerFullName => $"{ManagerFirstName} {ManagerLastName}";

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string EmployeeName { get; set; } // Avans talebinde bulunan kişinin adı

        public string Description { get; set; } // Eğer avans talebiyle ilgili açıklama varsa

        public Guid CompanyId { get; set; }

        public Roles Roles { get; set; }
    }
}
