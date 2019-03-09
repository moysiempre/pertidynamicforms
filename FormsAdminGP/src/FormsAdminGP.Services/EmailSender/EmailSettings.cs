namespace FormsAdminGP.Core.EmailSender
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
}
