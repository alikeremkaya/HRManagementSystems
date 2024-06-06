using Ekip2.Domain.Entities;

namespace Ekip2.Presentation.Models
{
    public class ContactModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Package { get; set; }
    }
}
