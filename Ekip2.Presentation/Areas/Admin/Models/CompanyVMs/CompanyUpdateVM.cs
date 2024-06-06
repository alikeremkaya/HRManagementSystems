namespace Ekip2.Presentation.Areas.Admin.Models.CompanyVMs
{
    public class CompanyUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile NewImage { get; set; }
        public byte[] ExistingImage { get; set; }
        public Guid PackageId { get; set; }
        public SelectList? Packages { get; set; }
    }
}
