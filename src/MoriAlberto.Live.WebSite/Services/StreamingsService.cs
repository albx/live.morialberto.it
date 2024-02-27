using MoriAlberto.Live.Models;
using MoriAlberto.Live.WebSite.Client;
using MoriAlberto.Live.WebSite.Model;

namespace MoriAlberto.Live.WebSite.Services;

public class StreamingsService
{
    public StreamingsClient Client { get; }

    public StreamingsService(StreamingsClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IndexViewModel> GetStreamingsForHomePageAsync()
    {
        var model = new IndexViewModel();

        model.NextStreaming = (await Client.GetNextStreaming.ExecuteAsync(new DateTimeOffset(DateTime.Today)))
            .Data?
            .Streamings
            .Items
            .Select(s => new StreamingList.StreamingListItem
            {
                Title = s.Title,
                ScheduleDate = DateOnly.FromDateTime(s.ScheduleDate.DateTime),
                Slug = s.Slug,
                EndTime = TimeOnly.Parse(s.EndingTime),
                StartTime = TimeOnly.Parse(s.StartingTime)
            }).FirstOrDefault();

        var queryResult = await Client.GetLatestStreamings.ExecuteAsync();
        model.Streamings = queryResult.Data?
            .Streamings
            .Items
            .Select(s => new StreamingList.StreamingListItem
            {
                Title = s.Title,
                EndTime = TimeOnly.Parse(s.EndingTime),
                ScheduleDate = DateOnly.FromDateTime(s.ScheduleDate.DateTime),
                Slug = s.Slug,
                StartTime = TimeOnly.Parse(s.StartingTime)
            }) ?? [];

        //var streamings = await Client.GetFromJsonAsync<IEnumerable<StreamingList.StreamingListItem>>("api/streamings/scheduled");
        //model.Streamings = streamings ?? Array.Empty<StreamingList.StreamingListItem>();

        return model;
    }

    public async Task<ArchiveViewModel> GetStreamingsArchiveAsync(StreamingsSearchParameters? search = null)
    {
        search ??= new StreamingsSearchParameters();

        var sortDirection = search.Sort switch
        {
            StreamingsSearchParameters.SortDirection.Ascending => OrderBy.Asc,
            _ => OrderBy.Desc
        };

        var streamingsQuery = await Client.GetStreamingsArchive.ExecuteAsync(
            search.Query,
            sortDirection,
            numberOfItems: 20,
            search.PageCursor);

        var endCursor = streamingsQuery.Data?.Streamings.EndCursor;
        if (!string.IsNullOrEmpty(endCursor))
        {
            search.PageCursor = endCursor;
        }

        //var streamingList = await Client.GetFromJsonAsync<StreamingList>(url);
        var streamings = streamingsQuery.Data?
            .Streamings
            .Items
            .Select(s => new StreamingList.StreamingListItem
            {
                Title = s.Title,
                Slug = s.Slug,
                ScheduleDate = DateOnly.FromDateTime(s.ScheduleDate.Date),
                EndTime = TimeOnly.Parse(s.EndingTime),
                StartTime = TimeOnly.Parse(s.StartingTime)
            }) ?? [];

        var model = new ArchiveViewModel
        {
            Streamings = streamings,
            HasNextPage = streamingsQuery.Data?.Streamings.HasNextPage ?? false
        };

        return model;
    }

    public async Task<LiveDetailViewModel?> GetStreamingDetailAsync(string slug)
    {
        return new();
        //var streaming = await Client.GetFromJsonAsync<Streaming>($"api/streamings/detail/{slug}");
        //if (streaming is null)
        //{
        //    return null;
        //}

        //return new LiveDetailViewModel
        //{
        //    Abstract = Markdown.ToHtml(streaming.Abstract),
        //    EndTime = streaming.EndingTime,
        //    ScheduleDate = streaming.ScheduleDate,
        //    Seo = streaming.Seo,
        //    StartTime = streaming.StartingTime,
        //    Title = streaming.Title,
        //    TwitchUrl = streaming.HostingChannelUrl,
        //    YouTubeUrl = streaming.YouTubeVideoUrl?.Replace("https://youtu.be", "https://www.youtube.com/embed")
        //};
    }

    internal static List<string?> PageCursorMap = [null];
}
