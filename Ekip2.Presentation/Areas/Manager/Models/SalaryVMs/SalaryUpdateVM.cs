namespace Ekip2.Presentation.Areas.Manager.Models.SalaryVMs
{
    public class SalaryUpdateVM
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime SalaryDate { get; set; }
        public Guid ManagerId { get; set; }
        public SelectList Managers { get; set; }
       
    }
}
