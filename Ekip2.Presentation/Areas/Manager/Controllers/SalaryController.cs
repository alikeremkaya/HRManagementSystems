using Ekip2.Application.DTOs.SalaryDTOs;
using Ekip2.Application.Services.SalaryLogServices;
using Ekip2.Application.Services.SalaryServices;
using Ekip2.Presentation.Areas.Manager.Models.SalaryLogVMs;
using Ekip2.Presentation.Areas.Manager.Models.SalaryVMs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class SalaryController : ManagerBaseController
    {


        private readonly ISalaryService _salaryService;
        private readonly IManagerService _managerService;
        private readonly ISalaryLogService _salaryLogService;

        public SalaryController(ISalaryService salaryService, IManagerService managerService, ISalaryLogService salaryLogService)
        {
            _salaryService = salaryService;
            _managerService = managerService;
            _salaryLogService = salaryLogService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);
            var result = await _salaryService.GetAllAsync();
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<SalaryListVM>());
            }

            var employeeVMs = result.Data
                                   
                                     .Adapt<List<SalaryListVM>>();

            await Console.Out.WriteLineAsync(result.Messages);
            return View(employeeVMs);
        }



        public async Task<IActionResult> Create()
        {
            var salaryVM = new SalaryCreateVM()
            {
                Managers = await GetManagers()
            };
            return View(salaryVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalaryCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Managers = await GetManagers(); // ModelState geçerli değilse tekrar doldurun
                return View(model);
            }
            try
            {
                var salaryCreateDTO = model.Adapt<SalaryCreateDTO>();
                var result = await _salaryService.CreateAsync(salaryCreateDTO);
                if (!result.IsSuccess)
                {
                    model.Managers = await GetManagers();
                    await Console.Out.WriteLineAsync(result.Messages);
                    return View(model);
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
                var result = await _salaryService.DeleteAsync(id);
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
            var result = await _salaryService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            var salaryUpdateVM = result.Data.Adapt<SalaryUpdateVM>();
            salaryUpdateVM.Managers = await GetManagers(salaryUpdateVM.ManagerId);
            return View(salaryUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SalaryUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Managers = await GetManagers(); // ModelState geçerli değilse tekrar doldurun
                return View(model);
            }
            var salary = await _salaryService.GetByIdAsync(model.Id);

            var salaryUpdateDto = model.Adapt<SalaryUpdateDTO>();

            var result = await _salaryService.UpdateAsync(salaryUpdateDto);

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logs()
        {
            var result = await _salaryLogService.GetAllAsync();
            
            // Hata ayıklama için sonuç mesajını ve veri durumunu yazdırma
            await Console.Out.WriteLineAsync($"IsSuccess: {result.IsSuccess}, Message: {result.Messages}, Data Count: {result.Data?.Count}");

            if (!result.IsSuccess || result.Data == null || !result.Data.Any())
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<SalaryLogListVM>());
            }
            
            var logVMs = result.Data.Adapt<List<SalaryLogListVM>>();
            return View(logVMs);
        }

        private async Task<SelectList> GetManagers(Guid? ManagerId = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);
            var userCompanyId = managerResult.Data.CompanyId;

            var allEmployees = (await _managerService.GetAllAsync()).Data;

            var employees = allEmployees.Where(x => x.CompanyId == userCompanyId);

            var selectListItems = employees.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName,
                Selected = x.Id == ManagerId
            }).OrderBy(x => x.Text).ToList();

            var selectList = new SelectList(selectListItems, "Value", "Text");

            return selectList;
        }

    }
}
