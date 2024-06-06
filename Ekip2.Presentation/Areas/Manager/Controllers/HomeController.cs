using Ekip2.Presentation.Areas.Manager.Models.LeaveVMs;
using Ekip2.Presentation.Areas.Manager.Models.ManagerIndexVMs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Claims;
using Ekip2.Application.Services.AdvanceServices;
using Ekip2.Application.Services.ExpenseServices;
using Ekip2.Application.Services.LeaveService;
using Ekip2.Presentation.Areas.Employee.Models.EmployeeAdvanceVMs;
using Ekip2.Presentation.Areas.Employee.Models.EmployeeExpenseVMs;
using Ekip2.Presentation.Areas.Manager.Models.LeaveVMs;
namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class HomeController : ManagerBaseController
    {
        private readonly IManagerService _managerService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IAdvanceService _advanceService;
        private readonly ILeaveService _leaveService;
        private readonly IExpenseService _expenceService;

        public HomeController(IExpenseService expenceService, ILeaveService leaveService, IAdvanceService advanceService, ILeaveTypeService leaveTypeService, IManagerService managerService)
        {
            _expenceService = expenceService;
            _leaveService = leaveService;
            _advanceService = advanceService;
            _leaveTypeService = leaveTypeService;
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loginManager = await _managerService.GetByIdentityUserIdAsync(userId);
            var leaves = await _leaveService.GetAllAsync();
            var advances = await _advanceService.GetAllAsync();
            var managers = await _managerService.GetAllAsync();
            var employees = managers.Data.Where(x => x.Roles == Ekip2.Domain.Enums.Roles.Employee)
                                         .Where(x => x.CompanyId == loginManager.Data.CompanyId);
            var advanceVMs = advances.Data.Adapt<List<AdvanceListVM>>()
                                         .Where(x => x.CompanyId == loginManager.Data.CompanyId);
            var leaveVMs = leaves.Data.Where(x => x.Manager.CompanyId == loginManager.Data.CompanyId)
                                      .Adapt<List<LeaveListVM>>();
            var expenses = await _expenceService.GetAllAsync();
            var expenseVMs = expenses.Data.Adapt<List<ExpenseListVM>>()
                                      .Where(x => x.CompanyId == loginManager.Data.CompanyId);

            var expenseCountsByDay = expenseVMs
                .GroupBy(l => l.ExpenseDate.DayOfWeek)
                .Select(g => new { Day = g.Key, Count = g.Count() })
                .OrderBy(g => g.Day)
                .ToList();

            var expenseCounts = new Dictionary<string, int>
        {
            { "Monday", 0 },
            { "Tuesday", 0 },
            { "Wednesday", 0 },
            { "Thursday", 0 },
            { "Friday", 0 },
            { "Saturday", 0 },
            { "Sunday", 0 }
        };

            foreach (var item in expenseCountsByDay)
            {
                expenseCounts[item.Day.ToString()] = item.Count;
            }

            var advanceCountsByDay = advanceVMs
                .GroupBy(l => l.AdvanceDate.DayOfWeek)
                .Select(g => new { Day = g.Key, Count = g.Count() })
                .OrderBy(g => g.Day)
                .ToList();

            var advanceCounts = new Dictionary<string, int>
        {
            { "Monday", 0 },
            { "Tuesday", 0 },
            { "Wednesday", 0 },
            { "Thursday", 0 },
            { "Friday", 0 },
            { "Saturday", 0 },
            { "Sunday", 0 }
        };

            foreach (var item in advanceCountsByDay)
            {
                advanceCounts[item.Day.ToString()] = item.Count;
            }

            var leaveCountsByDay = leaveVMs
                .GroupBy(l => l.StartDate.DayOfWeek)
                .Select(g => new { Day = g.Key, Count = g.Count() })
                .OrderBy(g => g.Day)
                .ToList();

            var leaveCounts = new Dictionary<string, int>
        {
            { "Monday", 0 },
            { "Tuesday", 0 },
            { "Wednesday", 0 },
            { "Thursday", 0 },
            { "Friday", 0 },
            { "Saturday", 0 },
            { "Sunday", 0 }
        };

            foreach (var item in leaveCountsByDay)
            {
                leaveCounts[item.Day.ToString()] = item.Count;
            }

            var model = new ManagerIndexVM
            {
                ExpenseCounts = expenseCounts,
                AdvanceCounts = advanceCounts,
                LeaveCounts = leaveCounts
            };

            return View(model);
        }


    }
}
