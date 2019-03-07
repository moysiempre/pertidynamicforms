using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdmin.Core.Repositories
{
    public class InfoRequestRepository : BaseRepository<InfoRequest>, IInfoRequestRepository
    {
        public InfoRequestRepository(DbContext context)
             : base(context)
        {

        }
    }

}
