using Ekip2.Application.Services.AdvanceServices;
using Ekip2.Application.Services.ExpenseServices;
using Ekip2.Application.Services.LeaveService;
using Ekip2.Presentation.Areas.Manager.Models.SummaryVMs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class SummaryController : ManagerBaseController
    {
        private readonly IAdvanceService _advanceService;
        private readonly IExpenseService _expenseService;
        private readonly ILeaveService _leaveService;

        public SummaryController(IAdvanceService advanceService, IExpenseService expenseService, ILeaveService leaveService)
        {
            _advanceService = advanceService;
            _expenseService = expenseService;
            _leaveService = leaveService;
        }

        public async Task<IActionResult> Index(Guid? employeeId)
        {
            var advances = await _advanceService.GetAllAsync();
            var expenses = await _expenseService.GetAllAsync();
            var leaves = await _leaveService.GetAllAsync();

            var model = new SummaryViewModel
            {
                Advances = employeeId.HasValue ? advances.Data.Where(a => a.ManagerId == employeeId.Value).ToList() : advances.Data,
                Expenses = employeeId.HasValue ? expenses.Data.Where(e => e.ManagerId == employeeId.Value).ToList() : expenses.Data,
                Leaves = employeeId.HasValue ? leaves.Data.Where(l => l.ManagerId == employeeId.Value).ToList() : leaves.Data
            };

            return View(model);
        }
    }
}
