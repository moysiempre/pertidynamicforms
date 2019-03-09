using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Data.Repositories
{
    public interface IUserTokenRepository : IBaseRepository<UserToken>
    {
    }

    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(DbContext context)
             : base(context)
        {

        }
    }

}
