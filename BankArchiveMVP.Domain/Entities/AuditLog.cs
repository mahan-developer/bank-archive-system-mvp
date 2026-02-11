using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public string EntityType { get; set; } = null!;  // Customer | Case | Document
        public Guid EntityId { get; set; }

        public string Action { get; set; } = null!;
        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
