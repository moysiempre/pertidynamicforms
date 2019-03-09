using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Core.Repositories
{
    public class FormDetailRepository : BaseRepository<FormDetail>, IFormDetailRepository
    {
        public FormDetailRepository(DbContext context)
             : base(context)
        {

        }
    }

}
