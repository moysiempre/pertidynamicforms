using System;

namespace FormsAdminGP.Core.Entities
{
    public class UserToken
    {
        public string Id { get; set; }

        public string AccessTokenHash { get; set; }

        public DateTimeOffset AccessTokenExpiresDateTime { get; set; }

        public string RefreshTokenIdHash { get; set; }

        public string RefreshTokenIdHashSource { get; set; }

        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}