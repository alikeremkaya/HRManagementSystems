using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class SalaryLogConfiguration : AuditableEntityConfiguraton<SalaryLog>
    {
        public override void Configure(EntityTypeBuilder<SalaryLog> builder)
        {
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.SalaryDate).IsRequired();
          

            base.Configure(builder);
        }
    }
}
