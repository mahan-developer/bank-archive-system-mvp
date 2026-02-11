using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Domain.Entities
{
    public class Case
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }

        public string CaseNo { get; set; } = null!;
        public string CaseType { get; set; } = null!;
        public string CaseStatus { get; set; } = "Open";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate { get; set; }
    }
}
