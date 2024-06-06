namespace Ekip2.Domain.Core.BaseEntityConfigurations;

public class BaseUserConfiguration<TEntity> : AuditableEntityConfiguraton<TEntity> where TEntity : BaseUser
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(128);
        builder.Property(e => e.Email).IsRequired();
        base.Configure(builder);
    }
}
