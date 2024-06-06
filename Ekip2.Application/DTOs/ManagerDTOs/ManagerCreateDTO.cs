using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.ManagerDTOs
{
    public class ManagerCreateDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsFirstLogin { get; set; } = true;
        public byte[] Image { get; set; }
        public string? RoleName { get; set; }
        public Roles Roles { get; set; }
        public Guid CompanyId { get; set; }
    }
}
