

using Castle.Components.DictionaryAdapter.Xml;
using Ekip2.Application.Services.PackageServices;
using Ekip2.Presentation.Areas.Admin.Models.ManagerVMs;
using Ekip2.Presentation.Extentions;

namespace Ekip2.Presentation.Areas.Admin.Controllers
{
    public class CompanyController : AdminBaseController
    {

        private readonly ICompanyService _companyService;
        private readonly IManagerService _managerService;
        private readonly IPackageService _packageService;
        public CompanyController(ICompanyService companyService, IManagerService managerService, IPackageService packageService)
        {
            _companyService = companyService;
            _managerService = managerService;
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _companyService.GetAllAsync();
            var companyVMs = result.Data.Adapt<List<CompanyListVM>>();

            await Console.Out.WriteLineAsync(result.Messages);
            return View(companyVMs);
        }
        public async Task<IActionResult> Create()
        {
            var companyVm = new CompanyCreateVM()
            {
                Packages = await GetPackages()
            };
            return View(companyVm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    await Console.Out.WriteLineAsync(error.ErrorMessage);
                }
                return RedirectToAction("Index");
            }
            var companyDTO = model.Adapt<CompanyCreateDTO>();

            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                companyDTO.Image = await model.NewImage.StringToByteArrayAsync();
            }

            var result = await _companyService.CreateAsync(companyDTO);
            if (!result.IsSuccess)
            {
                model.Packages = await GetPackages();
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletinId = await _companyService.DeleteAsync(id);

            if (!deletinId.IsSuccess)
            {
                await Console.Out.WriteLineAsync(deletinId.Messages);
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(deletinId.Messages);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _companyService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }
            var companyUpdateVM = result.Data.Adapt<CompanyUpdateVM>();
            companyUpdateVM.ExistingImage = result.Data.Image;
            companyUpdateVM.Packages = await GetPackages(companyUpdateVM.PackageId);
            return View(companyUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CompanyUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Packages = await GetPackages(model.PackageId);
                return View(model); // RedirectToAction yerine View döndürmek daha iyi
            }

            var company = await _companyService.GetByIdAsync(model.Id);

            if (!company.IsSuccess)
            {
                model.Packages = await GetPackages(model.PackageId);
                await Console.Out.WriteLineAsync(company.Messages);
                return View(model); // RedirectToAction yerine View döndürmek daha iyi
            }

            var companyUpdateDTO = model.Adapt<CompanyUpdateDTO>();

            // Yeni resim varsa, byte array'e çevir ve DTO'ya ekle
            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                companyUpdateDTO.Image = await model.NewImage.StringToByteArrayAsync();
            }
            else
            {
                // Yeni resim yoksa, mevcut resmi DTO'ya ekle
                companyUpdateDTO.Image = company.Data.Image;
            }

            var result = await _companyService.UpdateAsync(companyUpdateDTO);

            if (!result.IsSuccess)
            {
                model.Packages = await GetPackages(model.PackageId);
                await Console.Out.WriteLineAsync(result.Messages);
                return View(model); // RedirectToAction yerine View döndürmek daha iyi
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");

        }
        private async Task<SelectList> GetPackages(Guid? packageId = null)
        {
            var packages = (await _packageService.GetAllAsync()).Data;
            return new SelectList(packages.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (packageId != null ? packageId.Value : packageId)

            }).OrderBy(x => x.Text), "Value", "Text");
        }
    }
}
