using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Core.Repositories
{
    public class LandingPageRepository : BaseRepository<LandingPage> , ILandingPageRepository
    {
        public LandingPageRepository(DbContext context)
             : base(context)
        {

        }
    }
    
}
