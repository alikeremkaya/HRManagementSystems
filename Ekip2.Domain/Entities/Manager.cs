namespace Ekip2.Domain.Entities;

public class Manager : BaseUser
{
    public string? Password { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsFirstLogin { get; set; } = true;
    public Roles Roles { get; set; }
    public byte[]? Image { get; set; }
    //nav
    public Guid CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public virtual IEnumerable<Leave> Leaves { get; set; }
    public virtual IEnumerable<Advance> Advances { get; set; }

    public virtual IEnumerable<Expense> Expenseses { get; set; }
    public virtual IEnumerable<Salary> Salaries { get; set; }
}
