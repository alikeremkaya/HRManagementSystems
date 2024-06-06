

namespace Ekip2.Application.Services.CompanyServices
{
    public interface ICompanyService
    {
        Task<IDataResult<List<CompanyListDTO>>> GetAllAsync();
        Task<IDataResult<CompanyDTO>> CreateAsync(CompanyCreateDTO companyCreateDTO);
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<CompanyDTO>> GetByIdAsync(Guid id);
        Task<IDataResult<CompanyDTO>> UpdateAsync(CompanyUpdateDTO companyUpdateDTO);
        
    }
}
