namespace Ekip2.Application.DTOs.AdvanceDTOs
{
    public class AdvanceCreateDTO
    {
        public double Amount { get; set; }
        public DateTime AdvanceDate { get; set; }
        public byte[] Image { get; set; }
        public Guid ManagerId { get; set; }
        public AdvanceStatus AdvanceStatus { get; set; } = AdvanceStatus.Pending;
    }
}
