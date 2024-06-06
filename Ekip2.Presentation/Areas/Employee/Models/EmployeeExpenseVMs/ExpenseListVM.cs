using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Employee.Models.EmployeeExpenseVMs
{
    public class ExpenseListVM
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }
        public string Description { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public byte[] Image { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ManagerId { get; set; }
        public Roles Roles { get; set; }
    }
}
