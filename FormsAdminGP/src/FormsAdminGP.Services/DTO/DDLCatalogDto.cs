using FormsAdminGP.Domain;

namespace FormsAdminGP.Services.DTO
{
    public class DDLCatalogDto : Entity<string>
    {
        public string Name { get; set; }
        public string FormDetailId { get; set; }
        
    }
}
