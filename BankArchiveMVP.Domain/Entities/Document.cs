using BankArchiveMVP.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Domain.Entities
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }

        public string FileName { get; set; } = null!;
        public string? Title { get; set; }

        public string DocumentType { get; set; } = null!;
        public DocumentStatus DocumentStatus { get; set; } = DocumentStatus.New;

        public string FilePath { get; set; } = null!;
        public string FileHash { get; set; } = null!;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastUpdate { get; set; }

        public Case Case { get; set; } = null!;
        public DocumentIndex? DocumentIndex { get; set; }

    }
}
