using Markdig;
using MoriAlberto.Live.Models;
using MoriAlberto.Live.WebSite.Client;
using MoriAlberto.Live.WebSite.Model;

namespace MoriAlberto.Live.WebSite.Services;

public class StreamingsService
{
    public IStreamingsClient Client { get; }

    public StreamingsService(IStreamingsClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IndexViewModel> GetStreamingsForHomePageAsync()
    {
        var model = new IndexViewModel();
        var todayOffset = new DateTimeOffset(DateTime.Today);

        var nextStreamingResult = await Client.GetNextStreaming.ExecuteAsync(todayOffset);
        model.NextStreaming = nextStreamingResult.Data?
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

        var latestStreamingsResult = await Client.GetLatestStreamings.ExecuteAsync(todayOffset);
        model.Streamings = latestStreamingsResult.Data?
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

        var pageCursor = PageCursorProvider.GetCursor(search.Page);

        var streamingsQuery = await Client.GetStreamingsArchive.ExecuteAsync(
            search.Query,
            sortDirection,
            numberOfItems: 12,
            pageCursor);

        var endCursor = streamingsQuery.Data?.Streamings.EndCursor;
        PageCursorProvider.AddCursor(search.Page + 1, endCursor);

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
        var streamingDetailQuery = await Client.GetStreamingDetail.ExecuteAsync(slug);
        var streaming = streamingDetailQuery.Data?
            .Streamings
            .Items
            .SingleOrDefault();

        if (streaming is null)
        {
            return null;
        }

        return new()
        {
            Abstract = Markdown.ToHtml(streaming.Abstract ?? string.Empty),
            EndTime = TimeOnly.Parse(streaming.EndingTime),
            ScheduleDate = DateOnly.FromDateTime(streaming.ScheduleDate.Date),
            StartTime = TimeOnly.Parse(streaming.StartingTime),
            Title = streaming.Title,
            TwitchUrl = streaming.HostingChannelUrl,
            YouTubeUrl = streaming.YouTubeVideoUrl?.Replace("https://youtu.be", "https://www.youtube.com/embed"),
            Seo = new LiveDetailViewModel.SeoInfo
            {
                Title = streaming.Seo_Title ?? string.Empty,
                Description = streaming.Seo_Description ?? string.Empty,
                Keywords = streaming.Seo_Keywords ?? string.Empty,
            }
        };
    }
}
