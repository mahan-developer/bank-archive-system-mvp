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

        // OCR output / Manual text
        public string? ExtractedText { get; set; }

        // OCR pipeline status
        public OcrStatus OcrStatus { get; set; } = OcrStatus.Pending;

        // Where the current text came from
        public TextSource TextSource { get; set; } = TextSource.Manual;

        // When OCR (or manual entry) produced/updated the text
        public DateTime? ProcessedAt { get; set; }

        // Human review
        public string? CheckedBy { get; set; }
        public DateTime? CheckedAt { get; set; }

        public Document Document { get; set; } = null!;
    }
}
