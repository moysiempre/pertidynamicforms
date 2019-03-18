using System;
using System.Collections.Generic;
using System.Text;

namespace FormsAdminGP.Services.DTO
{
    public class EmailSettings
    {
        public string Smtp { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailFrom { get; set; }
        public string DisplayName { get; set; }
    }

    public class AppSettings
    {
        public string EbookPath { get; set; }
    }

    public class BaseDetailSettings : FormDetailDto
    {

    }
}
