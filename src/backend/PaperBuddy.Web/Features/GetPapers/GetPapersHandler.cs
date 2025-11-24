using System.Data;
using Dapper;

namespace PaperBuddy.Web.Features.GetPapers;

public class GetPapersHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;
    public async Task<IEnumerable<GetPapersResponse>> HandleAsync(GetPapersRequest request, CancellationToken  cancellationToken)
    {
        var paperId = Guid.NewGuid();

        _dbConnection.Open();

        var sql = @"SELECT id, title FROM papers";
        var papers = await _dbConnection.QueryAsync<GetPapersResponse>(sql);

        _dbConnection.Close();

        return papers;
    }
}