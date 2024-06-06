using Ekip2.Application.DTOs.SalaryDTOs;
using Ekip2.Infrastructure.Repositories.SalaryLogRepositories;
using Ekip2.Infrastructure.Repositories.SalaryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.SalaryServices
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly ISalaryLogRepository _salaryLogRepository;
        private readonly IManagerService _managerService;

        public SalaryService(ISalaryRepository salaryRepository, ISalaryLogRepository salaryLogRepository, IManagerService managerService)
        {
            _salaryRepository = salaryRepository;
            _salaryLogRepository = salaryLogRepository;
            _managerService = managerService;
        }

        public async Task<IDataResult<List<SalaryListDTO>>> GetAllAsync()
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var salaryDTOs = salaries.Adapt<List<SalaryListDTO>>();

            foreach (var salaryDTO in salaryDTOs)
            {
                var manager = await _managerService.GetByIdAsync(salaryDTO.ManagerId);
                salaryDTO.ManagerName = manager.Data?.FirstName + " " + manager.Data?.LastName;
            }

            return new SuccessDataResult<List<SalaryListDTO>>(salaryDTOs, "Salaries retrieved successfully");
        }

        public async Task<IDataResult<SalaryDTO>> CreateAsync(SalaryCreateDTO salaryCreateDTO)
        {
            var salary = salaryCreateDTO.Adapt<Salary>();
            await _salaryRepository.AddAsync(salary);
            await _salaryRepository.SaveChangesAsync();

            // Salary oluşturulduktan sonra, ilgili Manager nesnesini yükleyin
            var manager = await _managerService.GetByIdAsync(salary.ManagerId);
            if (manager == null)
            {
                return new ErrorDataResult<SalaryDTO>("Manager not found.");
            }

            var salaryLog = new SalaryLog
            {
               
                Amount = salary.Amount,
                SalaryDate = salary.SalaryDate,
                EmployeeName = salary.Manager.FirstName +" " + salary.Manager.LastName // Manager nesnesinden ismi alın
            };

            await _salaryLogRepository.AddAsync(salaryLog);
            await _salaryLogRepository.SaveChangesAsync();

            return new SuccessDataResult<SalaryDTO>(salary.Adapt<SalaryDTO>(), "Salary created successfully");
        }


        public async Task<IResult> DeleteAsync(Guid id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            if (salary == null)
            {
                return new ErrorResult("Salary not found");
            }

            await _salaryRepository.DeleteAsync(salary);
            await _salaryRepository.SaveChangesAsync();
            return new SuccessResult("Salary deleted successfully");
        }

        public async Task<IDataResult<SalaryDTO>> GetByIdAsync(Guid id)
        {
            var salary = await _salaryRepository.GetByIdAsync(id);
            if (salary == null)
            {
                return new ErrorDataResult<SalaryDTO>("Salary not found");
            }

            var salaryDTO = salary.Adapt<SalaryDTO>();
            return new SuccessDataResult<SalaryDTO>(salaryDTO, "Salary retrieved successfully");
        }

        public async Task<IDataResult<SalaryUpdateDTO>> UpdateAsync(SalaryUpdateDTO salaryUpdateDTO)
        {
            var salary = await _salaryRepository.GetByIdAsync(salaryUpdateDTO.Id);
            if (salary == null)
            {
                return new ErrorDataResult<SalaryUpdateDTO>("Salary not found");
            }

            salaryUpdateDTO.Adapt(salary);
            await _salaryRepository.UpdateAsync(salary);
            await _salaryRepository.SaveChangesAsync();

            var salaryLog = new SalaryLog
            {
              
                Amount = salary.Amount,
                SalaryDate = salary.SalaryDate,
                EmployeeName = salary.Manager.FirstName + " " + salary.Manager.LastName,
              
                Status = Domain.Enums.Status.Updated
            };
            await _salaryLogRepository.AddAsync(salaryLog);
            await _salaryLogRepository.SaveChangesAsync();

            return new SuccessDataResult<SalaryUpdateDTO>(salaryUpdateDTO, "Salary updated successfully");
        }
    }
}
