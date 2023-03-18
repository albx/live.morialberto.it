using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MoriAlberto.Live.ReadModel.Persistence.Converters;

internal class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter()
        : base(d => d.ToDateTime(TimeOnly.MinValue), d => DateOnly.FromDateTime(d))
    {
    }
}
