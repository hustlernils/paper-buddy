using Dapper;
using System.Data;

namespace PaperBuddy.Web.Infrastructure;

public class StringArrayHandler : SqlMapper.TypeHandler<string[]>
{
    public override void SetValue(IDbDataParameter parameter, string[] value)
        => parameter.Value = value;

    public override string[] Parse(object value)
        => (string[])value;
}