using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.SalaryVMs
{
    public class SalaryCreateVM
    {
        public decimal Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SalaryDate { get; set; }
        public Guid ManagerId { get; set; }
        public SelectList Managers { get; set; }
        public string EmployeName { get; set; }

    }
}
