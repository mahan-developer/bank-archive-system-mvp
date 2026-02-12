using BankArchiveMVP.Application.Abstractions.Persistence;
using BankArchiveMVP.Domain.Entities;
using BankArchiveMVP.Domain.Enums;

namespace BankArchiveMVP.Application.UseCases.Documents;

public class CreateDocumentService
{
    private readonly ICaseRepository _caseRepo;
    private readonly IDocumentRepository _docRepo;

    public CreateDocumentService(ICaseRepository caseRepo, IDocumentRepository docRepo)
    {
        _caseRepo = caseRepo;
        _docRepo = docRepo;
    }

    public async Task<Guid> CreateAsync(
        string caseNo,
        string fileName,
        string filePath,
        string fileHash,
        string documentType,
        string? title,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(caseNo))
            throw new InvalidOperationException("CaseNo is required.");

        if (string.IsNullOrWhiteSpace(fileName))
            throw new InvalidOperationException("FileName is required.");

        if (string.IsNullOrWhiteSpace(filePath))
            throw new InvalidOperationException("FilePath is required.");

        if (string.IsNullOrWhiteSpace(fileHash))
            throw new InvalidOperationException("FileHash is required.");

        if (string.IsNullOrWhiteSpace(documentType))
            throw new InvalidOperationException("DocumentType is required.");

        caseNo = caseNo.Trim();
        fileName = fileName.Trim();
        filePath = filePath.Trim();
        fileHash = fileHash.Trim();
        documentType = documentType.Trim();

        // Normalize
        documentType = documentType.Replace("  ", " ");
        documentType = documentType.ToUpperInvariant();

        if (documentType.Length > 50)
            throw new InvalidOperationException("DocumentType is too long (max 50).");

        title = string.IsNullOrWhiteSpace(title) ? null : title.Trim();

        var @case = await _caseRepo.GetByCaseNoAsync(caseNo, ct);
        if (@case == null)
            throw new InvalidOperationException("Case not found.");

        // -------- Duplicate protection --------
        var exists = await _docRepo.ExistsByCaseAndHashAsync(@case.Id, fileHash, ct);
        if (exists)
            throw new InvalidOperationException(
                "This document already exists for the case (duplicate file).");
        // --------------------------------------

        var doc = new Document
        {
            Id = Guid.NewGuid(),
            CaseId = @case.Id,
            FileName = fileName,
            Title = title,
            DocumentType = documentType,
            DocumentStatus = DocumentStatus.New,
            FilePath = filePath,
            FileHash = fileHash,
            UploadedAt = DateTime.UtcNow
        };

        await _docRepo.AddAsync(doc, ct);
        await _docRepo.SaveChangesAsync(ct);

        return doc.Id;
    }
}
