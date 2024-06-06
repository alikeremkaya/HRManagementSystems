namespace Ekip2.Application.DTOs.AdvanceDTOs;

public class AdvanceDTO
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public DateTime AdvanceDate { get; set; }
    public byte[]? Image { get; set; }
    public Guid ManagerId { get; set; }



    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
    public Guid CompanyId { get; set; }
    public Roles Roles { get; set; }
    public AdvanceStatus AdvanceStatus { get; set; }
}
