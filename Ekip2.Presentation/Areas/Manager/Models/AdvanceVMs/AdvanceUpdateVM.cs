using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs
{
    public class AdvanceUpdateVM
    {
        public Guid Id { get; set; }    

        public double Amount { get; set; }
        
        public DateTime AdvanceDate { get; set; }   

        public IFormFile NewImage { get; set; }

        public byte[] ExistingImage { get; set; }
        public Guid ManagerId { get; set; }
        public string ManagerFirstName { get; set; }
        public SelectList Managers { get; set; }
        public string ManagerLastName { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
    }
}
