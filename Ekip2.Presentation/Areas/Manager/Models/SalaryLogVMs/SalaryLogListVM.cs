using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.SalaryLogVMs
{
    public class SalaryLogListVM
    {
       
        public decimal Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SalaryDate { get; set; }
       

        public string EmployeeName { get; set; }

    }
}
