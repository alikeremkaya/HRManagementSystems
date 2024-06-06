using Ekip2.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ekip2.Presentation.Areas.Admin.Models.ManagerVMs
{
    public class ManagerUpdateVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile NewImage { get; set; }

        public byte[] ExistingImage { get; set; }
        public string RoleName { get; set; }
        public Roles Roles { get; set; }
        public SelectList? RoleList { get; set; }
        public Guid CompanyId { get; set; } // Seçilen şirket ID'si
        public SelectList? Companies { get; set; } // Şirketler listesi
    }
}
