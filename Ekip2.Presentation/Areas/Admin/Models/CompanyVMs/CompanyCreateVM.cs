using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace Ekip2.Presentation.Areas.Admin.Models.CompanyVMs
{
    public class CompanyCreateVM
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
        public IFormFile NewImage { get; set; }
        public Guid PackageId { get; set; }
        public SelectList? Packages { get; set; }
    }
}
