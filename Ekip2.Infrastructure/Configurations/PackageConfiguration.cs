using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class PackageConfiguration : AuditableEntityConfiguraton<Package>
    {
        public override void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.Property(x=>x.Name).IsRequired();
            builder.Property(x=> x.Description).IsRequired();   
            builder.Property(x=>x.EmployeeCount).IsRequired();
            builder.Property(x=>x.Price).IsRequired();
            base.Configure(builder);
        }
    }
}
