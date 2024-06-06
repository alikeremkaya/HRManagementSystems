using Ekip2.Application.DTOs.AdvanceDTOs;
using Ekip2.Application.DTOs.ExpenseDTOs;
using Ekip2.Application.DTOs.LeaveDTOs;

namespace Ekip2.Presentation.Areas.Manager.Models.SummaryVMs
{
    public class SummaryViewModel
    {
        public List<AdvanceListDTO> Advances { get; set; }
        public List<ExpenseListDTO> Expenses { get; set; }
        public List<LeaveListDTO> Leaves { get; set; }
    }
}
