using Ekip2.Application.DTOs.AdvanceDTOs;
using Ekip2.Application.Services.AdvanceServices;
using Ekip2.Domain.Enums;
using Ekip2.Presentation.Areas.Manager.Models.AdvanceVMs;
using Ekip2.Presentation.Areas.Manager.Models.EmployeeVMs;
using Ekip2.Presentation.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class AdvanceController : ManagerBaseController
    {
        private readonly IAdvanceService _advanceService;
        private readonly IManagerService _managerService;

        public AdvanceController(IAdvanceService advanceService, IManagerService managerService)
        {
            _advanceService = advanceService;
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);
            var result = await _advanceService.GetAllAsync();
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<AdvanceListVM>());
            }
            var employeeVMs = result.Data
                                           
                                           .Where(x => x.CompanyId == managerResult.Data.CompanyId)
                                           .Adapt<List<AdvanceListVM>>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(employeeVMs);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _advanceService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            var advanceDetailsVM = result.Data.Adapt<AdvanceDetailsVM>();
            return View(advanceDetailsVM);
        }

        public async Task<IActionResult> Create()
        {
            var advanceVM = new AdvanceCreateVM()
            {
                Managers = await GetManagers()
            };
            return View(advanceVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvanceCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                /*model.Managers = await GetManagers();*/ // ModelState geçerli değilse tekrar doldurun
                return View(model);
            }
            try
            {
                var advanceCreateDTO = model.Adapt<AdvanceCreateDTO>();
                if (model.NewImage != null && model.NewImage.Length > 0)
                {
                    advanceCreateDTO.Image = await model.NewImage.StringToByteArrayAsync();
                }
                var result = await _advanceService.CreateAsync(advanceCreateDTO);
                if (!result.IsSuccess)
                {
                    model.Managers = await GetManagers();
                    await Console.Out.WriteLineAsync(result.Messages);
                    return RedirectToAction("Index");
                }
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Unexpected error: {ex.Message}");
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var result = await _advanceService.DeleteAsync(id);
                if (!result.IsSuccess)
                {
                    await Console.Out.WriteLineAsync(result.Messages);
                    return RedirectToAction("Index");
                }

                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _advanceService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            var advanceUpdateVM = result.Data.Adapt<AdvanceUpdateVM>();
            advanceUpdateVM.Managers = await GetManagers(advanceUpdateVM.ManagerId);
            advanceUpdateVM.ExistingImage = result.Data.Image;
            advanceUpdateVM.AdvanceDate=result.Data.AdvanceDate;
            return View(advanceUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AdvanceUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Managers = await GetManagers(); /// ModelState geçerli değilse tekrar doldurun
                return RedirectToAction("Index");
            }
            var advance = await _advanceService.GetByIdAsync(model.Id);

            var advanceUpdateDto = model.Adapt<AdvanceUpdateDTO>();

            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                advanceUpdateDto.Image = await model.NewImage.StringToByteArrayAsync();
            }
            else
            {
                advanceUpdateDto.Image = advance.Data.Image;
            }

            var result = await _advanceService.UpdateAsync(advanceUpdateDto);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> ApproveAdvance(Guid id)
        {
            var result = await _advanceService.ApproveAsync(id);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> RejectAdvance(Guid id)
        {
            var result = await _advanceService.RejectAsync(id);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }

        private async Task<SelectList> GetManagers(Guid? ManagerId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);
            var userCompanyId = managerResult.Data.CompanyId;


            var allEmployees = (await _managerService.GetAllAsync()).Data;

            // Role'ü Employee olanları filtrele
            var employees = allEmployees.Where(x => x.CompanyId == userCompanyId);

            // SelectListItem oluşturma
            var selectListItems = employees.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName,
                Selected = x.Id == ManagerId
            }).OrderBy(x => x.Text).ToList();

            // SelectList oluşturma
            var selectList = new SelectList(selectListItems, "Value", "Text");

            return selectList;
        }
    }
}
