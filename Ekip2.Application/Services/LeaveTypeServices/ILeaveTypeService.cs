using Ekip2.Application.DTOs.LeaveTypeDTOs;

namespace Ekip2.Application.Services.LeaveTypeServices
{
    public interface ILeaveTypeService
    {
        /// <summary>
        /// Tüm izin tiplerini asenkron olarak getirir.
        /// </summary>
        /// <returns>
        /// Asenkron işlemi temsil eden bir görev. Görev sonucu, LeaveTypeListDTO nesnelerinden oluşan bir liste ile IDataResult içerir.
        /// </returns>
        Task<IDataResult<List<LeaveTypeListDTO>>> GetAllAsync();
        /// <summary>
        /// Yeni bir izin tipi asenkron olarak oluşturur.
        /// </summary>
        /// <param name="leaveTypeCreateDTO">Oluşturulacak izin tipinin detaylarını içeren bir DTO.</param>
        /// <returns>
        /// Asenkron işlemi temsil eden bir görev. Görev sonucu, oluşturulan izin tipinin detaylarını içeren LeaveTypeDTO ile IDataResult içerir.
        /// </returns>
        Task<IDataResult<LeaveTypeDTO>> CreateAsync(LeaveTypeCreateDTO leaveTypeCreateDTO);
        /// <summary>
        /// Bir izin tipini asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek izin tipinin benzersiz kimliği.</param>
        /// <returns>
        /// Asenkron işlemi temsil eden bir görev. Görev sonucu, silme işleminin sonucunu içerir.
        /// </returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bir izin tipini kimliğine göre asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Getirilecek izin tipinin benzersiz kimliği.</param>
        /// <returns>
        /// Asenkron işlemi temsil eden bir görev. Görev sonucu, izin tipinin detaylarını içeren LeaveTypeDTO ile IDataResult içerir.
        /// </returns>
        Task<IDataResult<LeaveTypeDTO>> GetByIdAsync(Guid id);
        /// <summary>
        /// Bir izin tipini asenkron olarak günceller.
        /// </summary>
        /// <param name="leaveTypeUpdateDTO">Güncellenecek izin tipinin detaylarını içeren bir DTO.</param>
        /// <returns>
        /// Asenkron işlemi temsil eden bir görev. Görev sonucu, güncellenen izin tipinin detaylarını içeren LeaveTypeDTO ile IDataResult içerir.
        /// </returns>
        Task<IDataResult<LeaveTypeUpdateDTO>> UpdateAsync(LeaveTypeUpdateDTO leaveTypeUpdateDTO);
    }
}
