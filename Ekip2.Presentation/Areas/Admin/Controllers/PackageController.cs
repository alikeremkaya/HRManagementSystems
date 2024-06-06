using Ekip2.Application.DTOs.LeaveTypeDTOs;
using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Application.Services.PackageServices;
using Ekip2.Domain.Enums;
using Ekip2.Presentation.Areas.Admin.Models.PackageVMs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ekip2.Presentation.Areas.Admin.Controllers
{
    public class PackageController : AdminBaseController
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            
            var packages = await _packageService.GetAllAsync();
            if (!packages.IsSuccess)
            {
                await Console.Out.WriteLineAsync(packages.Messages);
                return View(new List<PackageListVM>());
            }
            var packageVM = packages.Data.Adapt<List<PackageListVM>>();
            await Console.Out.WriteLineAsync(packages.Messages);
            return View(packageVM);


            


        }



        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PackageCreateVM model)
        {
            var packageDTO = model.Adapt<PackageCreateDTO>();
            var result = await _packageService.CreateAsync(packageDTO);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(model);
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _packageService.DeleteAsync(id);

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
            var result = await _packageService.GetByIdAsync(id);
            var packageUpdateVM = result.Data.Adapt<PackageUpdateVM>();
            return View(packageUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(PackageUpdateVM model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _packageService.UpdateAsync(model.Adapt<PackageUpdateDTO>());
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(model);
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");

        }


        //private async Task<SelectList> GetPackages(Guid? packageId = null)
        //{
        //    var packages = (await _packageService.GetAllAsync()).Data;
        //    return new SelectList(packages.Select(x => new SelectListItem
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Name,
        //        Selected = x.Id == (packageId != null ? packageId.Value : packageId)

        //    }).OrderBy(x => x.Text), "Value", "Text");




        //}

    }
}
