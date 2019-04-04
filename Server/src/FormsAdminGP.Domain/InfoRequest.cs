using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormsAdminGP.Domain
{
    public class InfoRequest : Entity<string>
    {
        public string InfoRequestData { get; set; }
        public DateTime RequestDate { get; set; }
        public string LandingPageId { get; set; }
        public LandingPage LandingPage { get; set; }
        public string FormHdId { get; set; }
        public FormHd FormHd { get; set; }


        [NotMapped]
        public DateTime RequestShortDate
        {
            get
            {
                DateTime _requestDate = DateTime.MinValue;                
                DateTime.TryParse(RequestDate.ToShortDateString(), out _requestDate);
                return _requestDate;
            }
        }

    }
}
