using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Data.Repositories
{
    public class MailTemplateRepository : BaseRepository<MailTemplate>, IMailTemplateRepository
    {
        public MailTemplateRepository(DbContext context)
             : base(context)
        {

        }
    }

}
