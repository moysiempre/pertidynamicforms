using FormsAdminGP.Domain;
using FormsAdminGP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Core.Repositories
{
    public class FormHdRepository : BaseRepository<FormHd>, IFormHdRepository
    {
        public FormHdRepository(DbContext context)
             : base(context)
        {

        }
    }

}
