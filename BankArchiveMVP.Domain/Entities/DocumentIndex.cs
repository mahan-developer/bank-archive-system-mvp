using BankArchiveMVP.Domain.Enums;
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
        public OcrStatus TextStatus { get; set; } = OcrStatus.Pending;
        public TextSource TextSource { get; set; } = TextSource.OCR;

        public DateTime? ProcessedAt { get; set; }

        public Document Document { get; set; } = null!;

    }
}
