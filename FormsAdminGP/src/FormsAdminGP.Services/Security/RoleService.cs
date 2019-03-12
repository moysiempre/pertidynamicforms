using FormsAdminGP.Data.Repositories;
using FormsAdminGP.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public interface IRoleService
    {
        Task<List<Role>> FindUserRolesAsync(string userId);
        Task<bool> IsUserInRoleAsync(string userId, string roleName);
        Task<List<User>> FindUsersInRoleAsync(string roleName);
    }

    public class RoleService : IRoleService
    {

        private readonly IRoleRepository _roleRepository;

        public RoleService(
            IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<Role>> FindUserRolesAsync(string userId)
        {
            var roles = new List<Role>();
            var userRoles = await _roleRepository.FindBy(x => x.UserId == userId, p => p.Role);
            userRoles.ToList().ForEach(x=> roles.Add(x.Role));

            return roles.OrderBy(x => x.Name).ToList();
        }

        public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
        {
            var roles = await FindUserRolesAsync(userId);            
            var userRole = roles.FirstOrDefault(x=>x.Name == roleName);
            return userRole != null;
        }

        public async Task<List<User>> FindUsersInRoleAsync(string roleName)
        {
            var users = new List<User>();
            var userRoles = await _roleRepository.GetAll(p => p.User, x=>x.Role);
            //foreach (var item in userRoles)
            //{
            //    if(item.Role.Name == roleName)
            //    {
            //        users.Add(item.User);
            //    }
            //}
            return userRoles.Where(x => x.Role.Name == roleName).Select(x => x.User).ToList();             
        }
    }
}