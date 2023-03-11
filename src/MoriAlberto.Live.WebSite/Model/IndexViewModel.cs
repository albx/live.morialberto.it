namespace MoriAlberto.Live.WebSite.Model;

public class IndexViewModel
{
    public IEnumerable<ScheduledStreaming> Streamings { get; set; } = Enumerable.Empty<ScheduledStreaming>();

    public record ScheduledStreaming
    {
        public string Title { get; init; } = string.Empty;

        public string Slug { get; init; } = string.Empty;

        public DateOnly ScheduleDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
