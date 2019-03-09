using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Core.Repositories
{
    public class InfoRequestRepository : BaseRepository<InfoRequest>, IInfoRequestRepository
    {
        public InfoRequestRepository(DbContext context)
             : base(context)
        {

        }
    }

}
