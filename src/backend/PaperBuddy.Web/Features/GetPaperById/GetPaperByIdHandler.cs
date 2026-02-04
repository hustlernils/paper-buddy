using System.Data;
using Dapper;

namespace PaperBuddy.Web.Features.GetPaperById;

public class GetPaperByIdHandler(IDbConnection connection)
{
    private readonly IDbConnection _dbConnection = connection;

    public async Task<GetPaperDetailsResponse?> HandleAsync(GetPaperByIdRequest request, CancellationToken cancellationToken)
    {
        _dbConnection.Open();
        
        string sql = "SELECT id, title, authors, summary FROM papers WHERE id = @PaperId";
        
        var paper =  await _dbConnection.QueryFirstOrDefaultAsync<GetPaperDetailsResponse>(sql, new { request.PaperId });
        
        _dbConnection.Close();
        
        return paper is not null ? new GetPaperDetailsResponse(request.PaperId, paper.Title, paper.Authors, paper.Summary) : null;
    }
}