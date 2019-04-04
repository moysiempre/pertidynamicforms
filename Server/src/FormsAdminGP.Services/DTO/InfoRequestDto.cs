using FormsAdminGP.Domain;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FormsAdminGP.Services.DTO
{
    public class InfoRequestDto : Entity<string>
    {
        [Required]
        public string InfoRequestData { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        public string LandingPageId { get; set; }
        public string FormHdId { get; set; }
        public string MailTemplateId { get; set; }
        public string FileName { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequestDateStr
        {
            get
            {
                return (RequestDate > DateTime.MinValue)? RequestDate.ToString("dd/MM/yyyy HH:mm:ss"): string.Empty;
            }
        }
        public LandingPageDto LandingPage { get; set; }
        public string LandingPageName { get { return  (LandingPage != null) ? LandingPage.Name : string.Empty; } }
        public StatField StatField { get; set; }

    }


    public class  StatField
    {
        public KeyValuePair<string, string> Field1 { get; set; }
        public KeyValuePair<string, string> Field2 { get; set; }
        public KeyValuePair<string, string> Field3 { get; set; }
        public KeyValuePair<string, string> Field4 { get; set; }
        public KeyValuePair<string, string> Field5 { get; set; }
        public KeyValuePair<string, string> Field6 { get; set; }
        public KeyValuePair<string, string> Field7 { get; set; }
        public KeyValuePair<string, string> Field8 { get; set; }
        public KeyValuePair<string, string> Field9 { get; set; }
        public KeyValuePair<string, string> Field10 { get; set; }
        public KeyValuePair<string, string> Field11 { get; set; }
        public KeyValuePair<string, string> Field12 { get; set; }
        public KeyValuePair<string, string> Field13 { get; set; }
        public KeyValuePair<string, string> Field14 { get; set; }
        public KeyValuePair<string, string> Field15 { get; set; }
        public KeyValuePair<string, string> Field16 { get; set; }
        public KeyValuePair<string, string> Field17 { get; set; }
        public KeyValuePair<string, string> Field18 { get; set; }
        public KeyValuePair<string, string> Field19 { get; set; }
        public KeyValuePair<string, string> Field20 { get; set; }
    }
}
