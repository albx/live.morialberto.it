using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MoriAlberto.Live.ReadModel.Persistence.Converters;

public class TimeOnlyConverter : ValueConverter<TimeOnly, TimeSpan>
{
    public TimeOnlyConverter()
        : base(t => t.ToTimeSpan(), t => TimeOnly.FromTimeSpan(t))
    {
    }
}
