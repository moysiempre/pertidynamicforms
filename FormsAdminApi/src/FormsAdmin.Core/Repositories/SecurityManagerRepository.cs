using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using FormsAdmin.Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdmin.Core.Repositories
{
    public class SecurityManagerRepository : BaseRepository<ApplicationUser>, ISecurityManagerRepository
    {
        public SecurityManagerRepository(DbContext context)
           : base(context)
        {

        } 

        public async Task<ApplicationUser> GetUser(AppUser appUser)
        {
            // Attempt to validate user
            var passwordEncrypted = Protector.Encrypt(appUser.Password, string.Empty);
            var userClaims = await FindEntityBy(u => u.UserName.ToLower() == appUser.UserName.ToLower()
               && u.PasswordHash == passwordEncrypted);

            return userClaims;
        }

        public async Task<bool> ValidateUser(string userName)
        {
            // validate user
            var user = await FindEntityBy(u => u.UserName.ToLower() == userName.ToLower());
            return user != null;
        }

        public async Task<AppRole> GetRoleByRoleName(string roleName)
        {
            // validate user
            var user = _entities.Set<AppRole>().FirstOrDefault(x => x.Description.ToLower().Equals(roleName.ToLower()));
            return user;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            var user = await FindEntityBy(u => u.UserName.ToLower() == email.ToLower());
            return user;
        }

        public async Task<ApplicationUser> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await FindEntityBy(u => u.UserName.ToLower() == email.ToLower() && u.PasswordHash == password);
            return user;
        }

        public async Task<bool> AddUser(ApplicationUser user)
        {
            this.Add(user);
            return true;
        }

        public async Task<bool> UpdateUser(ApplicationUser user)
        {
            this.Edit(user);
            return true;
        }
        public async Task<bool> SaveUser()
        {
            await SaveChanges();
            return true;
        }
    }


}

