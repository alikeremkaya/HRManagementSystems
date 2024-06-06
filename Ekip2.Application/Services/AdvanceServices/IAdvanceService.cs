using Ekip2.Application.DTOs.AdvanceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.AdvanceServices;

public interface IAdvanceService
{
    Task<IDataResult<List<AdvanceListDTO>>> GetAllAsync();
    Task<IDataResult<AdvanceDTO>> CreateAsync(AdvanceCreateDTO advanceCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
   
    Task<IDataResult<AdvanceDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AdvanceDTO>> UpdateAsync(AdvanceUpdateDTO advanceUpdateDTO);
    Task<IResult> RejectAsync(Guid id);
    Task<IResult> ApproveAsync(Guid id);
    Task<IDataResult<List<AdvanceListDTO>>> GetAllByManagerIdAsync(Guid managerId);


}
