namespace MoriAlberto.Live.Models;

public record StreamingListItem
{
    public string Title { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public DateOnly ScheduleDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
