using MoriAlberto.Live.Models;
using MoriAlberto.Live.WebSite.Model;
using System.Net.Http.Json;

namespace MoriAlberto.Live.WebSite.Services;

public class StreamingsService
{
    public HttpClient Client { get; }

    public StreamingsService(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<IndexViewModel> GetStreamingsForHomePageAsync()
    {
        var model = new IndexViewModel();
        model.Streamings = Enumerable.Range(0, 6).Select(i => new StreamingListItem
        {
            Title = $"Test {i}",
            ScheduleDate = DateOnly.FromDateTime(DateTime.Now),
            StartTime = new TimeOnly(16, 0),
            EndTime = new TimeOnly(18, 0),
            Slug = $"test-{i}"
        });

        return Task.FromResult(model);
    }

    public async Task<ArchiveViewModel> GetStreamingsArchiveAsync()
    {
        var streamings = await Client.GetFromJsonAsync<IEnumerable<StreamingListItem>>("api/streamings");
        var model = new ArchiveViewModel
        {
            Streamings = streamings ?? Array.Empty<StreamingListItem>(),
        };

        return model;
    }
}
