using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.WebSite.Model;

public class LiveDetailViewModel
{
    public string Title { get; init; } = string.Empty;

    public string Abstract { get; init; } = string.Empty;

    public DateOnly ScheduleDate { get; init; }

    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }

    public string TwitchUrl { get; init; } = string.Empty;

    public string? YouTubeUrl { get; init; } = string.Empty;

    public Streaming.SeoInfo Seo { get; init; } = new();
}
