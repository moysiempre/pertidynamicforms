namespace FormsAdminGP.Core.Entities
{
    public class LandingPage : AuditableEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public short TypeId { get; set; }
    }
}



