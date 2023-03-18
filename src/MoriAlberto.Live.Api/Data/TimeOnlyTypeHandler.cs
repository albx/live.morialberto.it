using Dapper;
using System.Data;

namespace MoriAlberto.Live.Api.Data;

public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
{
    public override TimeOnly Parse(object value)
    {
        var valueString = value.ToString();
        if (string.IsNullOrWhiteSpace(valueString))
        {
            return TimeOnly.MinValue;
        }

        var timeSpanValue = TimeSpan.Parse(valueString);
        return TimeOnly.FromTimeSpan(timeSpanValue);
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.Value = value.ToTimeSpan();
    }
}
