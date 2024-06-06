using Ekip2.Domain.Enums;

namespace Ekip2.Presentation.Areas.Manager.Models.EmployeeVMs
{
    public class EmployeeListVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public Roles Roles { get; set; }
    }
}
