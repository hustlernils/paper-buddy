using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;
using PaperBuddy.Web.Domain;

namespace PaperBuddy.Web.Features.UploadPaper;

public class UploadPaperHandler(IDbConnection connection, IMessageBus messageBus)
{
    private readonly IDbConnection _dbConnection = connection;
    private readonly IMessageBus _bus = messageBus; 
    
    public async Task<Guid> HandleAsync(UploadPaperRequest request, CancellationToken  cancellationToken)
    {
        _dbConnection.Open();
        using var transaction = _dbConnection.BeginTransaction();

        var paper = new Article()
        {
            UploadedBy = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"),
            Title = StripPdfExtension(request.File.FileName),
        };
        
        try
        {
            await _dbConnection.ExecuteAsync(
                @"INSERT INTO papers (id, uploaded_by, title, created_at)
                VALUES (@Id, @UploadedBy, @Title, NOW())", paper);

            await InsertPaperData(request.File, paper.Id);

            await _bus.PublishAsync(new ExtractPaperInfoRequest(paper.Id), cancellationToken);
            
            transaction.Commit();
            _dbConnection.Close();

            return paper.Id;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task InsertPaperData(IFormFile file, Guid paperId)
    {
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        await _dbConnection.ExecuteAsync(
            @"INSERT INTO paper_data (id, paper_id, data, created_at)
              VALUES (@Id, @PaperId, @Data, NOW())",
            new
            {
                Id = Guid.NewGuid(),
                PaperId = paperId,
                Data = ms.ToArray()
            }
        );
    }

    private string StripPdfExtension(string fileName) => fileName.Substring(0, fileName.LastIndexOf('.'));
}