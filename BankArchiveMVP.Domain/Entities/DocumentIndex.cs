using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Domain.Entities
{
    public class DocumentIndex
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }

        public string? Text { get; set; }
        public string TextStatus { get; set; } = "Pending";   // Pending | Ready | Failed
        public string TextSource { get; set; } = "OCR";       // OCR | Manual

        public DateTime? ProcessedAt { get; set; }
    }
}
