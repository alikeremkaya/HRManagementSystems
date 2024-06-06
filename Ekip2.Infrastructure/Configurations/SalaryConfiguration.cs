using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class SalaryConfiguration : AuditableEntityConfiguraton<Salary>
    {
        public override void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.SalaryDate).IsRequired();
           
            base.Configure(builder);
        }
    }
}
