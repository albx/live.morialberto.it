namespace MoriAlberto.Live.Models;

public class StreamingList
{
    public IEnumerable<StreamingListItem> Streamings { get; init; } = Enumerable.Empty<StreamingListItem>();

    public int NumberOfPages { get; init; }

    public record StreamingListItem
    {
        public string Title { get; init; } = string.Empty;

        public string Slug { get; init; } = string.Empty;

        public DateOnly ScheduleDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
