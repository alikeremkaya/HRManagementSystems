
using Ekip2.Application.DTOs.LeaveTypeDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Ekip2.Presentation.Areas.Manager.Controllers
{
    public class LeaveTypeController : ManagerBaseController
    {
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        //private readonly ILeaveService _leaveService;
        public async Task<IActionResult> Index()
        {
            var result = await _leaveTypeService.GetAllAsync();
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<LeaveTypeListVM>());
            }
            var leaveTypeVM = result.Data.Adapt<List<LeaveTypeListVM>>();
            await Console.Out.WriteLineAsync(result.Messages);
            return View(leaveTypeVM);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LeaveTypeCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var leaveTypeDTO = model.Adapt<LeaveTypeCreateDTO>();
                var result = await _leaveTypeService.CreateAsync(leaveTypeDTO);
                if (!result.IsSuccess)
                {
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
            var result = await _leaveTypeService.DeleteAsync(id);

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
            var result = await _leaveTypeService.GetByIdAsync(id);
            var leaveTypeUpdateVM = result.Data.Adapt<LeaveTypeUpdateVM>();
            return View(leaveTypeUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(LeaveTypeUpdateVM model)

        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var result = await _leaveTypeService.UpdateAsync(model.Adapt<LeaveTypeUpdateDTO>());
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");

        }
    }
}
