using FormsAdminGP.Domain;
using FormsAdminGP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Core.Repositories
{
    public class FormHdLandingPageRepository : BaseRepository<FormHdLandingPage>, IFormHdLandingPageRepository
    {
        public FormHdLandingPageRepository(DbContext context)
             : base(context)
        {

        }
    }

}
