namespace MoriAlberto.Live.WebSite.Model;

public record StreamingListItem
{
    public string Title { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public DateOnly ScheduleDate { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }
}
