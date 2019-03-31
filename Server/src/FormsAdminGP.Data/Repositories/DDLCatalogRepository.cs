using FormsAdminGP.Core.Repositories;
using FormsAdminGP.Data.Repositories.Interfaces;
using FormsAdminGP.Domain;
using Microsoft.EntityFrameworkCore;

namespace FormsAdminGP.Data.Repositories
{
    public class DDLCatalogRepository : BaseRepository<DDLCatalog>, IDDLCatalogRepository
    {
        public DDLCatalogRepository(DbContext context)
             : base(context)
        {

        }
    }
}