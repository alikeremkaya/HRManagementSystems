
using Ekip2.Domain.Enums;
using Ekip2.Presentation.Extentions;

namespace Ekip2.Presentation.Areas.Admin.Controllers
{

    public class ManagerController : AdminBaseController
    {
        private readonly IManagerService _managerService;
        private readonly IMailService _mailService;
        private readonly ICompanyService _companyService;
        public ManagerController(IManagerService managerService, IMailService mailService, ICompanyService companyService)
        {
            _managerService = managerService;
            _mailService = mailService;
            _companyService = companyService;
        }


        public async Task<IActionResult> Index()
        {
            var result = await _managerService.GetAllAsync();

            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return View(new List<ManagerListVM>()); // Hata durumunda boş liste döndür
            }

            // Yalnızca Role property'si Manager olanları filtrele
            var managerVMs = result.Data.Where(x => x.Roles == 0 || x.Roles ==Roles.Manager)
                                  .Adapt<List<ManagerListVM>>();
            

            await Console.Out.WriteLineAsync(result.Messages); // Başarılı durumda da mesajı yazdır
            return View(managerVMs);
        }
        public async Task<IActionResult> Create()
        {

            var managerVM = new ManagerCreateVM()
            {
               
                Roles = Roles.Manager,
                Companies = await GetCompanies(),
            };
            return View(managerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ManagerCreateVM model)
        {
            
            var managerUserDTO = model.Adapt<ManagerCreateDTO>();
            
            if (model.NewImage != null && model.NewImage.Length > 0)
            {
                managerUserDTO.Image = await model.NewImage.StringToByteArrayAsync();
            }
            managerUserDTO.RoleName = model.Roles.ToString();
            var result = await _managerService.CreateAsync(managerUserDTO);

            if (!result.IsSuccess)
            {
                model.Companies = await GetCompanies();
                model.Roles = Roles.Manager;
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _managerService.DeleteAsync(id);

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
            var result = await _managerService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                return RedirectToAction("Index");
            }

            var managerUpdateVM = result.Data.Adapt<ManagerUpdateVM>();
            managerUpdateVM.Companies = await GetCompanies(managerUpdateVM.CompanyId); // Şirket listesini ekleyin
            managerUpdateVM.Roles = Roles.Manager;
            managerUpdateVM.ExistingImage = result.Data.Image;


            return View(managerUpdateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ManagerUpdateVM managerUpdateVM)

        {
            if (!ModelState.IsValid)
            {
                managerUpdateVM.Companies = await GetCompanies(managerUpdateVM.CompanyId);
                managerUpdateVM.Roles = Roles.Manager;
                return RedirectToAction("Index");
            }
            var updateManagerDTO = managerUpdateVM.Adapt<ManagerUpdateDTO>();
            if (managerUpdateVM.NewImage != null && managerUpdateVM.NewImage.Length > 0)
            {
                updateManagerDTO.Image = await managerUpdateVM.NewImage.StringToByteArrayAsync();
            }
            else
            {
                updateManagerDTO.Image = (await _managerService.GetByIdAsync(managerUpdateVM.Id)).Data.Image;
            }
            var result = await _managerService.UpdateAsync(updateManagerDTO);
            if (!result.IsSuccess)
            {
                await Console.Out.WriteLineAsync(result.Messages);
                managerUpdateVM.Companies = await GetCompanies(managerUpdateVM.CompanyId);
                managerUpdateVM.Roles = Roles.Manager;
                return RedirectToAction("Index");
            }
            await Console.Out.WriteLineAsync(result.Messages);
            return RedirectToAction("Index", "Manager", new { Area = "Admin" });

        }


        private async Task<SelectList> GetCompanies(Guid? companyId = null)
        {
            var companies = (await _companyService.GetAllAsync()).Data;
            return new SelectList(companies.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (companyId != null ? companyId.Value : companyId)

            }).OrderBy(x => x.Text), "Value", "Text");
        }
        

    }
}
