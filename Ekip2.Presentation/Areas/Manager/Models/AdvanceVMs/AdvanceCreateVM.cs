using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs
{
    public class AdvanceCreateVM
    {
        public double Amount { get; set; }
        
        public DateTime AdvanceDate { get; set; }= DateTime.Now;
        public IFormFile NewImage { get; set; }
        public Roles Roles { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerFirstName { get; set; }

        public string ManagerLastName { get; set; }
        public SelectList Managers { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
    }
}
