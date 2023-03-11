namespace MoriAlberto.Live.Models;

public class Streaming
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string Abstract { get; init; } = string.Empty;

    public DateTime ScheduleDate { get; init; }

    public TimeSpan StartingTime { get; init; }

    public TimeSpan EndingTime { get; init; }

    public string HostingChannelUrl { get; init; } = string.Empty;

    public string YouTubeVideoUrl { get; init; } = string.Empty;

    public SeoInfo Seo { get; init; } = default!;

    public record SeoInfo
    {
        public string Title { get; init; } = string.Empty;

        public string Description { get; init; } = string.Empty;

        public string Keywords { get; init; } = string.Empty;
    }
}
