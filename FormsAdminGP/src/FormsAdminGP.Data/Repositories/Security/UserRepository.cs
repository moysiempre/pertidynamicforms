using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Data.Repositories.Security
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
             : base(context)
        {

        }
    }

}
