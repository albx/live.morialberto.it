using Microsoft.Extensions.Options;
using MoriAlberto.Live.Api.Configuration;
using MoriAlberto.Live.Models;

namespace MoriAlberto.Live.Api.Services;

public class StreamingsService
{
    private readonly KittDatabaseConfiguration _databaseConfiguration;

    public StreamingsService(IOptions<KittDatabaseConfiguration> databaseConfigurationOptions)
    {
        _databaseConfiguration = databaseConfigurationOptions?.Value ?? throw new ArgumentNullException(nameof(databaseConfigurationOptions));
    }

    public async Task<IEnumerable<Streaming>> GetScheduledStreamingsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Streaming>> GetAllStreamingsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Streaming?> GetStreamingDetailAsync(string slug)
    {
        throw new NotImplementedException();
    }
}
