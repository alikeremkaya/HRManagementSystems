using Ekip2.Application.DTOs.LeaveTypeDTOs;
using Ekip2.Infrastructure.Repositories.LeaveTypeRepositories;

namespace Ekip2.Application.Services.LeaveTypeServices
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<IDataResult<LeaveTypeDTO>> CreateAsync(LeaveTypeCreateDTO leaveTypeCreateDTO)
        {
            var newLeaveType = leaveTypeCreateDTO.Adapt<LeaveType>();
            await _leaveTypeRepository.AddAsync(newLeaveType);
            await _leaveTypeRepository.SaveChangesAsync();
            return new SuccessDataResult<LeaveTypeDTO>(newLeaveType.Adapt<LeaveTypeDTO>(),"İzin türü başarıyla eklendi");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingId = await _leaveTypeRepository.GetByIdAsync(id);
            if (deletingId != null)
            {
                await _leaveTypeRepository.DeleteAsync(deletingId);
                await _leaveTypeRepository.SaveChangesAsync();
                return new SuccessDataResult<LeaveTypeDTO>("Silme işlemi başarılı");
            }
            return new ErrorDataResult<LeaveTypeDTO>("Veri bulunamadı");
        }

        public async Task<IDataResult<List<LeaveTypeListDTO>>> GetAllAsync()
        {
            var list = await _leaveTypeRepository.GetAllAsync();
            if (list is null)
            {
                return new ErrorDataResult<List<LeaveTypeListDTO>>("Listeleme başarısız");
            }
            return new SuccessDataResult<List<LeaveTypeListDTO>>(list.Adapt<List<LeaveTypeListDTO>>(), "Listeleme başarılı");
        }

        public async Task<IDataResult<LeaveTypeDTO>> GetByIdAsync(Guid id)
        {
            var leaveTypeId = await _leaveTypeRepository.GetByIdAsync(id);
            if (leaveTypeId != null)
            {
                return new SuccessDataResult<LeaveTypeDTO>(leaveTypeId.Adapt<LeaveTypeDTO>(), "Veri başarıyla bulundu");
            }
            return new ErrorDataResult<LeaveTypeDTO>();
        }

        public async Task<IDataResult<LeaveTypeUpdateDTO>> UpdateAsync(LeaveTypeUpdateDTO leaveTypeUpdateDTO)
        {
            var updatingType = await _leaveTypeRepository.GetByIdAsync(leaveTypeUpdateDTO.Id);
            if (updatingType != null)
            {
                var updatedType = leaveTypeUpdateDTO.Adapt(updatingType);
                await _leaveTypeRepository.UpdateAsync(updatedType);
                await _leaveTypeRepository.SaveChangesAsync();
                return new SuccessDataResult<LeaveTypeUpdateDTO>(updatedType.Adapt<LeaveTypeUpdateDTO>(), "Güncelleme başarılı");
            }
            return new ErrorDataResult<LeaveTypeUpdateDTO>("Veri bulunamadı");
        }

      
    }
}
