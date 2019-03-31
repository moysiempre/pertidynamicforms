namespace FormsAdminGP.Domain
{
    public class LandingPage : AuditableEntity<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FormHdId { get; set; }
        public FormHd FormHd { get; set; }
    }
}



