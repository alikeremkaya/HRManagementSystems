using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class CompanyConfiguration : AuditableEntityConfiguraton<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x=>x.Address).IsRequired().HasMaxLength(128);
            builder.Property(x=>x.PhoneNumber).IsRequired();
            builder.Property(x => x.Image).IsRequired(false);
            base.Configure(builder);
        }
    }
}
