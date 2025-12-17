using Dapper;
using System.Data;

namespace PaperBuddy.Web.Infrastructure;

public class EnumTypeHandler<T> : SqlMapper.TypeHandler<T> where T : Enum
{
    public override T Parse(object value)
    {
        if (value is string stringValue)
            return (T)Enum.Parse(typeof(T), stringValue, true);
        
        return (T)value;
    }

    public override void SetValue(IDbDataParameter parameter, T? value)
    {
        parameter.Value = value?.ToString();
        parameter.DbType = DbType.String;
    }
}