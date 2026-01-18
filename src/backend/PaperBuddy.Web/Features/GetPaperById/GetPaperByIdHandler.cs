using System.Data;
using Dapper;

namespace PaperBuddy.Web.Features.GetPaperById;

public class GetPaperByIdHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;

    public async Task<PaperDetails> HandleAsync(GetPaperByIdRequest request, CancellationToken cancellationToken)
    {
        // TODO: Implement DB query to fetch PaperDetails by request.PaperId
        // Example: var paper = await _dbConnection.QuerySingleOrDefaultAsync<PaperDetails>(sql, new { PaperId = request.PaperId });
        // return paper;
        throw new NotImplementedException("Query not implemented yet");
    }
}