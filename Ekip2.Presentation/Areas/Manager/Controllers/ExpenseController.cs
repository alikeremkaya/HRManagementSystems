using Ekip2.Application.DTOs.ExpenseDTOs;
using Ekip2.Application.Services.ExpenseServices;
using Ekip2.Domain.Enums;
using Ekip2.Presentation.Areas.Manager.Models.EmployeeVMs;
using Ekip2.Presentation.Areas.Manager.Models.ExpenseVMs;
using Ekip2.Presentation.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.InteropServices;
using System.Security.Claims;


namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class ExpenseController : ManagerBaseController
    {
        private readonly IExpenseService _expenseService;
        private readonly IManagerService _managerService;

        public ExpenseController(IExpenseService expenseService, IManagerService managerService)
        {
            _expenseService = expenseService;
            _managerService = managerService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var managerResult = await _managerService.GetByIdentityUserIdAsync(userId);
            var result = await _expenseService.GetAllAsync();
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<ExpenseListVM>());
            }
            var employeeVMs = result.Data
                                           .Where(x => x.CompanyId == managerResult.Data.CompanyId)
                                           .Adapt<List<ExpenseListVM>>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(employeeVMs);
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _expenseService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            var expenseDetailsVM = result.Data.Adapt<ExpenseDetailsVM>();
            return View(expenseDetailsVM);
        }

        public async Task<IActionResult> Create()
        {
            var expenseVM = new ExpenseCreateVM()
            {
                Managers = await GetManagers()
            };
            return View(expenseVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Managers = await GetManagers(); // ModelState geçerli değilse tekrar doldurun
                return RedirectToAction("Index");
            }
            try
            {
                var expenseCreateDTO = model.Adapt<ExpenseCreateDTO>();
                if (model.NewImage != null && model.NewImage.Length > 0)
                {
                    expenseCreateDTO.Image = await model.NewImage.StringToByteArrayAsync();
                }
                var result = await _expenseService.CreateAsync(expenseCreateDTO);
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
                var result = await _expenseService.DeleteAsync(id);
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
            var result = await _expenseService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            var expenseUpdateVM = result.Data.Adapt<ExpenseUpdateVM>();
            expenseUpdateVM.Managers = await GetManagers(expenseUpdateVM.ManagerId);
            expenseUpdateVM.ExistingImage = result.Data.Image;
            expenseUpdateVM.Description = result.Data.Description;
            return View(expenseUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExpenseUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Managers = await GetManagers(); // ModelState geçerli değilse tekrar doldurun
                return RedirectToAction("Index");
            }
            var expense = await _expenseService.GetByIdAsync(model.Id);

            // Model'den ExpenseUpdateDTO'ya dönüşüm yaparken description alanını da dahil edin
            var expenseUpdateDto = model.Adapt<ExpenseUpdateDTO>();
           

            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                expenseUpdateDto.Image = await model.NewImage.StringToByteArrayAsync();
            }
            else
            {
                expenseUpdateDto.Image = expense.Data.Image;
            }

            var result = await _expenseService.UpdateAsync(expenseUpdateDto);

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

            var employees = allEmployees.Where(x=> x.CompanyId == userCompanyId);

           
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
