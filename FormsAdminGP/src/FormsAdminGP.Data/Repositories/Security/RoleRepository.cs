using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Data.Repositories
{
    public interface IRoleRepository : IBaseRepository<UserRole>
    {
    }

    public class RoleRepository : BaseRepository<UserRole>, IRoleRepository
    {
        public RoleRepository(DbContext context)
             : base(context)
        {

        }
    }

}
