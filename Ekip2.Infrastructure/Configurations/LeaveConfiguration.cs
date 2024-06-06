using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class LeaveConfiguration : AuditableEntityConfiguraton<Leave>
    {
        public override void Configure(EntityTypeBuilder<Leave> builder)
        {
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            base.Configure(builder);
        }
    }
}
