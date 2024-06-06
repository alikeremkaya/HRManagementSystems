using Ekip2.Application.DTOs.SalaryDTOs;
using Ekip2.Application.DTOs.SalaryLogDTOs;
using Ekip2.Infrastructure.Repositories.SalaryLogRepositories;
using Ekip2.Infrastructure.Repositories.SalaryRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.SalaryLogServices
{
    public class SalaryLogService:ISalaryLogService
    {
        private readonly ISalaryLogRepository _salaryLogRepository;
        private readonly ISalaryRepository _salaryRepository;

        public SalaryLogService(ISalaryLogRepository salaryLogRepository, ISalaryRepository salaryRepository)
        {
            _salaryLogRepository = salaryLogRepository;
            _salaryRepository = salaryRepository;
        }

        public async Task<IDataResult<List<SalaryLogListDTO>>> GetAllAsync()
        {
            var logs = await _salaryLogRepository.GetAllAsync();
            var EmplyooeName = await _salaryRepository.GetAllAsync();
            if (logs == null || !logs.Any())
            {
                return new ErrorDataResult<List<SalaryLogListDTO>>(new List<SalaryLogListDTO>(), "No logs found");
            }

            var logDTOs = logs.Adapt<List<SalaryLogListDTO>>();

            return new SuccessDataResult<List<SalaryLogListDTO>>(logDTOs, "Logs retrieved successfully");
        }







        public async Task<IDataResult<SalaryLogDTO>> CreateAsync(SalaryLogDTO salaryLogDTO)
        {
            var salaryLog = salaryLogDTO.Adapt<SalaryLog>();
            await _salaryLogRepository.AddAsync(salaryLog);
            await _salaryLogRepository.SaveChangesAsync();
            return new SuccessDataResult<SalaryLogDTO>(salaryLogDTO, "Salary log created successfully");
        }

      

       
    }
}
