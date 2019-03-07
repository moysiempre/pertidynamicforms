using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdmin.Core.Repositories
{
    public class FormHdRepository : BaseRepository<FormHd>, IFormHdRepository
    {
        public FormHdRepository(DbContext context)
             : base(context)
        {

        }
    }

}
