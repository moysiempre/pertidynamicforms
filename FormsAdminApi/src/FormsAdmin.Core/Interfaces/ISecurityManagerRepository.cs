using FormsAdmin.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsAdmin.Core.Interfaces
{
    public interface ISecurityManagerRepository
    {
        Task<ApplicationUser> GetUser(AppUser appUser);
        Task<bool> ValidateUser(string userName);
        Task<AppRole> GetRoleByRoleName(string roleName);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<ApplicationUser> GetUserByEmailAndPassword(string userName, string password);
        Task<bool> AddUser(ApplicationUser user);
        Task<bool> UpdateUser(ApplicationUser user);
        Task<bool> SaveUser();
    }
}
