using Ekip2.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ekip2.Presentation.Areas.Manager.Models.EmployeeVMs
{
    public class EmployeeCreateVM
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string PhoneNumber { get; set; }
        public IFormFile NewImage { get; set; }

        public string RoleName { get; set; }
        public Roles Roles { get; set; }
        public SelectList? RoleList { get; set; }

        public Guid CompanyId { get; set; }
        public string CompanyName { get; set;}
    }
}
