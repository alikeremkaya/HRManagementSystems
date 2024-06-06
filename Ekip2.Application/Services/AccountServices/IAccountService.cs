
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Ekip2.Domain.Enums;

namespace Ekip2.Application.Services.AccountServices
{
    public interface IAccountService
    {
        /// <summary>
        /// Belirtilen koşulu sağlayan herhangi bir kullanıcı olup olmadığını belirler.
        /// </summary>
        /// <param name="expression">Her bir kullanıcıyı bir koşula göre test etmek için kullanılan bir fonksiyon.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, herhangi bir kullanıcının belirtilen koşulu sağlayıp sağlamadığını b
        Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression);

        /// <summary>
        /// Kullanıcıyı benzersiz kimliğine göre bulur.
        /// </summary>
        /// <param name="identityId">Kullanıcının benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. 
        /// Görev sonucu, belirtilen kimliğe sahip kullanıcıyı veya hiçbir kullanıcı bulunamazsa null değerini içerir.</returns>
        Task<IdentityUser?> FindByIdAsync(string identityId);

        /// <summary>
        /// Belirtilen role sahip yeni bir kullanıcı oluşturur.
        /// </summary>
        /// <param name="user">Oluşturulacak kullanıcı.</param>
        /// <param name="role">Kullanıcının rolü.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. 
        /// Görev sonucu, kullanıcı oluşturma işleminin sonucunu içeren IdentityResult nesnesini içerir.</returns>
        Task<IdentityResult> CreateUserAsync(IdentityUser user,Roles role);

        /// <summary>
        /// Kullanıcıyı benzersiz kimliğine göre siler.
        /// </summary>
        /// <param name="identityId">Kullanıcının benzersiz kimliği.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. Görev sonucu, kullanıcı silme işleminin sonucunu içeren IdentityResult nesnesini içerir.</returns>
        Task<IdentityResult> DeleteUserAsync(string identityId);

        /// <summary>
        /// Kullanıcının kimliğini ve rolünü kullanarak kullanıcı kimliğini alır.
        /// </summary>
        /// <param name="identityId">Kullanıcının benzersiz kimliği.</param>
        /// <param name="role">Kullanıcının rolü.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. 
        /// Görev sonucu, kullanıcının kimliğini içeren bir Guid içerir.</returns>
        Task<Guid> GetUserIdAsync(string identityId, string role);

        /// <summary>
        /// Kullanıcı bilgilerini günceller.
        /// </summary>
        /// <param name="user">Güncellenecek kullanıcı.</param>
        /// <returns>Asenkron işlemi temsil eden bir görev. 
        /// Görev sonucu, kullanıcı güncelleme işleminin sonucunu içeren IdentityResult nesnesini içerir.</returns>
        Task<IdentityResult> UpdateUserAsync(IdentityUser user);
        Task<IdentityResult> ChangePasswordAsyncc(IdentityUser user, string oldPassword, string newPassword);
        Task CheckSubscriptionStatus();
    }
}
