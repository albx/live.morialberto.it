using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MoriAlberto.Live.ReadModel.Persistence.Comparer;

public class TimeOnlyComparer : ValueComparer<TimeOnly>
{
    public TimeOnlyComparer()
        : base((t1, t2) => t1 == t2, t => t.GetHashCode())
    {
    }
}
