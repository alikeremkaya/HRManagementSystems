using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Application.DTOs.SalaryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.SalaryServices
{
    public interface ISalaryService
    {
        Task<IDataResult<List<SalaryListDTO>>> GetAllAsync();
        Task<IDataResult<SalaryDTO>> CreateAsync(SalaryCreateDTO salaryCreateDTO);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<SalaryDTO>> GetByIdAsync(Guid id);
        Task<IDataResult<SalaryUpdateDTO>> UpdateAsync(SalaryUpdateDTO salaryUpdateDTO);
    }
}
