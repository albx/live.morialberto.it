using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MoriAlberto.Live.Api.Configuration;
using MoriAlberto.Live.Models;
using System.Data.SqlClient;

namespace MoriAlberto.Live.Api.Services;

public class StreamingsService
{
    private readonly KittDatabaseConfiguration _databaseConfiguration;

    private readonly ILogger<StreamingsService> _logger;

    public StreamingsService(IOptions<KittDatabaseConfiguration> databaseConfigurationOptions, ILogger<StreamingsService> logger)
    {
        _databaseConfiguration = databaseConfigurationOptions?.Value ?? throw new ArgumentNullException(nameof(databaseConfigurationOptions));
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
            using var connection = new SqlConnection(_databaseConfiguration.ConnectionString);
            connection.Open();

            var sql = """
                      SELECT TOP 20
                      c.Title AS Title, c.Slug AS Slug, s.ScheduleDate AS ScheduleDate, s.StartingTime AS StartTime, s.EndingTime AS EndTime
                      FROM KITT_Contents c
                      JOIN KITT_Streamings s ON c.Id=s.Id
                      ORDER BY s.ScheduleDate, s.StartingTime, s.EndingTime
                      """;

            var streamings = await connection.QueryAsync<StreamingListItem>(sql);
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
