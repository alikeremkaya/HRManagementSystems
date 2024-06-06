namespace Ekip2.Infrastructure.Configurations;

public class ManagerConfiguration : BaseUserConfiguration<Manager>
{
    public override void Configure(EntityTypeBuilder<Manager> builder)
    {
        builder.Property(x => x.Password).IsRequired(false);
        builder.Property(x=>x.Address).IsRequired()
            .HasMaxLength(128);
        builder.Property(x=>x.PhoneNumber).IsRequired()
            .HasMaxLength (11);
        builder.Property(x => x.Image).IsRequired(false);
        base.Configure(builder);
    }
}
