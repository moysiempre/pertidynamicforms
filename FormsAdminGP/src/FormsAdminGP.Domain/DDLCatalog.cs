namespace FormsAdminGP.Domain
{
    public class DDLCatalog : Entity<string>
    {
        public string Name { get; set; }
        public string FormDetailId { get; set; }
        public FormDetail FormDetail { get; set; }
    }
}
