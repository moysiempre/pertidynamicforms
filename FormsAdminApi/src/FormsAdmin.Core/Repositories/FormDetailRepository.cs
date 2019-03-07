using FormsAdmin.Core.Entities;
using FormsAdmin.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FormsAdmin.Core.Repositories
{
    public class FormDetailRepository : BaseRepository<FormDetail>, IFormDetailRepository
    {
        public FormDetailRepository(DbContext context)
             : base(context)
        {

        }
    }

}
