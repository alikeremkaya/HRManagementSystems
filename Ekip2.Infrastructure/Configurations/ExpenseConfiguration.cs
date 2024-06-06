using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Infrastructure.Configurations
{
    public class ExpenseConfiguration : AuditableEntityConfiguraton<Expense>
    {
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.Property(e => e.Amount).IsRequired();
            builder.Property(e => e.Image).IsRequired(false);
            builder.Property(e => e.Description).IsRequired();
            builder.Property(e => e.ExpenseDate).IsRequired();


            base.Configure(builder);
        }

    }
}
