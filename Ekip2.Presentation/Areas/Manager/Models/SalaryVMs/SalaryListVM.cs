using Ekip2.Application.DTOs.ExpenseDTOs;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.SalaryVMs
{
    public class SalaryListVM
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SalaryDate { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerName { get; set; }
        public SelectList Managers { get; set; }
        public List<ManagerListDTO> Manager { get; set; }
    }
}
