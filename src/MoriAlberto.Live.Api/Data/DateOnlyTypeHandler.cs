using Dapper;
using System.Data;

namespace MoriAlberto.Live.Api.Data;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        var valueString = value.ToString();
        if (string.IsNullOrWhiteSpace(valueString))
        {
            return DateOnly.MinValue;
        }

        var dateTimeValue = Convert.ToDateTime(valueString);
        return DateOnly.FromDateTime(dateTimeValue);
    }

    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }
}
