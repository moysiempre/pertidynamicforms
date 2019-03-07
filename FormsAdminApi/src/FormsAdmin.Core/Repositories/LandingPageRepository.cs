using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdmin.Core.Repositories
{
    public class LandingPageRepository : BaseRepository<LandingPage> , ILandingPageRepository
    {
        public LandingPageRepository(DbContext context)
             : base(context)
        {

        }
    }
    
}
