using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Application.DTOs.PackageLogDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.PackageLogServices
{
    public interface IPackageLogService
    {
        Task<IResult> AssignSubscription(PackageSelectDTO packageSelectDTO);
        Task<IDataResult<List<PackageLogListDTO>>> GetAllAsync();
    }
}
