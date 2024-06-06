using Ekip2.Application.DTOs.SalaryDTOs;
using Ekip2.Application.DTOs.SalaryLogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.SalaryLogServices
{
    public interface ISalaryLogService
    {
        Task<IDataResult<List<SalaryLogListDTO>>> GetAllAsync();

        Task<IDataResult<SalaryLogDTO>> CreateAsync(SalaryLogDTO salaryLogDTO);
    }
}
