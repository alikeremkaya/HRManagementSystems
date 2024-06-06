using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.ManagerDTOs
{
    public class ManagerUpdateDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public byte[]? Image { get; set; }
        public string RoleName { get; set; }
        public string Email { get; set; }
        public Roles Roles { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set;}
    }
}
