using BankArchiveMVP.Domain.Enums;
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
        public CaseType CaseType { get; set; }
        public CaseStatus CaseStatus { get; set; } = CaseStatus.Open;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate { get; set; }

        public Customer Customer { get; set; } = null!;
        public ICollection<Document> Documents { get; set; } = new List<Document>();

    }
}
