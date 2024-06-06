using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class PackageLogConfiguration : AuditableEntityConfiguraton<PackageLog>
    {
        public override void Configure(EntityTypeBuilder<PackageLog> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            base.Configure(builder);
        }
    }
}
