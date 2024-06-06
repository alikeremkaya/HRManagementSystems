
using Ekip2.Infrastructure.Repositories.PackageLogRepositories;
using System.Linq;
using System.Linq.Expressions;


namespace Ekip2.Aplication.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdminRepository _adminRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IPackageLogRepository _packageLogRepository;
        public AccountService(UserManager<IdentityUser> userManager, IAdminRepository adminRepository, IManagerRepository managerRepository, IPackageLogRepository packageLogRepository)
        {
            _userManager = userManager;
            _adminRepository = adminRepository;
            _managerRepository = managerRepository;
            _packageLogRepository = packageLogRepository;
        }

        public async Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression)
        {
            return await _userManager.Users.AnyAsync(expression);
        }

        public async Task<IdentityResult> ChangePasswordAsyncc(IdentityUser user, string oldPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role)
        {
            var result = await _userManager.CreateAsync(user, "newPassword+1");
            if (!result.Succeeded)
            {
                return result;
            }
            return await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<IdentityResult> DeleteUserAsync(string identityId)
        {
            var user = await _userManager.FindByIdAsync(identityId);
            if(user == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "Kullanıcı bulunamadı",
                    Description = "Kullanıcı bulunamadı"
                });
            }
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityUser?> FindByIdAsync(string identityId)
        {
            return await _userManager.FindByIdAsync(identityId);
        }

        public async Task<Guid> GetUserIdAsync(string identityId, string role)
        {
            BaseUser? user = role switch
            {
                "Admin" => await _adminRepository.GetByIdentityId(identityId),
                "Manager" => await _managerRepository.GetByIdentityId(identityId),
                _ => null,
            };
            return user is null ? Guid.Empty : user.Id;
        }

        public async Task<IdentityResult> UpdateUserAsync(IdentityUser user)
        {
            var updatingUser = await _userManager.FindByIdAsync(user.Id);
            if (updatingUser == null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "Güncellenecek User Bulunamadı",
                    Description = "Güncellenecek User Bulunamadı"
                });
            }
            return await _userManager.UpdateAsync(user);
        }
        public async Task CheckSubscriptionStatus()
        {
            var subscriptions = await _packageLogRepository.GetAllAsync();
            var expiredSubscriptions = subscriptions
                .Where(s => s.IsActive && s.EndDate < DateTime.Now)
                .ToList();

            foreach (var subscription in expiredSubscriptions)
            {
                subscription.IsActive = false;
                await _packageLogRepository.UpdateAsync(subscription);
            }

            await _packageLogRepository.SaveChangesAsync();

            // Log the subscription status check
            Console.WriteLine($"Checked subscription status at {DateTime.Now}");
        }
    }
}
