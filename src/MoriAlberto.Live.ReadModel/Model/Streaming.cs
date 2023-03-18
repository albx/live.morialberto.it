namespace MoriAlberto.Live.ReadModel.Model;

public class Streaming : Content
{
    public DateOnly ScheduleDate { get; init; }

    public TimeOnly StartingTime { get; init; }

    public TimeOnly EndingTime { get; init; }

    public string HostingChannelUrl { get; init; } = string.Empty;

    public string? YouTubeVideoUrl { get; init; }
}
