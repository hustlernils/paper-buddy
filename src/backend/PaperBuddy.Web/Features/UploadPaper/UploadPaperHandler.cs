using System.Data;
using Dapper;
using PaperBuddy.MessageBus.Abstractions;

namespace PaperBuddy.Web.Features.UploadPaper;

public class UploadPaperHandler(IDbConnection connection, IMessageBus messageBus)
{
    private readonly IDbConnection _dbConnection = connection;
    private readonly IMessageBus _bus = messageBus; 
    
    public async Task<Guid> HandleAsync(UploadPaperRequest request, CancellationToken  cancellationToken)
    {
        var paperId = Guid.NewGuid();

        _dbConnection.Open();
        using var transaction = _dbConnection.BeginTransaction();

        try
        {
            await _dbConnection.ExecuteAsync(
                @"INSERT INTO papers (id, title, authors, year, doi, url, uploaded_by, created_at)
                VALUES (@Id, @Title, @Authors, @Year, @Doi, @Url, @UploadedBy, NOW())", new
                {
                    Id = paperId,
                    Title = "Test paper",
                    Authors = "Sparrow, Jack and Swann, Elizabeth",
                    Year = 2020,
                    Doi = "Sparrow",
                    Url = "Sparrow",
                    UploadedBy = new Guid("a3b99d2e-2fdf-4956-9690-cb6be5cf900a"),
                });

            await AddPaperData(request.File, paperId);

            await _bus.PublishAsync(new SummarizePaperRequest(paperId), cancellationToken);
            
            transaction.Commit();
            _dbConnection.Close();

            return paperId;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task AddPaperData(IFormFile file, Guid paperId)
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
}