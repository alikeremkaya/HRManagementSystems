using Ekip2.Application.DTOs.ChangePasswordDTOs;
using Ekip2.Application.DTOs.ManagerDTOs;
using Ekip2.Application.DTOs.PackageDTOs;
using Ekip2.Domain.Utilities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.Services.ManagerServices
{
    public interface IManagerService
    {
        /// <summary>
        /// Tüm yöneticileri getirir.
        /// </summary>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, tüm yöneticilerin listesini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<List<ManagerListDTO>>> GetAllAsync();
        /// <summary>
        /// Yeni bir yönetici oluşturur.
        /// </summary>
        /// <param name="managerCreateDTO">Oluşturulacak yöneticinin bilgilerini içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, oluşturulan yöneticinin bilgilerini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<ManagerDTO>> CreateAsync(ManagerCreateDTO managerCreateDTO);
        /// <summary>
        /// Yöneticiyi benzersiz kimliğine göre siler.
        /// </summary>
        /// <param name="id">Yöneticinin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, silme işleminin sonucunu içeren IResult nesnesini içerir.</returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Yöneticiyi benzersiz kimliğine göre getirir.
        /// </summary>
        /// <param name="id">Yöneticinin benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, belirtilen kimliğe sahip yöneticinin bilgilerini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<ManagerDTO>> GetByIdAsync(Guid id);
        /// <summary>
        /// Yönetici bilgilerini günceller.
        /// </summary>
        /// <param name="managerUpdateDTO">Güncellenecek yöneticinin bilgilerini içeren DTO.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, güncellenen yöneticinin bilgilerini içeren IDataResult nesnesini içerir.</returns>
        Task<IDataResult<ManagerDTO>> UpdateAsync(ManagerUpdateDTO managerUpdateDTO);


        Task<IDataResult<ManagerDTO>> CreateEmployeeAsync(ManagerCreateDTO managerCreateDTO);

        Task<IDataResult<ManagerDTO>> GetByIdentityUserIdAsync(string identityUserId);

        Task<IResult> ChangePasswordAsync(ManagerChangePasswordDTO managerChangePasswordDTO);


        Task<List<Manager>> GetManagersByCompanyIdAsync(Guid companyId);
        

    }
}
