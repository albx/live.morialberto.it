using MoriAlberto.Live.ReadModel.Model;

namespace MoriAlberto.Live.ReadModel;

public static class StreamingExtensions
{
    public static IQueryable<Streaming> OrderedBySchedule(this IQueryable<Streaming> streamings, bool ascending = true)
    {
        return ascending switch
        {
            false => OrderedByScheduleDescending(streamings),
            _ => OrderedByScheduleAscending(streamings)
        };
    }

    private static IQueryable<Streaming> OrderedByScheduleAscending(IQueryable<Streaming> streamings)
    {
        return streamings
            .OrderBy(s => s.ScheduleDate)
            .ThenBy(s => s.StartingTime)
            .ThenBy(s => s.EndingTime);
    }

    private static IQueryable<Streaming> OrderedByScheduleDescending(IQueryable<Streaming> streamings)
    {
        return streamings
            .OrderByDescending(s => s.ScheduleDate)
            .ThenByDescending(s => s.StartingTime)
            .ThenByDescending(s => s.EndingTime);
    }
}
