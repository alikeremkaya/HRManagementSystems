using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class LeaveTypeConfiguration : AuditableEntityConfiguraton<LeaveType>
    {
        public override void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Description).IsRequired(false);
            base.Configure(builder);
        }
    }
}
