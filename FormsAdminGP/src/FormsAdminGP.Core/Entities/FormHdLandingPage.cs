namespace FormsAdminGP.Core.Entities
{
    public class FormHdLandingPage 
    {
        public string LandingPageId { get; set; }
        public LandingPage LandingPage { get; set; }
        public string FormHdId { get; set; }
        public FormHd FormHd { get; set; }
    }
}
