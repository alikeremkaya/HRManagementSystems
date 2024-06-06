using Ekip2.Application.DTOs.LeaveDTOs;
using Ekip2.Application.Services.LeaveService;
using Ekip2.Domain.Enums;
using Ekip2.Presentation.Areas.Manager.Models.LeaveVMs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using System.Security.Claims;

namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class LeaveController : ManagerBaseController
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveService _leaveService;
        private readonly IManagerService _managerService;

        public LeaveController(ILeaveTypeService leaveTypeService, ILeaveService leaveService, IManagerService managerService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveService = leaveService;
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);
            if (!managerResult.IsSuccess)
            {
                await Console.Out.WriteLineAsync(managerResult.Messages);
                return View(new List<LeaveListVM>());
            }

            var companyId = managerResult.Data.CompanyId;

            var result = await _leaveService.GetAllAsync();
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<LeaveListVM>());
            }

            // DTO'ları VM'lere dönüştür
            var leaveVMs = result.Data

                .Where(l => l.Manager.CompanyId == companyId) // Manager üzerinden CompanyId ile filtreleme
                .Adapt<List<LeaveListVM>>();

            return View(leaveVMs);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _leaveService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            var leaveDetailsVM = result.Data.Adapt<LeaveDetailsVM>();
            return View(leaveDetailsVM);
        }

        public async Task<IActionResult> Create()
        {
            var leaveVM = new LeaveCreateVM()
            {
                LeaveTypes = await GetLeaveTypes(),
                Managers = await GetManagers(),
            };
            return View(leaveVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeaveCreateVM model)
        {

                var result = await _leaveService.CreateAsync(model.Adapt<LeaveCreateDTO>());

                if (!result.IsSuccess)
                {
                    await Console.Out.WriteLineAsync(result.Messages);
                    return RedirectToAction("Index");
                }

                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
           
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _leaveService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _leaveService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            var leaveUpdateVM = result.Data.Adapt<LeaveUpdateVM>();
            leaveUpdateVM.LeaveTypes = await GetLeaveTypes(leaveUpdateVM.LeaveTypeId);
            leaveUpdateVM.Managers = await GetManagers(leaveUpdateVM.ManagerId);
            leaveUpdateVM.StartDate = leaveUpdateVM.StartDate;
            leaveUpdateVM.EndDate = leaveUpdateVM.EndDate;

            return View(leaveUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LeaveUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var leaveUpdateDto = model.Adapt<LeaveUpdateDTO>();

            var result = await _leaveService.UpdateAsync(leaveUpdateDto);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ApproveLeave(Guid id)
        {
            var result = await _leaveService.ApproveLeaveAsync(id);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RejectLeave(Guid id)
        {
            var result = await _leaveService.RejectLeaveAsync(id);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        private async Task<SelectList> GetLeaveTypes(Guid? leaveTypeId = null)
        {
            var allLeaveTypes = (await _leaveTypeService.GetAllAsync()).Data;
            return new SelectList(allLeaveTypes.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Type,
                Selected = x.Id == (leaveTypeId != null ? leaveTypeId.Value : leaveTypeId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        private async Task<SelectList> GetManagers(Guid? ManagerId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);

            var userCompanyId = managerResult.Data.CompanyId;
            
            var allManagers = (await _managerService.GetAllAsync()).Data;

            var managers = allManagers.Where(x => x.CompanyId == userCompanyId);

            var selectListItems = managers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName,
                Selected = x.Id == ManagerId
            }).OrderBy(x => x.Text).ToList();

            return new SelectList(selectListItems, "Value", "Text");
        }
    }
}
