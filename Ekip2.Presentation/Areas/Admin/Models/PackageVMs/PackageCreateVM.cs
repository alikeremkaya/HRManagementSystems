using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Admin.Models.PackageVMs
{
    public class PackageCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EmployeeCount { get; set; }
        public decimal Price { get; set; }

        
    }
}
