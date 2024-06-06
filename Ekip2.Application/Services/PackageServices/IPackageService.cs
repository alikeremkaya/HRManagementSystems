using Ekip2.Application.DTOs.PackageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.PackageServices
{
    public interface IPackageService
    {
        Task<IDataResult<List<PackageListDTO>>> GetAllAsync();
        Task<IDataResult<PackageDTO>> CreateAsync(PackageCreateDTO packageCreateDTO);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<PackageDTO>> GetByIdAsync(Guid id);
        Task<IDataResult<PackageUpdateDTO>> UpdateAsync(PackageUpdateDTO packageUpdateDTO);
       
    }
}
