namespace FormsAdminGP.Core.Entities
{
    public class InfoRequest: Entity<string>
    {
        public string InfoRequestData { get; set; }
        public string LandingPageId { get; set; }
        public LandingPage LandingPage { get; set; }
    }
}
