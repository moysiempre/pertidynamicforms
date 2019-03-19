using System;

namespace FormsAdminGP.Services.Request
{
    public class BaseRequest
    {
        public bool IsActive { get; set; }
        public bool HasInclude { get; set; }
        public string LandingPageId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
