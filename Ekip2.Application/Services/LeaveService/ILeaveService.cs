using Ekip2.Application.DTOs.LeaveDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.LeaveService
{
    public interface ILeaveService
    {

        /// <summary>
        /// Tüm izin kayıtlarını asenkron olarak getirir.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, LeaveListDTO içeren bir IDataResult döner.</returns>
        Task<IDataResult<List<LeaveListDTO>>> GetAllAsync();
        /// <summary>
        /// Yeni bir izin kaydı asenkron olarak oluşturur.
        /// </summary>
        /// <param name="leaveCreateDTO">Yeni bir izin kaydı oluşturmak için gerekli bilgileri içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, LeaveDTO içeren bir IDataResult döner.</returns>
        Task<IDataResult<LeaveDTO>> CreateAsync(LeaveCreateDTO leaveCreateDTO);
        /// <summary>
        /// Bir izin kaydını asenkron olarak siler.
        /// </summary>
        /// <param name="id">Silinecek izin kaydının kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, IResult döner.</returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Belirli bir kimliğe sahip izin kaydını asenkron olarak getirir.
        /// </summary>
        /// <param name="id">Getirilecek izin kaydının kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, LeaveDTO içeren bir IDataResult döner.</returns>
        Task<IDataResult<LeaveDTO>> GetByIdAsync(Guid id);
        /// <summary>
        /// Bir izin kaydını asenkron olarak günceller.
        /// </summary>
        /// <param name="leaveUpdateDTO">Güncellenecek izin kaydının bilgilerini içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, LeaveUpdateDTO içeren bir IDataResult döner.</returns>
        Task<IDataResult<LeaveDTO>> UpdateAsync(LeaveUpdateDTO leaveUpdateDTO);

        Task<IResult> RejectLeaveAsync(Guid id);


        Task<IResult> ApproveLeaveAsync(Guid id);

       
    }
}
