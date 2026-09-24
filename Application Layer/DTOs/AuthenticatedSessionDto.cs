using System;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.DTOs
{
    public class AuthenticatedSessionDto
    {
        public Guid UserId { get; set; }
        public Role Role { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
    }
}
