using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class AdvanceConfiguration : AuditableEntityConfiguraton<Advance>
    {
        public override void Configure(EntityTypeBuilder<Advance> builder)
        {
            builder.Property(a => a.Amount).IsRequired();
            builder.Property(a => a.AdvanceDate).IsRequired();
            builder.Property(a => a.Image).IsRequired(false);
            base.Configure(builder);
        }
    }
}
