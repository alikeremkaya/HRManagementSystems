using Ekip2.Application.DTOs.AdminDTOs;
using Ekip2.Application.DTOs.ChangePasswordDTOs;
using Ekip2.Application.DTOs.ManagerDTOs;
using Ekip2.Domain.Enums;
using Ekip2.Domain.Utilities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.AdminServices
{
    public interface IAdminService
    {
        /// <summary>
        /// Tüm adminleri getirir.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, tüm adminlerin listesini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<List<AdminListDTO>>> GetAllAsync();
        /// <summary>
        /// Yeni bir admin oluşturur.
        /// </summary>
        /// <param name="adminCreateDTO">Oluşturulacak admin bilgilerini içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, oluşturulan adminin bilgilerini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO);
        /// <summary>
        /// Admini benzersiz kimliğine göre siler.
        /// </summary>
        /// <param name="id">Adminin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, silme işleminin sonucunu içeren IResult nesnesini içerir.</returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Admini benzersiz kimliğine göre getirir.
        /// </summary>
        /// <param name="id">Adminin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, belirtilen kimliğe sahip adminin bilgilerini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id);
        /// <summary>
        /// Admin bilgilerini günceller.
        /// </summary>
        /// <param name="adminUpdateDTO">Güncellenecek admin bilgilerini içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, güncellenen adminin bilgilerini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO);

        /// <summary>
        /// Belirtilen IdentityUser ID'sine sahip olan Admin verisini getirir.
        /// </summary>
        /// <param name="identityUserId">Kullanıcının IdentityUser ID'si</param>
        /// <returns>Oturum açan kullanıcının bilgilerini  getiren metod</returns>
        Task<IDataResult<AdminDTO>>GetByIdentityUserIdAsync(string identityUserId);

        Task<IResult> ChangePasswordAsync(AdminChangePasswordDTO adminChangePasswordDTO);
    }
}
