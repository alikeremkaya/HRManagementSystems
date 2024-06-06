namespace Ekip2.Domain.Core.Base;

/// <summary>
/// Kullanıcı entityleri için gerekli sınıftır.
/// </summary>
public class BaseUser : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }
    public string IdentityId { get; set; }
}
