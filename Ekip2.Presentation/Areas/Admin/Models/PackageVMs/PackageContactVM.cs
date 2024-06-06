namespace Ekip2.Presentation.Areas.Admin.Models.PackageVMs
{
    public class PackageContactVM
    {
        public Guid PackageId { get; set; }
        public SelectList? Packages { get; set; }
    }
}
