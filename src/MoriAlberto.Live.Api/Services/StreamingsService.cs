using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoriAlberto.Live.Models;
using MoriAlberto.Live.ReadModel;
using System.Linq.Dynamic.Core;

namespace MoriAlberto.Live.Api.Services;

public class StreamingsService
{
    private readonly ILogger<StreamingsService> _logger;

    public IDatabase Database { get; }

    public StreamingsService(IDatabase database, ILogger<StreamingsService> logger)
    {
        Database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<StreamingList.StreamingListItem>> GetScheduledStreamingsAsync()
    {
        try
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var streamings = await Database.Streamings
                .OrderedBySchedule(ascending: false)
                .Where(s => s.ScheduleDate >= today)
                .Select(s => new StreamingList.StreamingListItem
                {
                    EndTime = s.EndingTime,
                    ScheduleDate = s.ScheduleDate,
                    Slug = s.Slug,
                    StartTime = s.StartingTime,
                    Title = s.Title
                }).Take(4).ToArrayAsync();

            return streamings;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero di tutte le live: {ErrorMessage}", ex.Message);
            return Array.Empty<StreamingList.StreamingListItem>();
        }
    }

    public async Task<StreamingList> GetAllStreamingsAsync(StreamingsSearchParameters search)
    {
        try
        {
            var ascending = search.Sort == StreamingsSearchParameters.SortDirection.Ascending;
            var streamingsQuery = Database.Streamings.OrderedBySchedule(ascending);
            if (!string.IsNullOrWhiteSpace(search.Query))
            {
                streamingsQuery = streamingsQuery.Where(s => s.Title.Contains(search.Query));
            }

            var pagedResult = streamingsQuery
                .Select(s => new StreamingList.StreamingListItem
                {
                    EndTime = s.EndingTime,
                    ScheduleDate = s.ScheduleDate,
                    Slug = s.Slug,
                    StartTime = s.StartingTime,
                    Title = s.Title
                }).PageResult(search.Page, 20);

            var streamings = await pagedResult.Queryable.ToArrayAsync();

            return new StreamingList
            {
                Streamings = streamings,
                NumberOfPages = pagedResult.PageCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero di tutte le live: {ErrorMessage}", ex.Message);
            return new StreamingList();
        }
    }

    public async Task<Streaming?> GetStreamingDetailAsync(string slug)
    {
        try
        {
            var streaming = await Database.Streamings
                .SingleOrDefaultAsync(s => s.Slug == slug);

            if (streaming is null)
            {
                return null;
            }

            return new Streaming
            {
                Abstract = streaming.Abstract ?? string.Empty,
                EndingTime = streaming.EndingTime,
                HostingChannelUrl = streaming.HostingChannelUrl,
                Id = streaming.Id,
                ScheduleDate = streaming.ScheduleDate,
                Seo = new Streaming.SeoInfo
                {
                    Description = streaming.Seo?.Description ?? string.Empty,
                    Keywords = streaming.Seo?.Keywords ?? string.Empty,
                    Title = streaming.Seo?.Title ?? string.Empty,
                },
                Slug = streaming.Slug,
                StartingTime = streaming.StartingTime,
                Title = streaming.Title,
                YouTubeVideoUrl = streaming.YouTubeVideoUrl
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero del dettaglio della live {Slug}: {ErrorMessage}", slug, ex.Message);
            throw;
        }
    }
}
