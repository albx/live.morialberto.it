using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MoriAlberto.Live.ReadModel.Persistence.Comparer;

internal class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer()
          : base((d1, d2) => d1 == d2, d => d.GetHashCode())
    {
    }
}
