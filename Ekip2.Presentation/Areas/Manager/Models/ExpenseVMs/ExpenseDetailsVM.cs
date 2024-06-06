using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.ExpenseVMs
{
    public class ExpenseDetailsVM
    {
        public Guid Id { get; set; }

        public double Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        public string ManagerFullName => $"{ManagerFirstName} {ManagerLastName}";

        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }

        public string EmployeeName { get; set; } // Harcamayı yapan kişinin adı

        public Guid CompanyId { get; set; }

        public Roles Roles { get; set; }
    }
}
