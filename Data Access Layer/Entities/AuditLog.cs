using System;
using System.ComponentModel.DataAnnotations;

using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.DataAccess.Entities
{
    public class AuditLog
    {
        [Key]
        public long AuditLogId { get; private set; }

        [Required]
        public Guid UserId { get; private set; }

        [Required]
        public AuditEventType EventType { get; private set; }

        [Required]
        public DateTime OccurredUtc { get; private set; }

        public virtual UserAccount User { get; private set; }

        private AuditLog()
        {
        }

        public AuditLog(Guid userId, AuditEventType eventType)
        {
            UserId = userId;
            EventType = eventType;
            OccurredUtc = DateTime.UtcNow;
        }
    }
}
