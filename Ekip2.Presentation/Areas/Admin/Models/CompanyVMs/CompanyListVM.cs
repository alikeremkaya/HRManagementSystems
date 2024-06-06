namespace Ekip2.Presentation.Areas.Admin.Models.CompanyVMs
{
    public class CompanyListVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
        public string PackageName { get; set; }
    }
}
