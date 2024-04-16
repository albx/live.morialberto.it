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

    public SeoInfo Seo { get; init; } = new();

    public record SeoInfo
    {
        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public string Keywords { get; init; } = string.Empty;
    }
}
