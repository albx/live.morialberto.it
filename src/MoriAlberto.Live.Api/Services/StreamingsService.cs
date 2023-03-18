using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoriAlberto.Live.Models;
using MoriAlberto.Live.ReadModel;

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

    public async Task<IEnumerable<StreamingListItem>> GetScheduledStreamingsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StreamingListItem>> GetAllStreamingsAsync()
    {
        try
        {
            var streamings = await Database.Streamings
                .Select(s => new StreamingListItem
                {
                    EndTime = s.EndingTime,
                    ScheduleDate = s.ScheduleDate,
                    Slug = s.Slug,
                    StartTime = s.StartingTime,
                    Title = s.Title
                }).Take(20).ToArrayAsync();

            return streamings;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Errore durante il recupero di tutte le live: {ErrorMessage}", ex.Message);
            return Array.Empty<StreamingListItem>();
        }
    }

    public async Task<Streaming?> GetStreamingDetailAsync(string slug)
    {
        throw new NotImplementedException();
    }
}
